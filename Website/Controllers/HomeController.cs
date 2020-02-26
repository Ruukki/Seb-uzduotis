using DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using DAL;
using RestSharp;
using Website.Models;
using System.Xml.Serialization;
using System.Data.Linq;
using Newtonsoft.Json;

namespace Website.Controllers
{

    public class HomeController : Controller
    {
        GetDbInfo db = new GetDbInfo();

        public ActionResult Index()
        {
            DeployDb();
            var model = new IndexViewModel();
            model.Agreements = db.GetAll<Agreement>().ToList();
            GetInfo("1", "");
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult GetInfo(string agreementId, string newBaseRateCode)
        {
            var model = GetAgreementInformation(agreementId, newBaseRateCode);

            return PartialView("InformationPartial", model);
        }

        [HttpGet]
        
        public ContentResult GetInformation(string agreementId, string newBaseRateCode)
        {
            var model = GetAgreementInformation(agreementId, newBaseRateCode);
            model.Customer.Agreements = null;
            model.Agreement.Customer = null;
            var json = JsonConvert.SerializeObject(model);
            return new ContentResult{Content = json, ContentType = "application/json" };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private decimal GetBaseRates(string rateCode)
        {
            var client = new RestClient("http://www.lb.lt/");
            var request =
                new RestRequest("/webservices/VilibidVilibor/VilibidVilibor.asmx/getLatestVilibRate", Method.GET,
                    DataFormat.Xml).AddParameter("RateType", rateCode);
            var response = client.Get(request).Content;
            var xml = new XmlDocument();
            xml.LoadXml(response);
            var result = decimal.Parse(xml["decimal"].InnerText.Replace('.', ','));
            
            return result;
        }

        private InformationViewModel GetAgreementInformation(string agreementId, string newBaseRateCode)
        {
            var model = new InformationViewModel();
            int id;
            if (int.TryParse(agreementId, out id))
            {
                var agreement = db.Get<Agreement>(x => x.Id == id, true);
                if (agreement == null)
                {
                    model.Error = "Agreement not found";
                }
                else
                {
                    model.Agreement = agreement;
                    model.Customer = agreement.Customer;
                    try
                    {
                        var oldRate = GetBaseRates(agreement.BaseRateCode);
                        var newRate = GetBaseRates(newBaseRateCode);
                        model.OldIntrestRate = oldRate + agreement.Margin;
                        model.NewInteresRate = newRate + agreement.Margin;
                        model.IntrestRateDiff = newRate - oldRate;
                    }
                    catch (Exception ex)
                    {
                        model.Error = ex.Message;
                    }
                }
            }
            else
            {
                model.Error = "Incorrect agreement ID";
            }

            return model;
        }

        private bool DeployDb()
        {
            try
            {
                var version = db.Get<DbVersion>(x => x.Id == 1);
                if (version == null)
                {
                    db.Save(new DbVersion {Version = "1.0"});
                    var agr1 = new Agreement(60, 12000, "VILIBOR3m", 1.6m);
                    var agr2 = new Agreement(36, 8000, "VILIBOR1y", 2.2m);
                    var agr3 = new Agreement(24, 1000, "VILIBOR6m", 1.85m);

                    var cust1 = new Customer
                    {
                        Name = "Goranas Truksevičius", PersonCode = "67812267006",
                        Agreements = new List<Agreement> {agr1}
                    };
                    var cust2 = new Customer
                    {
                        Name = "Dange Kulčiutė", PersonCode = "78706196287",
                        Agreements = new List<Agreement> {agr2, agr3}
                    };

                    db.Save(agr1);
                    db.Save(agr2);
                    db.Save(agr3);
                    db.Save(cust1);
                    db.Save(cust2);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
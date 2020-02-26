using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Model
{
    [DataContract]
    public class Agreement : BaseModel
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Amount { get; set; }
        [DataMember]
        public string BaseRateCode { get; set; }
        [DataMember]
        public decimal Margin { get; set; }
        [DataMember]
        public int AgreementDuration { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public Agreement(int agreementDuration, int amount, string baseRateCode, decimal margin)
        {
            Amount = amount;
            AgreementDuration = agreementDuration;
            BaseRateCode = baseRateCode;
            Margin = margin;
        }
        public Agreement() { }
        public override string ToString()
        {
            var result = string.Format("ID: {0} Amount: {1} Base rate code: {2} Margin: {3} Agreement duration: {4}", Id, Amount, BaseRateCode, Margin, AgreementDuration);
            return result;
        }
    }
}

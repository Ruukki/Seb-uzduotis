# Seb-uzduotis

Running software:
Run the source using Visual Studio and IIS express or deploy website website to IIS (https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/visual-studio-web-deployment/deploying-to-iis)

Database connection string can be found in DAL project config file.

Using as API:

GET /Home/GetInformation
Parameters
string  agreementId       required
string  newBaseRateCode   required

Response example:
{
  "agreement": {
          "id": 0,
          "amount": 0,
          "baseRateCode": null,
          "margin": 0.0,
          "agreementDuration": 0
        },
  "customer": {
          "id": 0,
          "personCode": null,
          "name": null
        },
  "oldIntrestRate": 0.0,
  "newInteresRate": 0.0,
  "intrestRateDiff": 0.0,
  "error": null
}


Framework:
.net
ASP.Net
EntityFramework

Libraries:
NewtonsoftJson
RestSharp

Architecture
Model View Controller

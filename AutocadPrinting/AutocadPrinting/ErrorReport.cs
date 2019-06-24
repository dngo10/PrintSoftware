using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
// https://docs.google.com/spreadsheets/d/1dkg_50SS75DFuC62zG84Nx3_QNTZtYsMSGQatdw0lRM/edit?usp=sharing
// My Project 75742-1331e93eae5f
// AIzaSyAFNbJY4kgTx2dh1DDY6zmoVU0mX7EwEBE
namespace AutocadPrinting
{
    class ErrorReport
    {
        public static void getError()
        {


            string[] Scopes = { SheetsService.Scope.Spreadsheets, };
            string ApplicationName = "Google Sheets API .NET Quickstart";
            UserCredential credential;

            /*
            string credentialPath = "My Project 75742-1331e93eae5f.json";
            var json = File.ReadAllText(credentialPath);
            Newtonsoft.Json.Linq.JObject cr = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
            string s = (string)cr.GetValue("private_key");
            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(s)
            {
                Scopes = Scopes1
            }.FromPrivateKey(s));
            */


            using (var stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory +  "\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }


            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            string spreadsheetID = "1ogxUDIhO0z9PCG-Mv-wPlCWooxxH7_DPw3JHoRkRdv8";
            //initializer.HttpClientInitializer = xCred;        
            
            
            string range = "Sheet1!A:A";
            ValueRange valueRange = new ValueRange();

            valueRange.MajorDimension = "ROWS";
            IList<IList<object>> values = new List<IList<object>>();
            IList<object> dataList = new List<object>();

            // INFORMATION TO ADD TO STRING
            dataList.Add(Environment.UserName);
            dataList.Add(DateTime.Now.ToString());

            values.Add(dataList);

            valueRange.Values = values;

            SpreadsheetsResource.ValuesResource.AppendRequest appendRequest = new SpreadsheetsResource.ValuesResource.AppendRequest(service, valueRange, spreadsheetID, range);
            appendRequest.ValueInputOption = (SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum)1;

            appendRequest.Execute();
        }
    }
}

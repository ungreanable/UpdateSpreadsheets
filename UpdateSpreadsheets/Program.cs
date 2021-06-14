using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using UpdateSpreadsheets.Models;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace UpdateSpreadsheets
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        private static readonly HttpClient client = new HttpClient();
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        const int HIDE = 0;
        const int SHOW = 5;

        static bool SCHEDULE = false;

        public static void DisappearConsole()
        {
            ShowWindow(GetConsoleWindow(), HIDE);
        }

        static async Task Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--schedule")
            {
                DisappearConsole();
                SCHEDULE = true; 
            }
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
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
            try
            {
                // Define request parameters.
                var reqDopple = await RequestDoppleAPI();
                String spreadsheetId = "1b5dyW7E_n-7HM-gqtYnD0lqaAsd8uqO0j7XDqMs6zBg";

                var valueRange = GenerateValueRange(reqDopple.DopplePriceUsd, "API Connector!A2");
                UpdateGoogleRequest(service, valueRange.Item1, spreadsheetId, valueRange.Item2);
                Console.WriteLine($"Update Price DOP (${ reqDopple.DopplePriceUsd }) to {valueRange.Item2} Successfully !");

                valueRange = GenerateValueRange(reqDopple.DoppleData.MarketCap, "API Connector!AA2");
                UpdateGoogleRequest(service, valueRange.Item1, spreadsheetId, valueRange.Item2);
                Console.WriteLine($"Update DOP MarketCap (${ reqDopple.DoppleData.MarketCap }) to {valueRange.Item2} Successfully !");

                valueRange = GenerateValueRange(reqDopple.DoppleData.TotalTradingVolume, "API Connector!W2");
                UpdateGoogleRequest(service, valueRange.Item1, spreadsheetId, valueRange.Item2);
                Console.WriteLine($"Update DOP TradingVolume (${ reqDopple.DoppleData.TotalTradingVolume }) to {valueRange.Item2} Successfully !");
                
                valueRange = GenerateValueRange(reqDopple.DoppleData.DoppleTotalSupply, "API Connector!X2");
                UpdateGoogleRequest(service, valueRange.Item1, spreadsheetId, valueRange.Item2);
                Console.WriteLine($"Update DOP TotalSupply (${ reqDopple.DoppleData.DoppleTotalSupply }) to {valueRange.Item2} Successfully !");

                valueRange = GenerateValueRange(reqDopple.TotalValueLock.TvlSum, "API Connector!AJ2");
                UpdateGoogleRequest(service, valueRange.Item1, spreadsheetId, valueRange.Item2);
                Console.WriteLine($"Update DOP Total Value Lock (${ reqDopple.TotalValueLock.TvlSum }) to {valueRange.Item2} Successfully !");

                var updatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                valueRange = GenerateValueRange(updatedDate, "API Connector!A5");
                UpdateGoogleRequest(service, valueRange.Item1, spreadsheetId, valueRange.Item2);
                Console.WriteLine($"Updated Date (${ updatedDate }) to {valueRange.Item2} Successfully !");
                if(SCHEDULE)
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Found exception Type: {ex.GetType()} then cannot Update Price");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key to exit...");
            //new ManualResetEvent(false).WaitOne();
            Environment.Exit(0);
        }

        private static UpdateValuesResponse UpdateGoogleRequest(SheetsService service, ValueRange valueRange, string spreadsheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.UpdateRequest updateReq = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            updateReq.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result = updateReq.Execute();
            return result;
        }

        private static Tuple<ValueRange, string> GenerateValueRange(dynamic value, string range)
        {
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "COLUMNS";//"ROWS";//COLUMNS

            var oblist = new List<object>() { value };
            valueRange.Values = new List<IList<object>> { oblist };
            return Tuple.Create(valueRange, range);
        }

        private static async Task<DoppleAPI> RequestDoppleAPI()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36");

            var stringTask = client.GetStringAsync("https://dopple-api.kowito.com");

            var msg = await stringTask;
            DoppleAPI myDeserializedClass = JsonSerializer.Deserialize<DoppleAPI>(msg);
            return myDeserializedClass;
            //Console.Write(msg);
        }
    }
}
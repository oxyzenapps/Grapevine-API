using Azure.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace grapevineApi.Controllers
{
    
    [ApiController]
    public class ExtractorController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ExtractorController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [Route("lands/in-mh/bhulekh-7-12")]
        [HttpGet]
        public async Task<IActionResult> BhulekhExtractor(string District, string Taluka, string Village, string SurveyNumber, string HissaNumber, string MobileNo, string Language)
        {
            //var exePath = @"D:\Office Projects\Grapevine\Grapevine_website\BhulekhExtractor\BhulekhExtractor.exe";
            var exePath = Path.Combine(
    _env.ContentRootPath,
    "BhulekhExtractor",
    "publish",
    "BhulekhExtractor.exe"
);
            string url = "https://bhulekh.mahabhumi.gov.in/";
            District = await TransliterateToMarathi(District);
            Taluka = await TransliterateToMarathi(Taluka);
            Village = await TransliterateToMarathi(Village);
            HissaNumber = await EnglishToHindiSmart(HissaNumber);

            var payload = new ScrapeRequest
            {
                District = District,
                Taluka = Taluka,
                Village = Village,
                SurveyNumber = SurveyNumber,
                HissaNumber = HissaNumber,
                MobileNo = MobileNo,
                Language = Language,
                Url = url,
                UrlType = "Bhulekh"
            };
            var json = JsonConvert.SerializeObject(payload);
            string escapedJson = json.Replace("\"", "\\\"");
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{escapedJson}\"",   // ✅ quoted
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var process = Process.Start(psi))
            {
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                var output = await outputTask;
                var error = await errorTask;

                if (!string.IsNullOrWhiteSpace(error))
                    JsonConvert.DeserializeObject(error);

                return Ok(output);
            }
        }

        [Route("lands/in-mh/circlerates")]
        [HttpGet]
        public async Task<IActionResult> CircleRatesExtractor(string District, string Taluka, string Village, string SurveyNumber, string HissaNumber, string MobileNo, string Language)
        {
            //var exePath = @"D:\Office Projects\Grapevine\Grapevine_website\BhulekhExtractor\BhulekhExtractor.exe";
            var exePath = Path.Combine(
    _env.ContentRootPath,
    "BhulekhExtractor",
    "publish",
    "BhulekhExtractor.exe"
);
            string url = "https://igreval.maharashtra.gov.in/eASR2.0/eASRCommon.aspx";
            
            var payload = new ScrapeRequest
            {
                District = District,
                Taluka = Taluka,
                Village = Village,
                SurveyNumber = SurveyNumber,
                HissaNumber = HissaNumber,
                MobileNo = MobileNo,
                Language = Language,
                Url = url,
                UrlType = "CircleRate"
            };
            var json = JsonConvert.SerializeObject(payload);
            string escapedJson = json.Replace("\"", "\\\"");
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{escapedJson}\"",   // ✅ quoted
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var process = Process.Start(psi))
            {
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                var output = await outputTask;
                var error = await errorTask;

                if (!string.IsNullOrWhiteSpace(error))
                    JsonConvert.DeserializeObject(error);

                return Ok(output);
            }
        }

        [Route("lands/in-mh/bhunaksha")]
        [HttpGet]
        public async Task<IActionResult> BhuNakshaExtractor(string District, string Taluka, string Village,string SurveyNumber,string HissaNumber,string MobileNo,string Language)
        {
            //var exePath = @"D:\Office Projects\Grapevine\Grapevine_website\BhulekhExtractor\BhulekhExtractor.exe";
            var exePath = Path.Combine(
    _env.ContentRootPath,
    "BhulekhExtractor",
    "publish",
    "BhulekhExtractor.exe"
);
            string url = "https://mahabhunakasha.mahabhumi.gov.in/27/index.html";
            District = await TransliterateToMarathi(District);
            Taluka = await TransliterateToMarathi(Taluka);
            Village = await TransliterateToMarathi(Village);
            
            var payload = new ScrapeRequest
            {
                District = District,
                Taluka = Taluka,
                Village = Village,
                SurveyNumber = SurveyNumber,
                HissaNumber = HissaNumber,
                MobileNo = MobileNo,
                Language = Language,
                Url = url,
                UrlType = "BhuNaksha"
            };
            var json = JsonConvert.SerializeObject(payload);
            string escapedJson = json.Replace("\"", "\\\"");
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{escapedJson}\"",   // ✅ quoted
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var process = Process.Start(psi))
            {
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                var output = await outputTask;
                var error = await errorTask;

                if (!string.IsNullOrWhiteSpace(error))
                    JsonConvert.DeserializeObject(error);

                return Ok(output);
            }
        }

        [Route("lands/in-mh/Index2")]
        [HttpGet]
        public async Task<IActionResult> Index2(string District, string Taluka, string Village, string SurveyNumber, string HissaNumber, string MobileNo, string Language,string TabText,
            string ToYear, string FromYear, string FromPageNo, string Last_Page, string Last_Year)
        {
            if (String.IsNullOrWhiteSpace(FromYear))
            {
                FromYear= DateTime.Now.Year.ToString();
            }
            if (String.IsNullOrWhiteSpace(ToYear))
            {
                ToYear = (DateTime.Now.Year + 10).ToString();
            }
            if (String.IsNullOrWhiteSpace(FromPageNo))
            {
                FromPageNo = "1";
            }
            if(String.IsNullOrWhiteSpace(Last_Page))
            {
                Last_Page = "0";
            }
            if(String.IsNullOrWhiteSpace(Last_Year))
            {
                    Last_Year = "0";
            }
            
            //var exePath = @"D:\Office Projects\Grapevine\Grapevine_website\BhulekhExtractor\BhulekhExtractor.exe";
            var exePath = Path.Combine(
            _env.ContentRootPath,
            "BhulekhExtractor",
            "publish",
            "BhulekhExtractor.exe"
);
            string url = "https://freesearchigrservice.maharashtra.gov.in/";
            //District = await TransliterateToMarathi(District);
            //Taluka = await TransliterateToMarathi(Taluka);
            //Village = await TransliterateToMarathi(Village);
            var payload = new ScrapeRequest
            {
                District = District,
                Taluka = Taluka,
                Village = Village,
                SurveyNumber = SurveyNumber,
                HissaNumber = HissaNumber,
                MobileNo = MobileNo,
                Language = Language,
                Url = url,
                UrlType = "Index2",
                TabText = TabText,
                ToYear = ToYear,
                FromYear = FromYear,
                FromPageNo = FromPageNo,
                Last_Page = Last_Page,
                Last_Year = Last_Year
            };
            var json = JsonConvert.SerializeObject(payload);
            string escapedJson = json.Replace("\"", "\\\"");
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{escapedJson}\"",   // ✅ quoted
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var process = Process.Start(psi))
            {
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                var output = await outputTask;
                var error = await errorTask;

                if (!string.IsNullOrWhiteSpace(error))
                    JsonConvert.DeserializeObject(error);

                return Ok(output);
            }
        }

        public class ScrapeRequest
        {
            public string Url { get; set; } = "";
            public string District { get; set; } = "";
            public string Taluka { get; set; } = "";
            public string Village { get; set; } = "";
            public string SurveyNumber { get; set; } = "";
            public string HissaNumber { get; set; } = "";
            public string MobileNo { get; set; } = "";
            public string Language { get; set; } = "";
            public string UrlType { get; set; } = "";
            public string TabText { get; set; } = "";
            public string ToYear { get; set; } = "";
            public string FromYear { get; set; } = "";
            public string FromPageNo { get; set; } = "";
            public string Last_Page { get; set; } = "";
            public string Last_Year { get; set; } = "";
        }
        [NonAction]
        public static async Task<string> EnglishToHindiSmart(string text)
        {
            // Case: 1/A or 58/B type
            var match = System.Text.RegularExpressions.Regex.Match(text, @"^(\d+)\/([A-Za-z])$");

            if (match.Success)
            {
                string numberPart = match.Groups[1].Value;
                string letterPart = match.Groups[2].Value.ToUpper();

                // Manual mapping A-Z → Hindi
                var map = new Dictionary<string, string>()
        {
            {"A","अ"},{"B","ब"},{"C","क"},{"D","द"},{"E","इ"},
            {"F","फ"},{"G","ग"},{"H","ह"},{"I","इ"},{"J","ज"},
            {"K","क"},{"L","ल"},{"M","म"},{"N","न"},{"O","ओ"},
            {"P","प"},{"Q","क"},{"R","र"},{"S","स"},{"T","त"},
            {"U","उ"},{"V","व"},{"W","व"},{"X","क्स"},{"Y","य"},{"Z","ज"}
        };

                string hindiLetter = map.ContainsKey(letterPart) ? map[letterPart] : letterPart;

                return $"{numberPart}/{hindiLetter}";
            }

            // Otherwise normal phonetic conversion
            using var client = new HttpClient();

            string url = $"https://inputtools.google.com/request?text={Uri.EscapeDataString(text)}&itc=hi-t-i0-und&num=1";

            var response = await client.GetStringAsync(url);
            var json = Newtonsoft.Json.Linq.JArray.Parse(response);

            if (json[0].ToString() != "SUCCESS")
                return text;

            string hindi = json[1][0][1][0].ToString();

            // Convert Hindi digits back to English
            hindi = hindi
                .Replace("०", "0")
                .Replace("१", "1")
                .Replace("२", "2")
                .Replace("३", "3")
                .Replace("४", "4")
                .Replace("५", "5")
                .Replace("६", "6")
                .Replace("७", "7")
                .Replace("८", "8")
                .Replace("९", "9");

            return hindi;
        }
        [NonAction]
        public async Task<string> TransliterateToMarathi(string text)
        {
            try
            {

            
            using (var client = new HttpClient())
            {
                // Google Transliteration API endpoint
                string url = $"https://inputtools.google.com/request?text={text}&itc=mr-t-i0-und&num=1&cp=0&cs=1&ie=utf-8&oe=utf-8";

                var response = await client.GetStringAsync(url);

                JArray root = JArray.Parse(response);

                // Navigate JSON structure
                string convertedValue = root[1]      // second element
                                           [0]       // first item inside
                                           [1]       // translated array
                                           [0]       // actual string
                                           .ToString();

                // Parse the JSON response
                var jsonDoc = JsonConvert.DeserializeObject(response);
                //var transliterated = jsonDoc.RootElement[1][0][1][0].GetString();

                return convertedValue;
            }
            }
            catch
            {
                throw new Exception("Error");
            }
        }
    }
}

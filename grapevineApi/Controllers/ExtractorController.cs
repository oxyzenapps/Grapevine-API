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
        [Route("lands/in-mh/bhulekh")]
        [HttpGet]
        public async Task<IActionResult> BhulekhExtractor(string District, string Taluka, string Village, string SurveyNumber1, string SurveryNumber, string MobileNo, string Language)
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
            var payload = new ScrapeRequest
            {
                District = District,
                Taluka = Taluka,
                Village = Village,
                SurveyNumber1 = SurveyNumber1,
                SurveryNumber = SurveryNumber,
                MobileNo = MobileNo,
                Language = Language,
                Url = url
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
            public string SurveyNumber1 { get; set; } = "";
            public string SurveryNumber { get; set; } = "";
            public string MobileNo { get; set; } = "";
            public string Language { get; set; } = "";
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using WaterMarkDocs.Models;
using WatermarkService;

namespace WaterMarkDocs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWaterStampService _waterstampService;

        public HomeController(IWaterStampService waterstampService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _waterstampService = waterstampService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Watermark(IFormFile pdfFile, string phrase, int watermarkMode)
        {
            string filename = string.Empty;
            if (string.IsNullOrWhiteSpace(phrase))
            {
                ViewData["errmsg"] = "No watermark phrase given!";
            }
            else
            {
                if (pdfFile?.Length > 0)
                {
                    using var dest = new MemoryStream();
                    filename = Path.GetFileName(pdfFile.FileName);
                    using Stream src = pdfFile.OpenReadStream();
                    if (watermarkMode == 1)
                    {
                        _waterstampService.AddWatermarkEveryPage(src, dest, phrase);
                    }
                    else
                    {
                        _waterstampService.AddWatermarkLastPage(src, dest, phrase);
                    }
                    var reader = new MemoryStream(dest.ToArray());
                    filename = filename.Replace(".pdf", "-watermarked.pdf");
                    return File(reader, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }
                else
                {
                    ViewData["errmsg"] = "Select a PDF file!";
                }
            }

            return View("Index");
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

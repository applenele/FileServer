using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AlentFileClientTest.Helper;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AlentFileClientTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Upload(IFormFile file)
        {
            Stream stream = new MemoryStream();
            file.CopyTo(stream);
            string result = await HttpPost.UploadFilesToRemoteUrl("http://localhost:5001/Upload/", stream, file.FileName);
            return Content(result);
        }
    }
}

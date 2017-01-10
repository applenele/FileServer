using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AlentFileClientTest.Helper;
using System.IO;
using Microsoft.Extensions.Configuration;
using AlentFileClientTest.Models;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AlentFileClientTest.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;

        public List<FileServerModel> FileServers { set; get; }

        public HomeController(IOptions<List<FileServerModel>> options)
        {
            FileServers = options.Value;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Upload(IFormFile file)
        {
            Stream stream = new MemoryStream();
            file.CopyTo(stream);
            int num = RandomHelper.GetRandom(FileServers.Count);
            FileServerModel fileServer = FileServers[num - 1];
            string url = string.Format("{0}://{1}:{2}/{3}", "http", fileServer.Host, fileServer.Port, "Upload");
            //string result = await HttpPost.UploadFilesToRemoteUrl("http://localhost:5001/Upload/", stream, file.FileName);
            string result = await HttpPost.UploadFilesToRemoteUrl(url, stream, file.FileName, "FileUpload");
            return Content(result);
        }
    }
}

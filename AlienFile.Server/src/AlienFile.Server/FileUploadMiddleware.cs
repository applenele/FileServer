using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using AlienFile.Server.Helper;

namespace AlienFile.Server
{
    public class FileUploadMiddleware
    {
        private readonly RequestDelegate _next;
        private IHostingEnvironment _hostingEnv;

        public FileUploadMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv)
        {
            _next = next;
            _hostingEnv = hostingEnv;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post)
            {
                var files = context.Request.Form.Files;
                foreach (var file in files)
                {
                    string fileName = DateHelper.GetTimeStamp() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(_hostingEnv.WebRootPath, "Upload", fileName);

                    using (FileStream fs = System.IO.File.Create(path))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
            //await _next.Invoke(context);
        }
    }
}

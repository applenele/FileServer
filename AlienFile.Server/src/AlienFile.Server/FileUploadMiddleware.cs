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
                try
                {
                    var files = context.Request.Form.Files;
                    foreach (var file in files)
                    {
                        string fileName = DateHelper.GetTimeStamp() + Path.GetExtension(file.FileName);
                        string path = Path.Combine(_hostingEnv.WebRootPath, "File", fileName);

                        using (FileStream fs = System.IO.File.Create(path))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("上传成功");
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = 503;
                    await context.Response.WriteAsync("上传失败");
                }
            }
        }
    }
}

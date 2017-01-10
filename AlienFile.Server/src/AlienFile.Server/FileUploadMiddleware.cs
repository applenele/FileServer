using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using AlienFile.Server.Helper;
using AlienFile.Server.Models;

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
                    var path = context.Request.Query["filepath"];
                    if (string.IsNullOrEmpty(path))
                    {
                        path = context.Request.Form["filepath"];
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = "File";
                    }

                    var files = context.Request.Form.Files;
                    List<string> filePathList = new List<string>();
                    List<string> realPathList = new List<string>();
                    foreach (var file in files)
                    {
                        string fileName = DateHelper.GetTimeStamp() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(path, fileName);
                        string fileDir = Path.Combine(_hostingEnv.WebRootPath,path);
                        if (!Directory.Exists(fileDir))
                        {
                            Directory.CreateDirectory(fileDir);
                        }
                        string realPath = Path.Combine(_hostingEnv.WebRootPath, filePath);

                        filePathList.Add(filePath);
                        realPathList.Add(realPath);

                        using (FileStream fs = System.IO.File.Create(realPath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    context.Response.StatusCode = 200;
                    ReturnMessage message = new ReturnMessage();
                    message.IsSuccess = true;
                    message.Message = "上传成功";
                    message.Path = string.Join(",",filePathList);
                    message.Path = string.Join(",", realPathList);
                    await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(message));
                }
                catch (Exception e)
                {
                    ReturnMessage message = new ReturnMessage();
                    message.IsSuccess = false;
                    message.Message = "上传失败";
                    context.Response.StatusCode = 503;
                    await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(message));
                }
            }
        }
    }
}

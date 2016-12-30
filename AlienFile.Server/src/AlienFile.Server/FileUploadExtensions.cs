using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienFile.Server
{
    public static class FileUploadExtensions
    {
        public static IApplicationBuilder UseFileUpload(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FileUploadMiddleware>();//手工高亮
        }
    }
}

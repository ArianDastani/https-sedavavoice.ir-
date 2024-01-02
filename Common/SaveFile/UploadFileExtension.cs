using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Common.SaveFile
{
    public static class UploadFileExtension
    {
        public static void AddImageAjaxToServer(this IFormFile file, string fileName, string OrginalPath)
        {
            if (file != null)
            {
                if (!Directory.Exists(OrginalPath))
                {
                    Directory.CreateDirectory(OrginalPath);
                }

                string orginalpath = OrginalPath + fileName;

                using (var stream = new FileStream(orginalpath, FileMode.Create))
                {
                    if (!Directory.Exists(orginalpath))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
        }
    }
}

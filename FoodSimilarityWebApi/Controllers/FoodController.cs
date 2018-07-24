using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using FoodSimilarityWebApi.Common;
using FoodSimilarityWebApi.Models;

namespace FoodSimilarityWebApi.Controllers
{
    public class FoodController : ApiController
    {
        [HttpPost]
        public FoodSimResult Get()
        {
            FoodSimResult result = new FoodSimResult();

            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Files["file1"];
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExt = Path.GetExtension(fileName);
                    if (fileExt == ".jpg")
                    {
                        string dir = "/Web/Test/ImageUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));
                        }
                        string newfileName = Guid.NewGuid().ToString();
                        string fullDir = dir + newfileName + fileExt;
                        file.SaveAs(HttpContext.Current.Server.MapPath(fullDir));

                        result = CalcSimiliarlity.CalcSim(HttpContext.Current.Server.MapPath(fullDir));
                    }
                }

            }

            return  result;
        }
    }
}
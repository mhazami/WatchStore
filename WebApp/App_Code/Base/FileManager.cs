using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ClockStore.DTO.DBContext;


namespace WebApp.App_Code.Base
{
    public class FileManager
    {
         

        public static string ImageHandler(Guid fileId)
        {
            ClockStoreContext db = new ClockStoreContext();
            var file = db.File.FirstOrDefault(x => x.FileId == fileId);
            if (file != null)
            {
                var base64 = Convert.ToBase64String(file.Context);
                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                return imagesrc;
                //return File(model.Context, "image/jpg");
            }
            return string.Empty;
        }

        public static FileResult VideoHandler(Guid fileId)
        {
            ClockStoreContext db = new ClockStoreContext();
            var file = db.File.FirstOrDefault(x => x.FileId == fileId);
            if (file != null)
            {
                FileContentResult res = new FileContentResult(file.Context,file.ContextType);
                res.FileDownloadName = file.Title;
                return res;
            }
            return null;
        }
    }
}
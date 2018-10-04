using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            }
            return string.Empty;
        }
    }
}
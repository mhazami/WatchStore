using ClockStore.BLL.Base;
using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClockStore.BLL
{
    public class FileBO:BaseBO<File>
    {
        public File Insert(HttpPostedFileBase image)
        {
            var file = new File();
            file.FileId = Guid.NewGuid();
            if (image != null)
            {
                file.Context = new byte[image.ContentLength];
                image.InputStream.Read(file.Context, 0, image.ContentLength);
                file.ContextType = image.ContentType;
                file.Title = image.FileName;
            }
            var d = new ClockStoreContext();
            d.File.Add(file);

            d.SaveChanges();
            return file;
        }

        public File NewsInsert(HttpPostedFileBase image)
        {
            var file = new File();
            file.FileId = Guid.NewGuid();
            if (image != null)
            {
                file.Context = new byte[image.ContentLength];
                image.InputStream.Read(file.Context, 0, image.ContentLength);
                file.ContextType = image.ContentType;
                file.Title = image.FileName;
            }
            var d = new ClockStoreContext();
            d.File.Add(file);

           
            return file;
        }

        public bool UpDate(Guid id, HttpPostedFileBase image)
        {
            var file = new ClockStoreContext().File.Find(id);
            if (file != null)
            {
                file.Context = new byte[image.ContentLength];
                image.InputStream.Read(file.Context, 0, image.ContentLength);
                file.ContextType = image.ContentType;
                file.Title = image.FileName;
                //new ClockStoreContext().Entry(file).State = EntityState.Modified;
                //return new ClockStoreContext().SaveChanges() > 0;
                
                base.Update(file);
                base.Dispose();
            }
            return false;
        }


     

        public bool UpDate(File file)
        {

            new ClockStoreContext().Entry(file).State = EntityState.Modified;
            return true;
        }

        public bool Delete(Guid id)
        {
            var db = new ClockStoreContext();
            var file = new ClockStoreContext().File.Find(id);
            db.Entry(file).State = EntityState.Deleted;
            db.File.Remove(file);
            return db.SaveChanges() > 0;

        }




    }
}

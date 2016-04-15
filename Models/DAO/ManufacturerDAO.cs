using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ViewModels;

namespace Models.DAO
{
   public class ManufacturerDAO
    {
       web_interiorEntities db = new web_interiorEntities();
       public List<ManufacturerModels> ListAll()
       {
           using (web_interiorEntities db = new web_interiorEntities())
           {
               var query = from pro in db.Manufacturers

                           select new ManufacturerModels()
                           {
                               Id = pro.Id,
                               Name = pro.Name,
                               Address = pro.Address,
                               Logo = pro.Logo,
                               PhoneNumber = pro.PhoneNumber,
                               TaxNumber = pro.TaxNumber

                           };


               return query.ToList();
           }

       }
    }
}

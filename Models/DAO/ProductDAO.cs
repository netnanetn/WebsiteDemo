using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ViewModels;
namespace Models.DAO
{
  public class ProductDAO
    {
      web_interiorEntities db = new web_interiorEntities();
      public List<ProductModels> ListAllProduct()
      {
          using (web_interiorEntities db = new web_interiorEntities())
          {
              var query = from pro in db.Products
                          from cate in pro.Categories
                          join manu in db.Manufacturers on pro.ManufacturerId equals manu.Id
                          where 1 == 1
                          //from manu in pro.Manufacturers
                          select new ProductModels()
                          {
                              Id = pro.Id,
                              Image = pro.Image,
                              Name = pro.Name,
                              Description = pro.Description,
                              ProductKind = cate.Name,
                              ProductManu = manu.Name

                          };


              return query.ToList();
          }

      }
    }
}

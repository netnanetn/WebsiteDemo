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
      public List<ProductModels> ListAllProduct(string sortOrder,string currentFilter,string searchString,int? page)
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
       
              switch (sortOrder)
              {
                  case "name_desc":
                      query = query.OrderByDescending(s => s.Name);
                      break;
                  case "Date":
                      query = query.OrderBy(s => s.CreateOn);
                      break;
                  case "date_desc":
                      query = query.OrderByDescending(s => s.ProductManu);
                      break;
                  default:
                      query = query.OrderBy(s => s.ProductKind);
                      break;
              }
              if (!String.IsNullOrEmpty(searchString))
              {
                  query = query.Where(s => s.Name.Contains(searchString)
                                         || s.ProductKind.Contains(searchString));
              }
             
              return query.ToList();
          

      }
      public void CreateNewProducts(ProductModels product,string pathimg,List<String> ListImg)
      {
          Product createProduct = new Product();
          createProduct.Id = product.Id;
          createProduct.Name = product.Name;
          createProduct.Code = product.Code;
          createProduct.Description = product.Description;
          createProduct.Alias = product.Alias;
          createProduct.Image = "/Upload/ProductImg/" + pathimg;
          createProduct.CreateOn = DateTime.Now;
          createProduct.Price = product.Price;
          createProduct.SalePrice = product.SalePrice;
          createProduct.Barcode = product.Barcode;
          createProduct.Size = product.Size;
          createProduct.Unit = product.Unit;
          createProduct.StockStatus = product.StockStatus;
          createProduct.Available = product.Available;
          createProduct.Material = product.Material;
          createProduct.ManufacturerId = product.ManufacturerId;
          createProduct.CategorieId = product.CategorieId;

          var cate = db.Categories.Find(product.CategorieId);
          createProduct.Categories.Add(cate);
          db.Products.Add(createProduct);
          var findProduct = db.Products.Find(product.Id);
          for (int i = 0; i < ListImg.Count; i++)
          {
              ProductImage proImg = new ProductImage();
              proImg.ProductId = findProduct.Id;
              proImg.Img = "/Upload/ProductImg/" + ListImg[i];
              db.ProductImages.Add(proImg);
              db.SaveChanges();
          }

          db.SaveChanges();
          


      }
      public ProductModels EditProduct(int id)
      {
          Product product = db.Products.Find(id);

          ProductModels productModels = new ProductModels();
          productModels.Id = product.Id;
          productModels.Name = product.Name;
          productModels.Alias = product.Alias;
          productModels.Image = product.Image;
          productModels.Price = product.Price;
          productModels.SalePrice = product.SalePrice;
          productModels.Barcode = product.Barcode;
          productModels.CategorieId = product.CategorieId;
          productModels.StockStatus = product.StockStatus;
          productModels.Available = product.Available;
          productModels.Material = product.Material;
          productModels.Size = product.Size;
          productModels.Code = product.Code;
          productModels.Description = product.Description;
          productModels.Unit = product.Unit;

          productModels.ListCategories = ListCategories();
          productModels.ListManufacturers = ListManufacturer();
          return productModels;
      }
      public List<ManufacturerModels> ListManufacturer()
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
      public List<AllCategoriesModels> ListCategories()
      {
          using (web_interiorEntities db = new web_interiorEntities())
          {
              var query = from pro in db.Categories

                          select new AllCategoriesModels()
                          {
                              Id = pro.Id,
                              Name = pro.Name,


                          };

              return query.ToList();
          }

      }
  }
}

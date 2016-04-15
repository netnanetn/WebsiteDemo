using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;
using Models.EF;
using System.Data.Entity;
namespace Models.DAO
{

   public class CategoryDAO
    {
      
        web_interiorEntities db = new web_interiorEntities();
        //public CategoryDAO()
        //{
        //    db = new web_interiorEntities();
        //}
        public List<AllCategoriesModels> ListAllCategory()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Categories

                            select new AllCategoriesModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                                Description = pro.Description,
                                Image = pro.Image
                            };

                return query.ToList();
            }


        }
        public Category CreateCategory(Category category,string img)
        {

            Category savecategory = new Category();
            savecategory.Id = category.Id;
            savecategory.Name = category.Name;
            savecategory.Code = category.Code;
            savecategory.Description = category.Description;
            savecategory.Alias = category.Alias;
            savecategory.Image = img;
            savecategory.CreateOn = DateTime.Now;

            db.Categories.Add(savecategory);
            db.SaveChanges();

            return savecategory;
        }
        public Category EditCategory(int id) {

            Category cate = db.Categories.Find(id);

            return cate;
        }
        public void SaveEditCategory(Category cate) 
        {
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteCategory(int id) 
        {
            Category cate = db.Categories.Find(id);
            db.Categories.Remove(cate);
            db.SaveChanges();
        
        }
        public Category FindCategory(int id)
        {
            Category category = db.Categories.Find(id);
            return category;
        }
    }
}

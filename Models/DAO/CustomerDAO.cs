using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{

    public class CustomerDAO
    {
        web_interiorEntities db = new web_interiorEntities();
        public List<AllCustomersModels> ListAll()
        {
            AllCustomersModels kaka = new AllCustomersModels();
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = (from pro in db.Users
                             from order in pro.Orders
                             orderby order.CreateOn ascending
                             //join order in db.Orders on pro.Id equals order.UserId
                             //orderby or.CreateOn descending
                             where 1 == 1
                             select new AllCustomersModels()
                             {
                                 Id = pro.Id,
                                 Name = pro.Name,
                                 Email = pro.Email,
                                 TotalCost = order.TotalPrice,
                                 NumberOrder = pro.Orders.Count,
                                 OrderRecently = order.Name
                                 
                             });


                return query.ToList();
            }


        }
        public void CreateCustomer(AllCustomersModels customer)
        {
            User user = new User();
            user.Name = customer.Name;
            user.Address = customer.Address;
            user.PhoneNumber = customer.PhoneNumber;
            var cate = db.Roles.Find(customer.Id);
            user.Roles.Add(cate);
            db.Users.Add(user);
            db.SaveChanges();
        }

    }
}

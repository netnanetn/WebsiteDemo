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
        public List<AllCustomersModels> ListAllCustomer()
        {
                //var query = (from pro in db.Users
                //             from order in pro.Orders
                //             orderby order.CreateOn ascending
                //             //join order in db.Orders on pro.Id equals order.UserId
                //             //orderby or.CreateOn descending
                //             where 1 == 1
                //             select new AllCustomersModels()
                //             {
                //                 Id = pro.Id,
                //                 Name = pro.Name,
                //                 Email = pro.Email,
                //                 TotalCost = order.TotalPrice,
                //                 NumberOrder = pro.Orders.Count,
                //                 OrderRecently = order.Name
                                 
                //             });

                             var c=from pro in db.Users
                                 select new AllCustomersModels()
                             {
                                 Id = pro.Id,
                                 Name = pro.Name,
                                 Email = pro.Email,
                                 //TotalCost = order.TotalPrice,
                                 NumberOrder = pro.Orders.Count,
                               //  OrderRecently = recentlyOrder(Int32.Parse(pro.Id.ToString()))
                                 
                             };

                          
                                 foreach (var item in c)
                                 {
                                  
                                     item.OrderRecently = recentlyOrder(item.Id);
                                     item.Id = 1;
                                     item.Name = "kk";
                                 }

                             

                return c.ToList();
            


        }
   
        public string recentlyOrder(int id)
        {
            
                    var s = (from p in db.Orders
                             where p.UserId == id
                             orderby p.CreateOn descending
                             select p.Name).Skip(0).Take(1);


                    return s.ToString();
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

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
                                 TotalCost = pro.Orders.Sum(o=>o.TotalPrice),
                                 NumberOrder = pro.Orders.Count,
                               // OrderRecently = recentlyOrder(Int32.Parse(pro.Id.ToString()))
                                 
                             };

                             List<AllCustomersModels> kaka=new List<AllCustomersModels>();
                                 foreach (AllCustomersModels item in c)
                                 {
                                     AllCustomersModels savecustomer = new AllCustomersModels();
                                   
                                   String s = recentlyOrder(item.Id);
                                     savecustomer.Id = item.Id;
                                     savecustomer.Name = item.Name;
                                     
                                    
                                     savecustomer.NumberOrder = item.NumberOrder;
                                     if (item.NumberOrder > 0)
                                     {
                                         savecustomer.OrderRecently = s.ToString();

                                     }
                                     else
                                     {
                                         savecustomer.OrderRecently = "NoOrder";
                                     }
                                     savecustomer.Email = item.Email;
                                     savecustomer.PhoneNumber = "";
                                     savecustomer.PassWordHash = "";
                                     savecustomer.Address = "";
                                     savecustomer.TotalCost = totalCost(item.Id);
                                     kaka.Add(savecustomer);
                                     
                                 }                       
                return kaka;
        }
   
   
        public string recentlyOrder(int id)
        {
            
                    var s = (from p in db.Orders
                             where p.UserId == id
                             orderby p.CreateOn descending
                             select new { p.Name, p.UserId}
                             
                             ).Skip(0).Take(1);
                    foreach (var k in s)
                    {
                      
                        return k.Name;
                    }

                    return s.ToString();
        }
        public List<AllOrderModels> ListOrder(int id)
        {
            var s = from order in db.Orders
                    where order.UserId == id
                    select new AllOrderModels
                    {
                        Id = order.Id,
                        CreateOn = order.CreateOn,
                        PaymentStatus = order.PaymentStatus,
                        ShippingStatus = order.ShippingStatus,
                        TotalPrice = order.TotalPrice,
                        Name = order.Name,
                        UserAddress = order.UserAddress,
                        UserPhoneNumber = order.UserPhoneNumber,
                        UserName=order.UserName,
                    };
            return s.ToList();


        }
        public decimal? totalCost(int id) {
            decimal? total=0;
            var s = from p in db.Orders
                     where p.UserId == id
                     select new AllOrderModels 
                     {
                         Name=p.Name,
                         ShippingStatus=p.ShippingStatus,
                         PaymentStatus=p.PaymentStatus,
                         TotalPrice=p.TotalPrice,
                     };
            foreach (var item in s)
            {
                if (item.PaymentStatus == true)
                {
                    total += item.TotalPrice;
                }
            }
            return total;

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

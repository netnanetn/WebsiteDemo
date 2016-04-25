using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
   public class OrderDAO
    {
        web_interiorEntities db = new web_interiorEntities();
        public List<OrderDetailModel> ListProductOnOrderDetails(int id)
        {
            var s = from d in db.OrderDetails
                    where d.OrderId == id
                    select new OrderDetailModel
                    {
                        ProductName = d.ProductName,
                        Number = d.Number,
                        ProductPrice = d.ProductPrice,
                        PriceItem = d.PriceItem,
                        Id = d.Id,
                        ProductCode = d.ProductCode,
                        CreateOn = d.CreateOn,
                        ProductSize = d.ProductSize,
                        ModifiedOn = d.ModifiedOn,
                        OrderId = id

                    };
            return s.ToList();
        }
        public List<AllOrderModels> ListAllOrder(string sortOrder, string currentFilter, string searchString, int? page)
        {
                var query = from pro in db.Orders

                            select new AllOrderModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                                CreateOn = pro.CreateOn,
                                UserName = pro.UserName,
                                PaymentStatus = pro.PaymentStatus.Value,
                                ShippingStatus = pro.ShippingStatus,
                                TotalPrice = pro.TotalPrice,


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
                        query = query.OrderByDescending(s => s.CreateOn);
                        break;
                    default:
                        query = query.OrderBy(s => s.UserName);
                        break;
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(s => s.Name.Contains(searchString)
                                           || s.UserName.Contains(searchString));
                }
                return query.ToList();
        }




    }
}

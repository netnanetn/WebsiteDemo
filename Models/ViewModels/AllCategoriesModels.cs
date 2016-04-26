using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModels
{
   public class AllCategoriesModels
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
          [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}

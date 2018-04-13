using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class CarModelDataView
    {
        public int Id { get; set; }

        public CarCompanyDataView CarCompany { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal BasePrice { get; set; }
        public string ImagePath { get; set; }
    }
}

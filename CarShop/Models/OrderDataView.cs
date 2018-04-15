using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class OrderDataView
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public CarModelDataView CarModel { get; set; }
        public List<CarFeatureDataView> CarFeatures { get; set; }
    }
}

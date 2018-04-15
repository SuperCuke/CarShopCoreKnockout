using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services.Models
{
    public class CarOrderData
    {
        public int CarModelId { get; set; }
        public List<int> SelectedFeaturesIds { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}

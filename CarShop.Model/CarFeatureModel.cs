using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Model
{
  public  class CarFeatureModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public virtual CarFeature CarFeature { get; set; }
        public virtual CarModel CarModel { get; set; }
    }
}

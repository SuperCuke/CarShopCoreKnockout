using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Model
{
   public class CarOrderFeature
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual CarOrder CarOrder { get; set; }
        public int FeatureModelId { get; set; }
        public virtual CarFeatureModel CarFeatureModel { get; set; }
    }
}

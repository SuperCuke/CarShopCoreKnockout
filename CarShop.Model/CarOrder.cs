using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Model
{
    public class CarOrder
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string SecureCode { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }

        public virtual CarModel CarModel { get; set; }
        public virtual ICollection<CarOrderFeature> CarFeatures { get; set; }
    }
}

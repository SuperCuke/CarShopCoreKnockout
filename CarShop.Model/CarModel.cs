using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Model
{
    public class CarModel
    {
        public int Id { get; set; }
        public virtual CarCompany CarCompany  { get;set;}

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string ShortDescription { get; set; }
        [MaxLength(1000)]
        public string LongDescription { get; set; }

        public decimal BasePrice { get; set; }
        [MaxLength(100)]
        public string ImagePath { get; set; }
    }
}

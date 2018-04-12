using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Model
{
    public class CarFeature
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }

    public enum CarFeatureType : int
    {
        EnginePerfomance,

    }
}

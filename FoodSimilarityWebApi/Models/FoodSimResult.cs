using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodSimilarityWebApi.Models
{
    public class FoodSimResult
    {
        public string Name { get; set; }

        public double Similarity { get; set; }

    }
}
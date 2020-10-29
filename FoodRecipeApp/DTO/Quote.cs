using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class Quote
    {
        public string QuoteString { get; set; }

        public Quote(DataRow row)
        {
            QuoteString = row["Quote"].ToString();
        }
    }
}

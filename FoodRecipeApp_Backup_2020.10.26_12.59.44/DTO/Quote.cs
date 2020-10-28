using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    class Quote
    {
        public List<string> Quotes { get; set; }

        public string getQuoteFromRow(DataRow row)
        {
            return row["Quote"].ToString();
        }

        public Quote()
        {
            Quotes = new List<string>();
            DataTable allQuotes = QuoteDAO.Instance.getAllQuotes();
            foreach(DataRow row in allQuotes.Rows)
            {
                Quotes.Add(getQuoteFromRow(row));
            }
        }
    }
}

using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class QuotesCollection : System.Collections.ObjectModel.ObservableCollection<Quote>
    {
        public static QuotesCollection GetQuotes()
        {
            QuotesCollection quotes = new QuotesCollection();
            DataTable allQuotes = QuoteDAO.Instance.getAllQuotes();
            foreach (DataRow row in allQuotes.Rows)
            {
                Quote quote = new Quote(row);
                quotes.Add(quote);
            }
            return quotes;
        }
    }
}

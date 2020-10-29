using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DAO
{
    class QuoteDAO
    {
        private static QuoteDAO instance;

        public static QuoteDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuoteDAO();
                return instance;
            }
            private set => instance = value;
        }

        private QuoteDAO() { }

        public DataTable getAllQuotes()
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getAllQuotes");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DAO
{
    class StepDAO
    {
        private static StepDAO instance;

        public static StepDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new StepDAO();
                return instance;
            }
            private set => instance = value;
        }

        public StepDAO()
        {
        }

        public DataTable getAllStepsInDish (string dish)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_getAllStepsInDish @Dish ", new object[] { dish });
        }
    }
}

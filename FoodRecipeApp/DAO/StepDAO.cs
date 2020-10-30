using FoodRecipeApp.DTO;
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

        internal void addNewStep(Step step, int dishCode)
        {
            string Dish = dishCode.ToString();
            string StepNumber = step.StepNumber.ToString();
            string Description = step.Description;

            DataProvider.Instance.ExecuteQuery("EXEC USP_addNewStep @Dish , @StepNumber , @Description", new object[] { Dish, StepNumber, Description});
            Images.AddNewStepsToData(step.ListImage, StepNumber, dishCode);
        }
    }
}

using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class StepCollection: System.Collections.ObjectModel.ObservableCollection<Step>
	{
		public static StepCollection GetStepList(int DishCode)
		{
			var stepsList = new StepCollection();

			var dataSteps = StepDAO.Instance.getAllStepsInDish(DishCode.ToString());
			foreach (var step in from DataRow row in dataSteps.Rows
								 let imagesPerStep = ImagesPerStepDataSource.GetImagesPerStepCollection(DishCode, (int)row["StepNumber"])
								 let step = new Step((int)row["StepNumber"], row["Desrciption"].ToString(), imagesPerStep)
								 select step)
			{
				stepsList.Add(step);
			}

			return stepsList;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class StepDataSource
	{
		public static StepCollection GetStepsCollection(int DishCode)
		{
			var stepsCollection = StepCollection.GetStepList(DishCode);
			return stepsCollection;
		}
	}
}
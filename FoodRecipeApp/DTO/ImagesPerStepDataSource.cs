using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class ImagesPerStepDataSource
	{
		public static ImagesPerStepCollection GetImagesPerStepCollection(int DishCode, int StepNumber)
		{
			var _imagesPerStepCollection = ImagesPerStepCollection.GetImagesPerStep(DishCode, StepNumber);
			return _imagesPerStepCollection;
		}
	}
}

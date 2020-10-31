using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class ImagesPerStepCollection: System.Collections.ObjectModel.ObservableCollection<string>
	{
		public static string GetImagePath(DataRow row)
		{
			var result = $"{AppDomain.CurrentDomain.BaseDirectory}{row["FilePath"]}";
			return result;
		}

		public static ImagesPerStepCollection GetImagesPerStep(int DishCode, int StepNumber)
		{
			var images = new ImagesPerStepCollection();
			var dataImage = ImageDAO.Instance.getImagesStepDish(DishCode.ToString(), StepNumber.ToString());
			foreach (var imagePath in from DataRow row in dataImage.Rows
									  let imagePath = GetImagePath(row)
									  select imagePath)
			{
				images.Add(imagePath);
			}

			return images;
		}
	}
}

using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FoodRecipeApp.DTO
{
    public class Step
    {
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public List<string> ListImage { get; set; }

       public ObservableCollection<string> ImagesCollection { get; set; }

        public ImagesPerStepCollection ImageList { get; set; }


        public Step(int stepNumber, string description, List<string> filePaths)
        {
            StepNumber = stepNumber;
            Description = description;
            ListImage = filePaths;

           ImagesCollection = Images.GetImages(ListImage);
        }

        public Step(int stepNumber, string description, ImagesPerStepCollection imageList)
        {
            StepNumber = stepNumber;
            Description = description;
            ImageList = imageList;
        }

        public static Step CreateStep (int stepNumber, string description, List<string> filePaths)
		{
			return CreateNewStep(stepNumber, description, ref filePaths);
		}

		private static Step CreateNewStep(int stepNumber, string description, ref List<string> filePaths)
		{
			if (description == "\r\n"
				|| description == ""
				|| filePaths == null)
			{
				return null;
			}
			else
			{
				filePaths = Images.trimListFilePathImage(filePaths);
				return new Step(stepNumber, description, filePaths);
			}
		}

		public static List<Step> getAllStepsInDish (int dish)
        {
            List<Step> steps = new List<Step>();
            
            DataTable allSteps = StepDAO.Instance.getAllStepsInDish(dish.ToString());
            foreach(DataRow row in allSteps.Rows)
            {
                List<string> filePaths = Images.getAllImagesInStep(dish, (int)row["StepNumber"]);
                Step step = new Step( (int)row["StepNumber"], row["Desrciption"].ToString(), filePaths);
                steps.Add(step);
            }

            return steps;
        }

        internal static void AddNewStepsToData(List<Step> steps, int dishCode)
        {
            foreach(Step step in steps)
            {
                StepDAO.Instance.addNewStep(step, dishCode);
            }
        }
    }
}

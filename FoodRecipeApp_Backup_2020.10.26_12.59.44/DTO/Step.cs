﻿using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class Step
    {
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public List<string> ListImage { get; set; }

        public Step(int stepNumber, string description, List<string> filePaths)
        {
            StepNumber = stepNumber;
            Description = description;
            ListImage = filePaths;
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
    }
}

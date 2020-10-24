using FoodRecipeApp.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
    public class Dish: INotifyPropertyChanged
    {
        public int DishCode { get; set; }
        public bool IsLove { get; set; }
        public string Name { get; set; }
        public string Video { get; set; }
        public string Loai { get; set; }
        public string ImagePath { get; set; }
        public List<Step> Steps { get; set; }

        public Dish(DataRow row)
        {
            DishCode = (int)row["Dish"];
            //IsLove = ((int)row["Love"] )== 1 ? true : false;
            IsLove = (bool)row["Love"];
            Name = row["Name"].ToString();
            Video = row["Video"].ToString();
            Loai = row["Loai"].ToString();
            ImagePath = Images.getFilePath(row);
            Steps = Step.getAllStepsInDish(DishCode);
        }

        public static List<Dish> getAllDish()
        {
            List<Dish> dishes = new List<Dish>();
            DataTable data = DishDAO.Instance.getAllDishes();
            foreach ( DataRow row  in data.Rows)
            {
                Dish dish = new Dish(row);
                dishes.Add(dish);
            }
            return dishes;
        }

        public static ObservableCollection<Dish> GetDishes()
        {
            ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();
            DataTable data = DishDAO.Instance.getAllDishes();
            foreach (DataRow row in data.Rows)
            {
                Dish dish = new Dish(row);
                dishes.Add(dish);
            }
            return dishes;
        }

       /* IEnumerable<Dish> _dishs;
        public IEnumerable<Dish> Dishs
        {
            get
            {
                if (Dishs == null)
                {
                    _dishs = this.Northwind.DishsCollection;
                }

                return Dishs;
            }
        }*/
    }
}

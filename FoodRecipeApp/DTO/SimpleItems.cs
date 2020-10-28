using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using System.Collections.Generic;

namespace FoodRecipeApp.DTO
{
    class SimpleItems : ViewModelBase
    {
        public string Name
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        public static IEnumerable<SimpleItems> Generate(int count)
        {
            List<SimpleItems> items = new List<SimpleItems>();

            for (int i = 0; i < count; i++)
            {
                items.Add(new SimpleItems()
                {
                    Name = i.ToString(), //Thay i = ten san pham
                    Image = images[i]   // Thay i = thu tu link anh
                });
            }

            return items;
        }

        private static List<string> images = new List<string>
        {
            "/FoodRecipeApp;component/Images/TileView/People/pic1.png",
        };
    }
}

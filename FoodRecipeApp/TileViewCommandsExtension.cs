using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodRecipeApp
{
    public static class TileViewCommandsExtension
    {
        static TileViewCommandsExtension()
        {
            TileViewCommandsExtension.Delete = new RoutedUICommand("Xóa khỏi danh sách", "Delete", typeof(TileViewCommandsExtension));
            TileViewCommandsExtension.AddNewFavouriteRecipe = new RoutedUICommand("Thêm vào danh sách yêu thích", "Add To Favourites", typeof(TileViewCommandsExtension));
        }

        public static RoutedUICommand Delete { get; private set; }
        public static RoutedUICommand AddNewFavouriteRecipe { get; private set; }
    }
}

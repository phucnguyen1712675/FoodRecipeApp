using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace FoodRecipeApp.FilteringHelperClasses
{
    public class MyHighlightBehavior : HighlightBehavior
    {
        public override int FindHighlightedIndex(string searchText, System.Collections.IList filteredItems, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
        {
            var items = filteredItems.OfType<Dish>().ToList<Dish>();
            if (items != null)
            {
                if (items.Any(x => x.Name == searchText))
                {
                    // there is an exact match 
                    var matchedItem = items.First(x => x.Name == searchText);
                    // return the index of the matched item 
                    return items.IndexOf(matchedItem);
                }
            }
            // there isn't exact match 
            // return the index of the last item from the filtered items  
            return items.Count - 1;
        }
    }
}

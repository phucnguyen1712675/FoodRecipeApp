using FoodRecipeApp.DTO;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using System.Linq.Dynamic;

namespace FoodRecipeApp.FilteringHelperClasses
{
    public class MyCustomFilteringBehavior : FilteringBehavior
    {
        public static object GetPropValue(object src, string propName) => src.GetType().GetProperty(propName).GetValue(src, null);

        private string ModifyItem(object originalItem, string textSearchPath)
        {
            var oldValueForProperty = GetPropValue(originalItem, textSearchPath).ToString();
            var newValueForProperty = Dish.RemoveDiacritics(oldValueForProperty).ToLower();
            return newValueForProperty;
        }

        /*        public string RemoveDiacritics(string text) => string.Concat(text.Normalize(NormalizationForm.FormD)
                                                                                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
                                                                                .Normalize(NormalizationForm.FormC);*/
        // giữ cho chắc :v cấm delete nha
        /*        public override IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
                {
                    searchText = this.RemoveDiacritics(searchText.ToUpper());

                    // Create key-value pair for each item in the itemsSource (you can cache this collection if it is not changed dynamically
                    // Use the key-value pair later when you have to return the original item.
                    var modifiedItems = new Dictionary<object, string>();
                    if (textSearchPath != null && textSearchPath != string.Empty)
                    {
                        foreach (object originalItem in items)
                        {
                            var modifiedItem = this.ModifyItem(originalItem, textSearchPath);
                            modifiedItems.Add(originalItem, modifiedItem);
                        }
                    }
                    else
                    {
                        foreach (object originalItem in items)
                        {
                            var modifiedItem = this.RemoveDiacritics(originalItem.ToString().ToUpper());
                            modifiedItems.Add(originalItem, modifiedItem);
                        }
                    }

                    // Get the matching items by sending the itemsSource without diacritics
                    var matchingItems = base.FindMatchingItems(searchText, modifiedItems.Values.ToList(), escapedItems, string.Empty, textSearchMode);

                    // return all Original matching items
                    var originalMatchingItems = new List<object>();
                    foreach (string matchItem in matchingItems)
                    {
                        var dictionaryItem = modifiedItems.FirstOrDefault(p => p.Value == matchItem);
                        modifiedItems.Remove(dictionaryItem.Key);
                        originalMatchingItems.Add(dictionaryItem.Key);
                    }

                    if (string.IsNullOrEmpty(searchText) || !originalMatchingItems.Any())
                    {
                        return ((IEnumerable<object>)items).Where(x => !escapedItems.Contains(x));
                    }

                    return originalMatchingItems;
                }
        */
        public override IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
        {
            if (string.IsNullOrEmpty(searchText.TrimStart()))
            {
                return ((IEnumerable<object>)items).Where(x => !escapedItems.Contains(x));
            }

            var resultItems = new List<object>();

            searchText = Dish.RemoveDiacritics(searchText);
            string queryStr = Dish.CreateQueryLinQ(searchText, "item");

            // Create key-value pair for each item in the itemsSource (you can cache this collection if it is not changed dynamically
            // Use the key-value pair later when you have to return the original item.
            var modifiedItems = new Dictionary<object, string>();
            if (textSearchPath != null && textSearchPath != string.Empty)
            {
                foreach (var (originalItem, modifiedItem) in from object originalItem in items
                                                             let modifiedItem = this.ModifyItem(originalItem, textSearchPath)
                                                             select (originalItem, modifiedItem))
                {
                    modifiedItems.Add(originalItem, modifiedItem);
                }
            }
            else
            {
                foreach (var (originalItem, modifiedItem) in from object originalItem in items
                                                             let modifiedItem = Dish.RemoveDiacritics(originalItem.ToString().ToUpper())
                                                             select (originalItem, modifiedItem))
                {
                    modifiedItems.Add(originalItem, modifiedItem);
                }
            }

            List<string> matchItems = new List<string>();
            if (Dish.checkQuery(queryStr))
                matchItems = modifiedItems.Values.ToList().WhereDynamic(item => queryStr).ToList();

            foreach (string matchItem in matchItems)
            {
                var dictionaryItem = modifiedItems.FirstOrDefault(p => p.Value == matchItem);
                modifiedItems.Remove(dictionaryItem.Key);
                resultItems.Add(dictionaryItem.Key);
            }

            return resultItems;
        }
    }
}

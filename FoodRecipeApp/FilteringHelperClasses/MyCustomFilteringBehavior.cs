using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace FoodRecipeApp.FilteringHelperClasses
{
    public class MyCustomFilteringBehavior : FilteringBehavior
    {
        public static object GetPropValue(object src, string propName) => src.GetType().GetProperty(propName).GetValue(src, null);

        private string ModifyItem(object originalItem, string textSearchPath)
        {
            var oldValueForProperty = GetPropValue(originalItem, textSearchPath).ToString();
            var newValueForProperty = this.RemoveDiacritics(oldValueForProperty);
            return newValueForProperty;
        }

        public string RemoveDiacritics(string text) => string.Concat(text.Normalize(NormalizationForm.FormD)
                                                                        .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
                                                                        .Normalize(NormalizationForm.FormC);

        public override IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
        {
            searchText = this.RemoveDiacritics(searchText.ToUpper());

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
                                                             let modifiedItem = this.RemoveDiacritics(originalItem.ToString().ToUpper())
                                                             select (originalItem, modifiedItem))
                {
                    modifiedItems.Add(originalItem, modifiedItem);
                }
            }

            // Get the matching items by sending the itemsSource without diacritics
            var matchingItems = base.FindMatchingItems(searchText, modifiedItems.Values.ToList(), escapedItems, string.Empty, textSearchMode);

            // return all Original matching items
            var originalMatchingItems = new List<object>();
            foreach (var dictionaryItem in from string matchItem in matchingItems
                                           let dictionaryItem = modifiedItems.FirstOrDefault(p => p.Value == matchItem)
                                           select dictionaryItem)
            {
                modifiedItems.Remove(dictionaryItem.Key);
                originalMatchingItems.Add(dictionaryItem.Key);
            }

            return originalMatchingItems;

            //return string.IsNullOrEmpty(searchText) || !originalMatchingItems.Any()
            //    ? ((IEnumerable<object>)items).Where(x => !escapedItems.Contains(x))
            //    : originalMatchingItems;
        }
    }
}

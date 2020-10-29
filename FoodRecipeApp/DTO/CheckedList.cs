using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.FieldList;

namespace FoodRecipeApp.DTO
{
    class CheckedList
    {
        List<System.Windows.Controls.CheckBox> _isCheckedList;
       
        public CheckedList() 
        {
            _isCheckedList = new List<System.Windows.Controls.CheckBox>();
        }
        public CheckedList(List<CheckBox> listCheckbox)
        {
            _isCheckedList = listCheckbox;
        }
        public void AddCheckBoxes(System.Windows.Controls.CheckBox checkBox) 
        {
            try
            {
                _isCheckedList.Add(checkBox);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Already checkbox");
                //Do something
            }
        }
        public int UncheckAll()
        {
            foreach(var index in _isCheckedList)
            {
                index.IsChecked = false;
            }
            return _isCheckedList.Count;
        }

        public  string GetFilterQuery()
        {
            string Filter= null;
            foreach(var index in _isCheckedList)
            {
                if( index.IsChecked== true)
                {
                    if (Filter == null)
                    {
                        Filter = Filter + index.Content;
                    }
                    else
                    {
                        Filter = Filter + "," + index.Content;
                    }
                }
                else
                { 
                    //do something
                }
            }
            return Filter;
        }




        
    }
}

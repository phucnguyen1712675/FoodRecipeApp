using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.ViewModels
{
	public class NasdaqViewModel : INotifyPropertyChanged
	{
		public NasdaqViewModel()
		{
			this._displayValue = 3498;
		}

		private double _displayValue;
		public double DisplayValue
		{
			get => this._displayValue;
			set
			{
				if (this._displayValue != value)
				{
					this._displayValue = value;
					this.OnPropertyChanged("DisplayValue");
				}
			}
		}

		public void UpdateDisplayValue()
		{
			if (this.DisplayValue == 3498)
			{
				this.DisplayValue = 3470;
			}
			else
			{
				this.DisplayValue = (int)3498;
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}

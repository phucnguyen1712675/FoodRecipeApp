using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace FoodRecipeApp.ViewModels
{
	public class RecipeViewModel
	{
		private ObservableCollection<Dish> _recipes;
		public ObservableCollection<Dish> Recipes
		{
			get
			{
				if (this._recipes != null)
				{
					this._recipes = Dish.GetDishes();
				}
				return this._recipes;
			}
		}

        IEnumerable<TileReorderMode> reorderModes;

        public IEnumerable<TileReorderMode> ReorderModes
        {
            get
            {
                if (this.reorderModes == null)
                {
                    this.reorderModes = new List<TileReorderMode>()
                    {
                        TileReorderMode.BetweenGroups,
                        TileReorderMode.InGroup,
                        TileReorderMode.None
                    };
                }

                return this.reorderModes;
            }
        }

        IEnumerable<Visibility> visibilityModes;

        public IEnumerable<Visibility> VisibilityModes
        {
            get
            {
                if (this.visibilityModes == null)
                {
                    this.visibilityModes = new List<Visibility>()
                    {
                        Visibility.Visible,
                        Visibility.Collapsed
                    };
                }

                return this.visibilityModes;
            }
        }

        IEnumerable<SelectionMouseButton> mouseButtonSelectionModes;

        public IEnumerable<SelectionMouseButton> MouseButtonSelectionModes
        {
            get
            {
                if (this.mouseButtonSelectionModes == null)
                {
                    this.mouseButtonSelectionModes = new List<SelectionMouseButton>()
                    {
                        SelectionMouseButton.Left,
                        SelectionMouseButton.Right,
                        SelectionMouseButton.Left | SelectionMouseButton.Right
                    };
                }

                return this.mouseButtonSelectionModes;
            }
        }

        IEnumerable<VerticalAlignment> verticalTilesAlignments;

        public IEnumerable<VerticalAlignment> VerticalTilesAlignments
        {
            get
            {
                if (this.verticalTilesAlignments == null)
                {
                    this.verticalTilesAlignments = new List<VerticalAlignment>()
                    {
                        VerticalAlignment.Center,
                        VerticalAlignment.Stretch,
                         VerticalAlignment.Top,
                        VerticalAlignment.Bottom
                    };
                }

                return this.verticalTilesAlignments;
            }
        }
    }
}

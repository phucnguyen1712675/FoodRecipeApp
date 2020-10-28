using FoodRecipeApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoodRecipeApp.ViewModels
{
	public class RecipeViewModel
	{
        public RecipeViewModel() { }

        private ObservableCollection<Dish> _recipes;
		public ObservableCollection<Dish> Recipes
		{
			get
			{
				if (this._recipes == null)
				{
					this._recipes = Dish.GetDishes();
				}
				return this._recipes;
			}
		}

        private ObservableCollection<string> _recipeTypes;
        public ObservableCollection<string> RecipeTypes
		{
            get
            {
                if (this._recipes != null)
                {
                    IEnumerable<Dish> dishesCollection = (IEnumerable<Dish>)this._recipes;
                    var dishesList = new List<Dish>(dishesCollection);
                    var dishTypesList = new List<string>();
                    var tempList = new List<string>();
                    var delimiterChars = ',';
                    foreach (var dish in dishesList)
					{
                        tempList = dish.Loai.Split(delimiterChars).ToList();
                        foreach (var type in tempList)
						{
                            var temp = type.ToLowerInvariant();
                            dishTypesList.Add(char.ToUpper(temp[0]) + temp.Substring(1));
                        }
                        tempList.Clear();
					}
                    this._recipeTypes = (ObservableCollection<string>)dishTypesList.Select(x => x).Distinct();
                }
                return this._recipeTypes;
            }
        }

        private IEnumerable<Dish> randomProducts;
        private IEnumerable<Dish> largeRandomProducts;

        /// <summary>
        /// Gets the random products.
        /// </summary>
        /// <value>The random products.</value>
        public IEnumerable<Dish> RandomProducts
        {
            get
            {
                if (this.randomProducts == null)
                {
                    this.randomProducts = new Dishes().GetData(20).ToList();
                }
                    
                return this.randomProducts;
            }
        }

        /// <summary>
        /// Gets the large random products.
        /// </summary>
        /// <value>The large random products.</value>
        public IEnumerable<Dish> LargeRandomProducts
        {
            get
            {
                if (this.largeRandomProducts == null)
                {
                    this.largeRandomProducts = new Dishes().GetData(1000000).ToList();
                }

                return this.largeRandomProducts;
            }
        }

        /*object[] _pagerDisplayModes;
        public object[] PagerDisplayModes
        {
            get
            {
                if (_pagerDisplayModes == null)
                {
                    _pagerDisplayModes = EnumHelper.GetValues(typeof(PagerDisplayModes));
                }

                return _pagerDisplayModes;
            }
        }

        object[] _autoEllipsisModes;
        public object[] AutoEllipsisModes
        {
            get
            {
                if (_autoEllipsisModes == null)
                {
                    _autoEllipsisModes = EnumHelper.GetValues(typeof(AutoEllipsisModes));
                }

                return _autoEllipsisModes;
            }
        }

        EndlessPagedCollectionView _view;
        public EndlessPagedCollectionView View
        {
            get
            {
                if (_view == null)
                {
                    _view = new EndlessPagedCollectionView();
                }

                return _view;
            }
        }*/
        /*IEnumerable<TileReorderMode> reorderModes;

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
        }*/
    }
}

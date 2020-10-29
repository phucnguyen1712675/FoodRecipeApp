using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.DTO
{
	public class QuotesDataSource
	{
		private static QuotesDataSource _instance = null;
		private QuotesCollection _quotesCollection;

		public static QuotesDataSource Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new QuotesDataSource();
				}
				return _instance;
			}
		}

		private QuotesDataSource()
		{
			this._quotesCollection = null;
		}

		public QuotesCollection QuotesCollection
		{
			get
			{
				if (this._quotesCollection == null)
				{
					this._quotesCollection = QuotesCollection.GetQuotes();
				}
				return this._quotesCollection;
			}
		}

		public string GetRandomQuote()
		{
			int count = QuotesCollection.Count;
			int index = MyRandom.Instance.Next(count);
			Quote newQuote = QuotesCollection[index];
			return newQuote.QuoteString;
		}
	}
}
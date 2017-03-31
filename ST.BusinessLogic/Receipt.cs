using ST.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.BusinessLogic
{
    public class Receipt
    {
        #region Fields
        private IList<IProduct> _listOfProducts;
        #endregion

        #region Properties
        /// <summary>
        /// The list of product.
        /// </summary>
        public IList<IProduct> ListOfProducts
        {
            get
            {
                return _listOfProducts ?? new List<IProduct>();
            }
            set
            {
                if (value != null && value.All(product => product != null))
                    _listOfProducts = value;
                else throw new ArgumentNullException($"List of product, or one of the items within it is null.");
            }
        }

        /// <summary>
        /// The total price of all product.
        /// </summary>
        public decimal Subtotal
        {
            get { return ListOfProducts.Sum(product => product.TotalPrice); }
        }

        /// <summary>
        /// The total tax of all product.
        /// </summary>
        public decimal SalesTax
        {
            get { return ListOfProducts.Sum(product => product.ProductTax.HasValue ? product.ProductTax.Value * product.Quantity : decimal.Zero); }
        }

        /// <summary>
        /// The total price of all product including tax based on the price and quantity.
        /// </summary>
        public decimal SubtotalIncTax
        {
            get { return ListOfProducts.Sum(product => product.TotalPriceIncTax); }
        }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        #region Constructor
        public Receipt()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="listOfProducts"></param>
        public Receipt(IList<IProduct> listOfProducts)
        {
            ListOfProducts = listOfProducts;
        }
        #endregion
    }
}

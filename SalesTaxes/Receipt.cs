using SalesTaxes.Interfaces;

namespace SalesTaxes
{
    /// <summary>
    /// Class that represents the receipt.
    /// </summary>
    public class Receipt : IReceipt
    {
        #region Fields

        private IProduct[] _products;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="products"></param>
        public Receipt(IProduct[] products)
        {
            Products = products;
        }

        #endregion

        #region Properies

        /// <summary>
        /// An array of products added.
        /// </summary>
        public IProduct[] Products
        {
            get
            {
                if (_products != null && _products.Length > 0)
                {
                    foreach (Product product in _products)
                    {
                        product.CalculateProductTax();
                    }
                }
                return _products;
            }
            set
            {
                if (value != null && value.Length > 0) _products = value;
            }
        }

        /// <summary>
        /// The total cost of all items including tax.
        /// </summary>
        public decimal Subtotal
        {
            get
            {
                return TaxCalculator.CalculateSubTotal(_products);
            }
        }

        /// <summary>
        /// The total amounts of sales taxes paid.
        /// </summary>
        public decimal SalesTaxes
        {
            get
            {
                return TaxCalculator.CalculateSalesTaxes(_products);
            }
        }

        #endregion
    }
}

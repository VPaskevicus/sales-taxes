using ST.BusinessLogic.Interfaces;
using ST.Common;
using System;
using System.Globalization;

namespace ST.BusinessLogic
{
    /// <summary>
    /// The standard product.
    /// </summary>
    public class Product : IProduct
    {
        #region Fields
        private string _title;
        private decimal _price;
        private int _quantity;
        private decimal? _productTax;
        #endregion

        #region Properties
        /// <summary>
        /// The product title.
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_title))
                    throw new ArgumentNullException(nameof(_title));
                return _title;
            }
            set
            {
                if (!string.IsNullOrEmpty(value)) _title = value;
                else throw new ArgumentNullException($"The product title cannot be null or empty.");
            }
        }

        /// <summary>
        /// The product price.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value >= 0) _price = Math.Truncate(100 * value) / 100;
                else throw new ArgumentOutOfRangeException($"The product price cannot be negative value.");
            }
        }

        /// <summary>
        /// The total price based on the quantity.
        /// </summary>
        public decimal TotalPrice => Price * Quantity;

        /// <summary>
        /// The product quantity.
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value > 0) _quantity = value;
                else throw new ArgumentOutOfRangeException($"The product quantity cannot be zero or negative.");
            }
        }

        /// <summary>
        /// The product type.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// The product tax based on the price.
        /// </summary>
        public decimal? ProductTax
        {
            get { return _productTax; }
            set
            {
                if (value >= 0) _productTax = value;
                else throw new ArgumentOutOfRangeException($"The product tax cannot be negative value.");
            }
        }

        /// <summary>
        /// The product price including tax.
        /// </summary>
        public decimal PriceIncTax => ProductTax.HasValue ? Price + ProductTax.Value : Price;

        /// <summary>
        /// The total price including tax based on the quantity.
        /// </summary>
        public decimal TotalPriceIncTax => ProductTax.HasValue ? (Price + ProductTax.Value) * Quantity : Price * Quantity;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Product()
        {
            Quantity = 1;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The product title.</param>
        /// <param name="price">The product price.</param>
        /// <param name="quantity">The product quantity.</param>
        /// <param name="productType">The product type.</param>
        public Product(string title, decimal price, int quantity, ProductType productType)
        {
            Title = title;
            Price = price;
            Quantity = quantity;
            ProductType = productType;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"{Quantity.ToString().PadRight(6)}{Title.Truncate(20).PadRight(23)}{PriceIncTax.ToString(CultureInfo.CurrentCulture).PadLeft(8)}{TotalPriceIncTax.ToString(CultureInfo.InvariantCulture).PadLeft(10)}";

        }
        #endregion
    }
}

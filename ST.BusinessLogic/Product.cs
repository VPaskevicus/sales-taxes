using ST.BusinessLogic.Interfaces;
using System;

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
            get { return _title; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _title = value;
                else throw new ArgumentException("The product title cannot be null or empty.");
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
                if (value >= 0) _price = value;
                else throw new ArgumentException("The product price cannot be negative value.");
            }
        }

        /// <summary>
        /// The product quantity.
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value > 0) _quantity = value;
                else throw new ArgumentException("The product quantity cannot be zero or negative.");
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
                else throw new ArgumentException("The product tax cannot be negative value.");
            }

        }
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
    }
}

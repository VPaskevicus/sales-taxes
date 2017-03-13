namespace ST.BusinessLogic
{
    /// <summary>
    /// The product base class.
    /// </summary>
    public abstract class Product
    {
        #region Fields

        private decimal _price;
        private int _quantity;
        private decimal _productTax;

        #endregion

        #region Properties

        /// <summary>
        /// The title of the product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The product price.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set { if (value >= 0) _price = value; }
        }

        /// <summary>
        /// The product quantity.
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set { if (value > 0) _quantity = value; }
        }

        /// <summary>
        /// The type of the product.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// The tax applyed to the product.
        /// </summary>
        public decimal ProductTax
        {
            get { return _productTax; }
            set { if (value >= 0) _productTax = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="productType"></param>
        protected Product(string title, decimal price, int quantity, ProductType productType)
        {
            Title = title;
            Price = price;
            Quantity = quantity;
            ProductType = productType;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Calculate product tax based on the price and type.
        /// </summary>
        public abstract void CalculateProductTax();

        /// <summary>
        /// Print product data.
        /// </summary>
        public abstract void Print();

        #endregion
    }
}

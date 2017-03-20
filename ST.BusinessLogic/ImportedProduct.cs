namespace ST.BusinessLogic
{
    /// <summary>
    /// An imported product.
    /// </summary>
    public sealed class ImportedProduct : Product
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImportedProduct()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The product title.</param>
        /// <param name="price">The product price.</param>
        /// <param name="quantity">The product quantity.</param>
        /// <param name="productType">The product type.</param>
        public ImportedProduct(string title, decimal price, int quantity, ProductType productType)
            : base(title, price, quantity, productType)
        {
        }
    }
}

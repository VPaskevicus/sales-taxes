namespace ST.BusinessLogic.Interfaces
{
    /// <summary>
    /// An interface that represents the product.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// The title of the product.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The product price.
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// The product quantity.
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// The type of the product.
        /// </summary>
        ProductType ProductType { get; set; }

        /// <summary>
        /// The tax applyed to the product.
        /// </summary>
        decimal ProductTax { get; set; }

        /// <summary>
        /// Calculate product tax based on the price and type.
        /// </summary>
        void CalculateProductTax();

        /// <summary>
        /// Print product data.
        /// </summary>
        void Print();
    }
}

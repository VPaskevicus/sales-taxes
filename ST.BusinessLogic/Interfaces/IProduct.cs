namespace ST.BusinessLogic.Interfaces
{
    /// <summary>
    /// An interface that represents the product.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// The product title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The product price.
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// The total price based on the quantity.
        /// </summary>
        decimal TotalPrice { get; }

        /// <summary>
        /// The product quantity.
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// The product type.
        /// </summary>
        ProductType ProductType { get; set; }

        /// <summary>
        /// The product tax based on the price.
        /// </summary>
        decimal? ProductTax { get; set; }

        /// <summary>
        /// The product price including tax.
        /// </summary>
        decimal PriceIncTax { get; }

        /// <summary>
        /// The total price including tax based on the quantity.
        /// </summary>
        decimal TotalPriceIncTax { get; }
    }
}

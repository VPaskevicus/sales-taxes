namespace ST.BusinessLogic.Interfaces
{
    /// <summary>
    /// An interface that represents the receipt.
    /// </summary>
    public interface IReceipt
    {
        /// <summary>
        /// An array of products added.
        /// </summary>
        IProduct[] Products { get; }

        /// <summary>
        /// The total cost of all items including tax.
        /// </summary>
        decimal Subtotal { get; }

        /// <summary>
        /// The total amounts of sales taxes paid.
        /// </summary>
        decimal SalesTaxes { get; }
    }
}

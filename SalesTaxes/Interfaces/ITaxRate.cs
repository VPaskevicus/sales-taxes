namespace SalesTaxes.Interfaces
{
    public interface ITaxRate
    {
        /// <summary>
        /// Basic sales tax for calculation.
        /// </summary>
        int BasicSalesTax { get; set; }

        /// <summary>
        /// Imported sales tax for calculation.
        /// </summary>
        int ImportedSalesTax { get; set; }

        /// <summary>
        /// Calculate import tax.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        decimal CalculateImportTax(decimal price);

        /// <summary>
        /// Calculate basic tax.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        decimal CalculateBasicTax(decimal price);
    }
}

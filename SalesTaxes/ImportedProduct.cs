using SalesTaxes.Interfaces;
using System;

namespace SalesTaxes
{
    /// <summary>
    /// Imported product.
    /// </summary>
    public class ImportedProduct : Product, IProduct
    {
        private ITaxRate _taxRate;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="productType"></param>
        /// <param name="taxRate"></param>
        public ImportedProduct(string title, decimal price, int quantity, ProductType productType, ITaxRate taxRate) : base(title, price, quantity, productType)
        {
            _taxRate = taxRate;
        }

        /// <summary>
        /// Calculate tax for imported ptoduct.
        /// </summary>
        public override void CalculateProductTax()
        {
            bool applyTax = TaxCalculator.ApplyTaxCheck(this.ProductType);

            decimal calculatedProductTax;

            if (applyTax)
            {
                calculatedProductTax = TaxCalculator.RoundTax(_taxRate.CalculateImportTax(this.Price) + _taxRate.CalculateBasicTax(this.Price));
            }
            else
            {
                calculatedProductTax = TaxCalculator.RoundTaxCeiling(_taxRate.CalculateImportTax(this.Price));
            }

            ProductTax = calculatedProductTax;
        }

        /// <summary>
        /// Print product data.
        /// </summary>
        public override void Print()
        {
            decimal productPriceIncludingTax = TaxCalculator.CalculatePrice(Price, ProductTax, Quantity);
            Console.WriteLine(string.Format($"{Quantity} imported {Title}: {productPriceIncludingTax:F2}"));
        }
    }
}

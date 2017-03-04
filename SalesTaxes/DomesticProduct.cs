using SalesTaxes.Interfaces;
using System;

namespace SalesTaxes
{
    /// <summary>
    /// Domestic product.
    /// </summary>
    public class DomesticProduct : Product,   IProduct
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
        public DomesticProduct(string title, decimal price, int quantity, ProductType productType, ITaxRate taxRate) : base(title, price, quantity, productType)
        {
            _taxRate = taxRate;
        }

        /// <summary>
        /// Calculate tax for domestic ptoduct.
        /// </summary>
        public override void CalculateProductTax()
        {
            bool applyTax = TaxCalculator.ApplyTaxCheck(this.ProductType);
      
            if (applyTax)
            {
                ProductTax = TaxCalculator.RoundTaxCeiling(_taxRate.CalculateBasicTax(Price));  
            }
        }

        /// <summary>
        /// Print product data.
        /// </summary>
        public override void Print()
        {
            decimal productPriceIncludingTax = TaxCalculator.CalculatePrice(Price, ProductTax, Quantity);
            Console.WriteLine(string.Format($"{Quantity} {Title}: {productPriceIncludingTax:F2}"));
        }
    }
}

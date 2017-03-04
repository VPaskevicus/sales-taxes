using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxes
{
    /// <summary>
    /// Static tax calculator.
    /// </summary>
    public static class TaxCalculator
    {
        /// <summary>
        /// Round tax using ceiling.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal RoundTaxCeiling(decimal value)
        {
            if (value > 0)
                return Math.Ceiling(20 * value) / 20.0m;
            return 0;
        }

        /// <summary>
        /// Round tax up to the nearest 0.05.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal RoundTax(decimal value)
        {
            if (value > 0)
                return Math.Round(value, 2, MidpointRounding.AwayFromZero);
            else
                return 0;
        }

        /// <summary>
        /// Calculate subtotal for receipt according to products.
        /// </summary>
        public static decimal CalculateSubTotal(IProduct[] products)
        {
            if (products != null && products.Length > 0)
            {
                return products.Sum(p => p.Price * p.Quantity) + products.Sum(p => p.ProductTax * p.Quantity);
            }
            return 0;
        }

        /// <summary>
        /// Calculate subtotal for receipt according to products.
        /// </summary>
        public static decimal CalculateSalesTaxes(IProduct[] products)
        {
            if (products != null && products.Length > 0)
            {
                return products.Sum(p => (p.ProductTax * p.Quantity));
            }
            return 0;
        }

        /// <summary>
        /// Check if the basic tax should be applied based on the product type.
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public static bool ApplyTaxCheck(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Book:
                case ProductType.Food:
                case ProductType.MedicalProduct:
                    return false;
                case ProductType.Perfume:
                case ProductType.none:
                    return true;
                default: return true;
            }
        }

        /// <summary>
        /// Sum of price and tax based on the quantity.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="tax"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static decimal CalculatePrice(decimal price, decimal tax, int quantity)
        {
            return quantity * (price + tax);
        }
    }
}

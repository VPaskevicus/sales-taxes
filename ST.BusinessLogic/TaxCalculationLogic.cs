using ST.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.BusinessLogic
{
    /// <summary>
    /// Static tax calculator.
    /// </summary>
    public static class TaxCalculationLogic
    {
        /// <summary>
        /// Calculate tax for list of products using specific tax rate.
        /// </summary>
        /// <param name="listOfProducts">The list of product to calculate tax for.</param>
        /// <param name="chargeRate">The charge rate.</param>
        /// <param name="recalculateAll">Recalculate tax for each product, even if some poducts already have tax calculated.</param>
        /// <returns>The list of product with calculated tax.</returns>
        public static IList<IProduct> CalculateTax(IList<IProduct> listOfProducts, ChargeRate chargeRate, bool recalculateAll = false)
        {
            //re-calculate product tax
            if (recalculateAll) return CalculateTaxForEachProduct(listOfProducts, chargeRate);

            if (listOfProducts.Any(product => product == null))
                throw new ArgumentNullException(nameof(listOfProducts));

            //if tax already caclulated for all product, return the list
            if (listOfProducts.All(r => r.ProductTax.HasValue))
                return listOfProducts.ToList();

            //calculate tax only for products without any.
            foreach (var product in listOfProducts)
            {
                if (!product.ProductTax.HasValue)
                {
                    product.ProductTax = CalculateTax(product, chargeRate);
                }
            }
            return listOfProducts;
        }

        /// <summary>
        /// Calculate tax for single product using specific tax rate.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="chargeRate">The charge rate.</param>
        /// <returns>The tax.</returns>
        public static decimal CalculateTax(IProduct product, ChargeRate chargeRate)
        {
            if (product == null || chargeRate == null) throw new ArgumentNullException();

            var productTax = decimal.Zero;

            //check if standard tax has to be applied
            if (ApplyTax(product.ProductType))
            {
                productTax = CalculatePercentage(chargeRate.TaxRate, product.Price);
            }

            //add import duty if product is imported
            if (product is ImportedProduct)
            {
                productTax += CalculatePercentage(chargeRate.ImportDutyRate, product.Price);
            }
            return RoundTax(productTax);
        }

        /// <summary>
        /// Calculate percentage.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static decimal CalculatePercentage(decimal rate, decimal value)
        {
            if (value > 0 && rate > 0)
                return value * rate / 100;
            else
                return decimal.Zero;
        }

        /// <summary>
        /// Round tax up to the nearest 0.05.
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns>rounded value.</returns>
        internal static decimal RoundTax(decimal value)
        {
            if (value > 0)
                return Math.Round(value, 2, MidpointRounding.AwayFromZero);
            else
                return decimal.Zero;
        }

        /// <summary>
        /// Apply tax based on product type.
        /// </summary>
        /// <param name="productType">The product type.</param>
        /// <returns></returns>
        internal static bool ApplyTax(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.None:
                case ProductType.Perfume:
                    return true;
                case ProductType.Book:
                case ProductType.Food:
                case ProductType.MedicalProduct:
                    return false;
                default: return true;
            }
        }

        /// <summary>
        /// Calculate tax for each product in the list.
        /// </summary>
        /// <param name="listOfProducts">The list of product to calculate tax for.</param>
        /// <param name="chargeRate">The charge rate.</param>
        /// <returns>The list of product with calculated tax.</returns>
        private static IList<IProduct> CalculateTaxForEachProduct(IList<IProduct> listOfProducts, ChargeRate chargeRate)
        {
            foreach (var product in listOfProducts)
            {
                product.ProductTax = CalculateTax(product, chargeRate);
            }
            return listOfProducts;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace ST.BusinessLogic.Tests
{
    /// <summary>
    /// Tax calculation tests.
    /// </summary>
    [TestClass]
    public class TaxCalculationLogicTests
    {
        /// <summary>
        /// Attempt to pass list of products paremeter as null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxCalculation_ProductParameterNull_Fail()
        {
            TaxCalculationLogic.CalculateTax(null, new ChargeRate(10m, 5m));
        }

        /// <summary>
        /// Attempt to pass the product as null within the list of products paremeter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxCalculation_ListOfProductParameterNull_Fail()
        {
            TaxCalculationLogic.CalculateTax(new List<IProduct> { new Product(), null }, new ChargeRate(10m, 5m));
        }

        /// <summary>
        /// Attempt to list of products and charge rate paremeter as null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxCalculation_ListOfProucts_ChargeRateParameterNull_Fail()
        {
            TaxCalculationLogic.CalculateTax(new List<IProduct> { new Product(), new Product() }, null);
        }

        /// <summary>
        /// Attempt to a sigle product and charge rate paremeter as null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaxCalculation_SingleProduct_ChargeRateParameterNull_Fail()
        {
            TaxCalculationLogic.CalculateTax(new Product(), null);
        }

        /// <summary>
        /// Calculate tax only for products without tax.
        /// </summary>
        [TestMethod]
        public void TaxCalculation_CalculateTaxForListOfProduct_PartiallyCalculated()
        {
            // Arrange
            var listOfProduct = new List<IProduct>
            {
                new Product("vitamin C 30 Tablets", 13.82m, 1, ProductType.MedicalProduct),
                new Product("D&G Blue", 42.99m, 1, ProductType.Perfume),
                new ImportedProduct("Box of apples", 3.56m, 3, ProductType.Food),
                new ImportedProduct("3D Puzzle", 10m, 2, ProductType.None)
            };
            var chargeRate = new ChargeRate(12.06m, 5.2m);

            // Act
            listOfProduct[1].ProductTax = 3.5m;
            listOfProduct[2].ProductTax = 5.5m;
            var listOfProductWithTax = TaxCalculationLogic.CalculateTax(listOfProduct, chargeRate);

            // Assert
            Assert.AreEqual(4, listOfProductWithTax.Count);
            Assert.AreEqual(0, listOfProductWithTax[0].ProductTax);
            Assert.AreEqual(3.5m, listOfProductWithTax[1].ProductTax);
            Assert.AreEqual(5.5m, listOfProductWithTax[2].ProductTax);
            Assert.AreEqual(1.73m, listOfProductWithTax[3].ProductTax);
        }

        /// <summary>
        /// Recalculate tax for all products in the list.
        /// </summary>
        [TestMethod]
        public void TaxCalculation_RecalculateForAllProduct_Realculated()
        {
            // Arrange
            var listOfProduct = new List<IProduct>
            {
                new Product("vitamin C 30 Tablets", 13.82m, 1, ProductType.MedicalProduct),
                new Product("D&G Blue", 42.99m, 1, ProductType.Perfume),
                new ImportedProduct("Box of apples", 3.56m, 3, ProductType.Food),
                new ImportedProduct("3D Puzzle", 10m, 2, ProductType.None)
            };
            var chargeRate = new ChargeRate(12.06m, 5.2m);

            // Act
            listOfProduct[1].ProductTax = 3.5m;
            listOfProduct[2].ProductTax = 5.5m;
            var listOfProductWithTax = TaxCalculationLogic.CalculateTax(listOfProduct, chargeRate, recalculateAll: true);

            // Assert
            Assert.AreEqual(4, listOfProductWithTax.Count);
            Assert.AreEqual(0, listOfProductWithTax[0].ProductTax);
            Assert.AreEqual(5.18m, listOfProductWithTax[1].ProductTax);
            Assert.AreEqual(0.19m, listOfProductWithTax[2].ProductTax);
            Assert.AreEqual(1.73m, listOfProductWithTax[3].ProductTax);
        }

        /// <summary>
        /// Calculate percentage based on value.
        /// </summary>
        [TestMethod]
        public void TaxCalculation_CalculatePercentage_PercentageValue()
        {
            var percentage = TaxCalculationLogic.CalculatePercentage(10, 13.6m);

            Assert.AreEqual(1.36m, percentage);
        }

        /// <summary>
        /// Round value up to the nearest 0.05
        /// </summary>
        [TestMethod]
        public void TaxCalculation_RoundTax_RoundedValue()
        {
            var value1 = TaxCalculationLogic.RoundTax(432.32543m);
            var value2 = TaxCalculationLogic.RoundTax(432.3245m);

            Assert.AreEqual(432.33m, value1);
            Assert.AreEqual(432.32m, value2);
        }

        /// <summary>
        /// Apply tax based on product type.
        /// </summary>
        [TestMethod]
        public void TaxCalculation_ApplyTax()
        {
            Assert.IsTrue(TaxCalculationLogic.ApplyTax(ProductType.None));
            Assert.IsFalse(TaxCalculationLogic.ApplyTax(ProductType.MedicalProduct));
        }
    }
}

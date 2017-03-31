using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ST.BusinessLogic.Tests
{
    /// <summary>
    /// Tests of specific scenarios
    /// </summary>
    [TestClass]
    public class ScenarioTests
    {
        /// <summary>
        /// Test first scenario.
        /// </summary>
        [TestMethod]
        public void Scenario_One_Success()
        {
            // Arrange
            var listOfProducts = new List<IProduct>
            {
                new Product("book", 12.49m, 1, ProductType.Book),
                new Product("music CD", 14.99m, 1, ProductType.None),
                new Product("chocolate bar", 0.85m, 1, ProductType.Food)
            };
            var chargeRate = new ChargeRate(10, 5);

            // Act
            listOfProducts = TaxCalculationLogic.CalculateTax(listOfProducts, chargeRate).ToList();

            // Assert
            Assert.AreEqual(12.49m, listOfProducts[0].TotalPriceIncTax);
            Assert.AreEqual(16.49m, listOfProducts[1].TotalPriceIncTax);
            Assert.AreEqual(0.85m, listOfProducts[2].TotalPriceIncTax);
            Assert.AreEqual(1.50m, listOfProducts.Sum(product => product.ProductTax));
            Assert.AreEqual(29.83m, listOfProducts.Sum(product => product.TotalPriceIncTax));
        }

        /// <summary>
        /// Test second scenario.
        /// </summary>
        [TestMethod]
        public void Scenario_Two_Success()
        {
            // Arrange
            var listOfProducts = new List<IProduct>
            {
                new ImportedProduct("box of chocolates", 10.00m, 1, ProductType.Food),
                new ImportedProduct("bottle of perfume", 47.50m, 1, ProductType.Perfume)
            };
            var chargeRate = new ChargeRate(10, 5);

            // Act
            listOfProducts = TaxCalculationLogic.CalculateTax(listOfProducts, chargeRate).ToList();

            // Assert
            Assert.AreEqual(10.50m, listOfProducts[0].TotalPriceIncTax);
            Assert.AreEqual(54.63m, listOfProducts[1].TotalPriceIncTax);
            Assert.AreEqual(7.63m, listOfProducts.Sum(product => product.ProductTax));
            Assert.AreEqual(65.13m, listOfProducts.Sum(product => product.TotalPriceIncTax));
        }

        /// <summary>
        /// Test third scenario.
        /// </summary>
        [TestMethod]
        public void Scenario_Three_Success()
        {
            // Arrange
            var listOfProducts = new List<IProduct>
            {
                new ImportedProduct("bottle of perfume", 27.99m, 1, ProductType.Perfume),
                new Product("bottle of perfume", 18.99m, 1, ProductType.Perfume),
                new Product("packet of headache pills", 9.75m, 1, ProductType.MedicalProduct),
                new ImportedProduct("box of chocolates", 11.25m, 1, ProductType.Food)
            };
            var chargeRate = new ChargeRate(10, 5);

            // Act
            TaxCalculationLogic.CalculateTax(listOfProducts, chargeRate);

            // Assert
            Assert.AreEqual(32.19m, listOfProducts[0].TotalPriceIncTax);
            Assert.AreEqual(20.89m, listOfProducts[1].TotalPriceIncTax);
            Assert.AreEqual(9.75m, listOfProducts[2].TotalPriceIncTax);
            Assert.AreEqual(11.81m, listOfProducts[3].TotalPriceIncTax);
            Assert.AreEqual(6.66m, listOfProducts.Sum(product => product.ProductTax));
            Assert.AreEqual(74.64m, listOfProducts.Sum(product => product.TotalPriceIncTax));
        }
    }
}

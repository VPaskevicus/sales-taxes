using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.BusinessLogic.Tests
{
    /// <summary>
    /// The receipt class tests.
    /// </summary>
    [TestClass]
    public class ReceiptTests
    {
        /// <summary>
        /// Create receipt using default constructor.
        /// </summary>
        [TestMethod]
        public void Receipt_CreateWithoutParameters_Success()
        {
            var receipt = new Receipt();

            Assert.IsNotNull(receipt.ListOfProducts);
            Assert.AreEqual(0, receipt.ListOfProducts.Count);
            Assert.AreEqual(0, receipt.SalesTax);
            Assert.AreEqual(0, receipt.Subtotal);
            Assert.AreEqual(0, receipt.SubtotalIncTax);
        }

        //Create receipt using parameterized constructor.
        [TestMethod]
        public void Receipt_CreateWithParameters_Success()
        {
            // Arrange
            var listOfProduct = new List<IProduct>
            {
                new Product("vitamin C 30 Tablets", 13.82m, 1, ProductType.MedicalProduct),
                new Product("D&G Blue", 42.99m, 1, ProductType.Perfume),
                new ImportedProduct("Box of apples", 3.56m, 3, ProductType.Food),
                new ImportedProduct("3D Puzzle", 10m, 2, ProductType.None)
            };

            // Act
            var receipt = new Receipt(listOfProduct);

            // Assert
            Assert.IsNotNull(receipt.ListOfProducts);
            Assert.AreEqual(4, receipt.ListOfProducts.Count);
        }

        /// <summary>
        /// Attempt to create the receipt using list of product as null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Receipt_CreateUsingNullValue_Fail()
        {
            var receipt = new Receipt(null);
        }

        /// <summary>
        /// Attempt to create the receipt using the list of products that contains null value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Receipt_SetListOfProduct_ProductIsNull_Fail()
        {
            var listOfProduct = new List<IProduct>
            {
                new ImportedProduct(),
                null
            };
            var receipt = new Receipt(listOfProduct);
        }

        /// <summary>
        /// Get subtotal.
        /// </summary>
        [TestMethod]
        public void Receipt_GetSubtotal_Success()
        {
            // Arrange
            var listOfProduct = new List<IProduct>
            {
                new Product("vitamin C 30 Tablets", 13.82m, 1, ProductType.MedicalProduct),
                new Product("D&G Blue", 42.99m, 1, ProductType.Perfume),
                new ImportedProduct("Box of apples", 3.56m, 3, ProductType.Food),
                new ImportedProduct("3D Puzzle", 10m, 2, ProductType.None)
            };

            // Act
            var receipt = new Receipt(listOfProduct);
            var subtotal = receipt.Subtotal;

            // Assert
            Assert.AreEqual(87.49m, subtotal);
        }

        /// <summary>
        /// Get sales tax.
        /// </summary>
        [TestMethod]
        public void Receipt_GetSalesTax_Success()
        {
            // Arrange
            var listOfProduct = new List<IProduct>
            {
                new Product("vitamin C 30 Tablets", 13.82m, 1, ProductType.MedicalProduct),
                new Product("D&G Blue", 42.99m, 1, ProductType.Perfume),
                new ImportedProduct("Box of apples", 3.56m, 3, ProductType.Food),
                new ImportedProduct("3D Puzzle", 10m, 2, ProductType.None)
            };
            var chargeRate = new ChargeRate(10m, 5m);

            // Act
            listOfProduct = TaxCalculationLogic.CalculateTax(listOfProduct, chargeRate).ToList();
            var receipt = new Receipt(listOfProduct);
            var salesTax = receipt.SalesTax;

            // Assert
            Assert.AreEqual(7.84m, salesTax);
        }

        /// <summary>
        /// Get subtotal including tax.
        /// </summary>
        [TestMethod]
        public void Receipt_GetSubtotalIncTax_Success()
        {
            // Arrange
            var listOfProduct = new List<IProduct>
            {
                new Product("vitamin C 30 Tablets", 13.82m, 1, ProductType.MedicalProduct),
                new Product("D&G Blue", 42.99m, 1, ProductType.Perfume),
                new ImportedProduct("Box of apples", 3.56m, 3, ProductType.Food),
                new ImportedProduct("3D Puzzle", 10m, 2, ProductType.None)
            };
            var chargeRate = new ChargeRate(10m, 5m);

            // Act
            listOfProduct = TaxCalculationLogic.CalculateTax(listOfProduct, chargeRate).ToList();
            var receipt = new Receipt(listOfProduct);
            var subtotalIncTax = receipt.SubtotalIncTax;

            // Assert
            Assert.AreEqual(95.33m, subtotalIncTax);
        }
    }
}

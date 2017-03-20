using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic.Interfaces;
using ST.Common;
using System;
using System.Collections.Generic;

namespace ST.BusinessLogic.Tests
{
    /// <summary>
    /// Test product class.
    /// </summary>
    [TestClass]
    public class ProductTests
    {
        /// <summary>
        /// Create product using default constructor.
        /// </summary>
        [TestMethod]
        public void Product_CreateWithoutParameters_Success()
        {
            var product = new Product();

            Assert.AreEqual(0, product.Price);
            Assert.AreEqual(0, product.PriceIncTax);
            Assert.AreEqual(0, product.TotalPrice);
            Assert.AreEqual(0, product.TotalPriceIncTax);
            Assert.AreEqual(1, product.Quantity);
            Assert.AreEqual(ProductType.None, product.ProductType);
            Assert.IsNull(product.ProductTax);
        }

        /// <summary>
        /// Create product using parameterized constructor.
        /// </summary>
        [TestMethod]
        public void Product_CreateWithParameters_Success()
        {
            var product = new Product("Olive oil 500mg", 8.99m, 2, ProductType.Food);

            Assert.AreEqual("Olive oil 500mg", product.Title);
            Assert.AreEqual(8.99m, product.Price);
            Assert.AreEqual(8.99m, product.PriceIncTax);
            Assert.AreEqual(17.98m, product.TotalPrice);
            Assert.AreEqual(17.98m, product.TotalPriceIncTax);
            Assert.AreEqual(2, product.Quantity);
            Assert.AreEqual(ProductType.Food, product.ProductType);
            Assert.IsNull(product.ProductTax);
        }

        /// <summary>
        /// Attempt to get unassigned product title.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Product_GetEmptyTitle_Fail()
        {
            var product = new Product();
            var title = product.Title;
        }

        /// <summary>
        /// Attempt to set the product title to an empty string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Product_SetEmptyTitle_Fail()
        {
            var product = new Product();
            product.Title = "";
        }

        /// <summary>
        /// Attempt to set the product title to null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Product_SetTitleToNull_Fail()
        {
            var product = new Product();

            product.Title = null;
        }

        /// <summary>
        /// Attempt to set the product price to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetNegativePrice_Fail()
        {
            var product = new Product { Price = -23.45m };
        }

        /// <summary>
        /// Attempt tp set the product price to zero.
        /// </summary>
        [TestMethod]
        public void Product_TruncatePriceDecimals_PriceWithTwoDecimals()
        {
            var listOfProduct = new List<IProduct>
            {
                new Product() { Price = 3.2392m },
                new Product() { Price = 3.231m }
            };

            Assert.AreEqual(3.23m, listOfProduct[0].Price);
            Assert.AreEqual(3.23m, listOfProduct[1].Price);
        }

        /// <summary>
        /// Attempt to set the product price to zero.
        /// </summary>
        [TestMethod]
        public void Product_SetPriceToZero_Success()
        {
            var product = new Product { Price = 0 };
            Assert.AreEqual(0, product.Price);
        }

        /// <summary>
        /// Attempt to set the product quantity to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetNegativeQuantity_Fail()
        {
            var product = new Product { Quantity = -4 };
        }

        /// <summary>
        /// Attemp to set the product quantity to zero.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetQuantityToZero_Success()
        {
            var product = new Product { Quantity = 0 };
        }

        /// <summary>
        /// Attempt to set the product tax to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetNegativeTax_Fail()
        {
            var product = new Product { ProductTax = -23.45m };
        }

        /// <summary>
        /// Attempt to set the product tax to zero.
        /// </summary>
        [TestMethod]
        public void Product_SetTaxToZero_Fail()
        {
            var product = new Product { ProductTax = 0 };
            Assert.AreEqual(0, product.ProductTax);
        }

        /// <summary>
        /// Attempt to get the price including tax without tax assigned, will return the product price.
        /// </summary>
        [TestMethod]
        public void Product_GetPriceIncTax_ProductTaxIsNull_SamePrice()
        {
            var product = new Product { Price = 2.83m };
            Assert.AreEqual(2.83m, product.PriceIncTax);
        }

        /// <summary>
        /// Get price including tax.
        /// </summary>
        [TestMethod]
        public void Product_GetPriceIncTax_PriceWithTax()
        {
            var product = new Product { Price = 4m, ProductTax = 2m };
            Assert.AreEqual(6m, product.PriceIncTax);
        }

        /// <summary>
        /// Get total price based on the product quantity.
        /// </summary>
        [TestMethod]
        public void Product_GetTotalPrice_PriceBasedOnQuantity()
        {
            var product = new Product { Price = 11.11m, Quantity = 3 };
            Assert.AreEqual(33.33m, product.TotalPrice);
        }

        /// <summary>
        /// Attempt to get the total price including tax without tax assigned, will return the product price.
        /// </summary>
        [TestMethod]
        public void Product_GetTotalPriceIncTax_ProductTaxIsNull_SamePriceBasedOnQuantity()
        {
            var product = new Product { Price = 11.11m, Quantity = 3 };
            Assert.AreEqual(33.33m, product.TotalPriceIncTax);
        }

        /// <summary>
        /// Get total price including tax.
        /// </summary>
        [TestMethod]
        public void Product_GetTotalPriceIncTax_PriceBasedOnQuantityIncTax()
        {
            var product = new Product { Price = 2.2m, Quantity = 3, ProductTax = .3m };
            Assert.AreEqual(7.5m, product.TotalPriceIncTax);
        }

        /// <summary>
        /// Use of the ToString method on product without title, will throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Product_ProductWithoutTitleToString_Fail()
        {
            var product = new Product();
            var result = product.ToString();
        }

        /// <summary>
        /// Standard product to string result.
        /// </summary>
        [TestMethod]
        public void Product_StandardProductToString_Success()
        {
            // Arrange
            var product = new Product { Title = "Popcorn 150g" };
            var expected = "1 Popcorn 150g 0 0";

            // Act
            var result = product.ToString();
            var normalizedResult = StringExtention.NormalizeWhitespace(result);

            // Assert
            Assert.AreEqual(expected, normalizedResult);
        }
    }
}

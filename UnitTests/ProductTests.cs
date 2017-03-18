using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ST.BusinessLogic.Interfaces;

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

            Assert.IsNull(product.Title);
            Assert.AreEqual(0, product.Price);
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
            var product = new Product("book", 18.99m, 1, ProductType.Book);

            Assert.AreEqual("book", product.Title);
            Assert.AreEqual(18.99m, product.Price);
            Assert.AreEqual(1, product.Quantity);
            Assert.AreEqual(ProductType.Book, product.ProductType);
            Assert.IsNull(product.ProductTax);
        }

        /// <summary>
        /// Set product title to an empty string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Product_SetEmptyTitle_Fail()
        {
            var product = new Product();
            product.Title = "";
        }

        /// <summary>
        /// Set product title to null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Product_SetTitleToNull_Fail()
        {
            var product = new Product();

            product.Title = null;
        }

        /// <summary>
        /// Set product price to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetNegativePrice_Fail()
        {
            var product = new Product { Price = -23.45m };
        }

        /// <summary>
        /// Set product price to zero.
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
        /// Set product price to zero.
        /// </summary>
        [TestMethod]
        public void Product_SetPriceToZero_Success()
        {
            var product = new Product { Price = 0 };
            Assert.AreEqual(0, product.Price);
        }

        /// <summary>
        /// Set product quantity to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetNegativeQuantity_Fail()
        {
            var product = new Product { Quantity = -4 };
        }

        /// <summary>
        /// Set product quantity to zero.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetQuantityToZero_Success()
        {
            var product = new Product { Quantity = 0 };
        }

        /// <summary>
        /// Set product tax to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Product_SetNegativeTax_Fail()
        {
            var product = new Product { ProductTax = -23.45m };
        }

        /// <summary>
        /// Set product tax to zero.
        /// </summary>
        [TestMethod]
        public void Product_SetTaxToZero_Fail()
        {
            var product = new Product { ProductTax = 0 };
            Assert.AreEqual(0, product.ProductTax);
        }

        [TestMethod]
        public void Product_PriceIncTax_ProductTaxIsNull_SamePrice()
        {
            var product = new Product { Price = 2.83m};
            Assert.AreEqual(2.83m, product.PriceIncTax);
        }

        [TestMethod]
        public void Product_PriceIncTax_PriceWithTax()
        {
            var product = new Product { Price = 4m, ProductTax = 2m };
            Assert.AreEqual(6m, product.PriceIncTax);
        }

        [TestMethod]
        public void Product_TotalPrice_PriceBasedOnQuantity()
        {
            var product = new Product { Price = 11.11m, Quantity = 3 };
            Assert.AreEqual(33.33m, product.TotalPrice);
        }

        [TestMethod]
        public void Product_TotalPriceIncTax_ProductTaxIsNull_SamePriceBasedOnQuantity()
        {
            var product = new Product { Price = 11.11m, Quantity = 3 };
            Assert.AreEqual(33.33m, product.TotalPriceIncTax);
        }

        [TestMethod]
        public void Product_TotalPriceIncTax_PriceBasedOnQuantityIncTax()
        {
            var product = new Product { Price = 2.2m, Quantity = 3, ProductTax = .3m };
            Assert.AreEqual(7.5m, product.TotalPriceIncTax);
        }
    }
}

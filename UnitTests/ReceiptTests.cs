using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic;
using ST.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    /// Test Receipt.
    /// </summary>
    [TestClass]
    public class ReceiptTests
    {
        /// <summary>
        /// Test Creation of the receipt.
        /// </summary>
        [TestMethod]
        public void TestCreateReceipt()
        {
            IReceipt receipt = CreateReceipt(); ;

            Assert.IsNotNull(receipt);
            Assert.AreEqual(3, receipt.Products.Length);
            Assert.IsNotNull(receipt.SalesTaxes);
            Assert.AreEqual(1.5m, receipt.SalesTaxes);
            Assert.IsNotNull(receipt.Subtotal);
            Assert.AreEqual(29.83m, receipt.Subtotal);
        }

        public IReceipt CreateReceipt()
        {
            ITaxRate taxRate = new TaxRate(10, 5);

            List<IProduct> listOfProducts = new List<IProduct>() {
                new DomesticProduct("book", 12.49m, 1, ProductType.Book, taxRate),
                new DomesticProduct("music CD", 14.99m, 1, ProductType.None, taxRate),
                new DomesticProduct("chocolate bar", 0.85m, 1, ProductType.Food, taxRate)
            };

            return new Receipt(listOfProducts.ToArray());
        }
    }
}

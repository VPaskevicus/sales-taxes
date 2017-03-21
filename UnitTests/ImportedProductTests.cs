using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.Common;

namespace ST.BusinessLogic.Tests
{
    /// <summary>
    /// Test imported product class tests.
    /// </summary>
    [TestClass]
    public class ImportedProductTests
    {
        /// <summary>
        /// Imported product to string result.
        /// </summary>
        [TestMethod]
        public void Product_ImportedProductToString_Success()
        {
            // Arrange
            var product = new ImportedProduct()
            {
                Title = "Chocolate 100g",
                Price = 1.5m,
                Quantity = 2,
                ProductTax = 0.07m,
                ProductType = ProductType.Food
            };
            var expected = "2 Chocolate 100g 1.57 3.14";

            // Act
            var result = product.ToString();
            var normalizedResult = StringExtention.NormalizeWhitespace(result);

            // Assert
            Assert.AreEqual(expected, normalizedResult);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxes;
using SalesTaxes.Interfaces;

namespace UnitTests
{
    /// <summary>
    /// Test domestic product.
    /// </summary>
    [TestClass]
    public class DomesticProductTests
    {
        /// <summary>
        /// Test creation of domestic product.
        /// </summary>
        [TestMethod]
        public void TestCreateDomesticProduct()
        {
            IProduct product = CreateDomesticProduct();

            Assert.AreEqual("book", product.Title);
            Assert.AreEqual(18.99m, product.Price);
            Assert.AreEqual(1, product.Quantity);
            Assert.AreEqual(ProductType.none, product.ProductType);
            Assert.AreEqual(0, product.ProductTax);
        }

        /// <summary>
        /// Test tax calculation for domestic product.
        /// </summary>
        [TestMethod]
        public void TestDomesticProductTaxCalculation()
        {
            IProduct product = CreateDomesticProduct();

            product.CalculateProductTax();
            Assert.AreEqual(1.90m, product.ProductTax);
        }

        /// <summary>
        /// Test tax calculation for domestic product with different types.
        /// </summary>
        [TestMethod]
        public void TestDomesticProductTaxCalculation_TestTypes()
        {
            IProduct product = CreateDomesticProduct();

            product.ProductType = ProductType.Book;
            product.CalculateProductTax();
            Assert.AreEqual(0, product.ProductTax);

            product.ProductType = ProductType.Food;
            product.CalculateProductTax();
            Assert.AreEqual(0, product.ProductTax);

            product.ProductType = ProductType.MedicalProduct;
            product.CalculateProductTax();
            Assert.AreEqual(0, product.ProductTax);
        }


        private IProduct CreateDomesticProduct()
        {
            ITaxRate taxRate = new TaxRate(10, 5);
            return new DomesticProduct("book", 18.99m, 1, ProductType.none, taxRate);
        }
    }
}

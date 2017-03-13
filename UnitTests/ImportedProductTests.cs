using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic;
using ST.BusinessLogic.Interfaces;

namespace UnitTests
{
    /// <summary>
    /// Test Imported product.
    /// </summary>
    [TestClass]
    public class ImportedProductTests
    {
        /// <summary>
        /// Test creation of imported product.
        /// </summary>
        [TestMethod]
        public void TestCreateImportedProduct()
        {
            IProduct product = CreateImportedProduct();

            Assert.AreEqual("box of chocolates", product.Title);
            Assert.AreEqual(32.19m, product.Price);
            Assert.AreEqual(1, product.Quantity);
            Assert.AreEqual(ProductType.None, product.ProductType);
            Assert.AreEqual(0, product.ProductTax);
        }

        /// <summary>
        /// Test tax calculation for imported product.
        /// </summary>
        [TestMethod]
        public void TestImportedProductTaxCalculation()
        {
            IProduct product = CreateImportedProduct();

            product.CalculateProductTax();
            Assert.AreEqual(4.83m, product.ProductTax);
        }

        /// <summary>
        /// Test tax calculation for imported product with different types.
        /// </summary>
        [TestMethod]
        public void TestImportedProductTaxCalculation_TestTypes()
        {
            IProduct product = CreateImportedProduct();

            product.ProductType = ProductType.Book;
            product.CalculateProductTax();
            Assert.AreEqual(1.65m, product.ProductTax);

            product.ProductType = ProductType.Food;
            product.CalculateProductTax();
            Assert.AreEqual(1.65m, product.ProductTax);

            product.ProductType = ProductType.MedicalProduct;
            product.CalculateProductTax();
            Assert.AreEqual(1.65m, product.ProductTax);
        }


        public IProduct CreateImportedProduct()
        {
            ITaxRate taxRate = new TaxRate(10, 5);

            return new ImportedProduct("box of chocolates", 32.19m, 1, ProductType.None, taxRate);
        }
    }
}

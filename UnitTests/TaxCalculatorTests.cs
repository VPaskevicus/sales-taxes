using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.BusinessLogic;
using ST.BusinessLogic.Interfaces;

namespace UnitTests
{
    /// <summary>
    /// Tax calculation tests.
    /// </summary>
    [TestClass]
    public class TaxCalculatorTests
    {
        /// <summary>
        /// Test ability to calculate basic product tax.
        /// </summary>
        [TestMethod]
        public void TestBasicTaxCalculation()
        {
            ITaxRate taxRate = new TaxRate(10, 5);
            Assert.AreEqual(1.125m, taxRate.CalculateBasicTax(11.25m));
        }

        /// <summary>
        /// Test ability to calculate imported product tax.
        /// </summary>
        [TestMethod]
        public void TestImportedTaxCalculation()
        {
            ITaxRate taxRate = new TaxRate(10, 5);
            Assert.AreEqual(0.5625m, taxRate.CalculateImportTax(11.25m));
        }

        /// <summary>
        /// Test ability to round tax.
        /// </summary>
        [TestMethod]
        public void TestTaxRoundCalculation()
        {
            Assert.AreEqual(0.6m, TaxCalculator.RoundTaxCeiling(0.5625m));
            Assert.AreEqual(0.56m, TaxCalculator.RoundTax(0.5625m));
        }

        /// <summary>
        /// Test tax check
        /// </summary>
        [TestMethod]
        public void TestApplyTaxCheck()
        {
            Assert.IsFalse(TaxCalculator.ApplyTaxCheck(ProductType.Book));
            Assert.IsFalse(TaxCalculator.ApplyTaxCheck(ProductType.Food));
            Assert.IsFalse(TaxCalculator.ApplyTaxCheck(ProductType.MedicalProduct));
            Assert.IsTrue(TaxCalculator.ApplyTaxCheck(ProductType.None));
            Assert.IsTrue(TaxCalculator.ApplyTaxCheck(ProductType.Perfume));
        }

        /// <summary>
        /// Test price calculation
        /// </summary>
        [TestMethod]
        public void TestCalculatePrice()
        {
            Assert.AreEqual(69.576m, TaxCalculator.CalculatePrice(28.99m, 5.798m, 2));
        }
    }
}

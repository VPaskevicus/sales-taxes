using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ST.BusinessLogic.Tests
{
    /// <summary>
    /// The charge rate class tests
    /// </summary>
    [TestClass]
    public class ChargeRateTests
    {
        /// <summary>
        /// Create charge rate using parameterized constructor.
        /// </summary>
        [TestMethod]
        public void ChargeRate_Create_Success()
        {
            var chargeRate = new ChargeRate(10.04m, 5m);

            Assert.AreEqual(10.04m, chargeRate.TaxRate);
            Assert.AreEqual(5m, chargeRate.ImportDutyRate);
        }

        /// <summary>
        /// Set tax rate to zero.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChargeRate_SetTaxRateToZero_Fail()
        {
            var chargeRate = new ChargeRate(0, 5m);
        }

        /// <summary>
        /// Set tax rate to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChargeRate_SetNegativeTaxRate_Fail()
        {
            var chargeRate = new ChargeRate(-3.2m, 5m);
        }

        /// <summary>
        /// Set import duty rate to zero.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChargeRate_SetImportDutyRateToZero_Fail()
        {
            var chargeRate = new ChargeRate(12m, 0);
        }

        /// <summary>
        /// Set import duty rate to negative value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChargeRate_SetNegativeImportDutyRate_Fail()
        {
            var chargeRate = new ChargeRate(14.2m, -34.7m);
        }
    }
}

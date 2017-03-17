using System;

namespace ST.BusinessLogic
{
    /// <summary>
    /// The tax rate.
    /// </summary>
    public sealed class ChargeRate
    {
        #region Fields
        private decimal _taxRate;
        private decimal _importDutyRate;
        #endregion

        #region Properties
        /// <summary>
        /// The standard tax rate.
        /// </summary>
        public decimal TaxRate
        {
            internal get { return _taxRate; }
            set
            {
                if (value > 0) _taxRate = value;
                else throw new ArgumentException("The tax rate connot be negative value.");
            }
        }

        /// <summary>
        /// An import duty.
        /// </summary>
        public decimal ImportDutyRate
        {
            internal get { return _importDutyRate; }
            set
            {
                if (value > 0) _importDutyRate = value;
                else throw new ArgumentException("An import duty connot be negative value.");
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="taxRate">The standard tax rate.</param>
        /// <param name="importDutyRate"></param>
        public ChargeRate(decimal taxRate, decimal importDutyRate)
        {
            TaxRate = taxRate;
            ImportDutyRate = importDutyRate;
        }
        #endregion
    }
}

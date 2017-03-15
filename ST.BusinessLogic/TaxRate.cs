using ST.BusinessLogic.Interfaces;
using System;

namespace ST.BusinessLogic
{
    /// <summary>
    /// Tax calculation data logic.
    /// </summary>
    public class TaxRate : ITaxRate
    {
        #region Fields

        private int _basicSalesTax;
        private int _importedSalesTax;

        #endregion

        #region Properties

        /// <summary>
        /// Basic sales tax for calculation.
        /// </summary>
        public int BasicSalesTax
        {
            get { return _basicSalesTax; }
            set
            {
                if (value > 0 && value <= 100)
                    _basicSalesTax = value;
                else
                    throw new ArgumentOutOfRangeException("Invalid tax rate for basic sales.\nPlease enter values from 1 to 100.\n");
            }
        }

        /// <summary>
        /// Imported sales tax for calculation.
        /// </summary>
        public int ImportedSalesTax
        {
            get { return _importedSalesTax; }
            set
            {
                if (value > 0 && value <= 100)
                    _importedSalesTax = value;
                else
                    throw new ArgumentOutOfRangeException("Invalid tax rate for imported sales.\nPlease enter values from 1 to 100.\n");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="basicSalesTax"></param>
        /// <param name="importedSalesTax"></param>
        public TaxRate(int basicSalesTax, int importedSalesTax)
        {
            BasicSalesTax = basicSalesTax;
            ImportedSalesTax = importedSalesTax;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculate import tax.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal CalculateImportTax(decimal price)
        {
            if (price > 0 && ImportedSalesTax > 0)
                return price * ImportedSalesTax / 100;
            else
                return 0;
        }

        /// <summary>
        /// calculate basic tax.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal CalculateBasicTax(decimal price)
        {
            if (price > 0 && BasicSalesTax > 0)
                return price * BasicSalesTax / 100;
            else
                return 0;
        }

        #endregion
    }
}

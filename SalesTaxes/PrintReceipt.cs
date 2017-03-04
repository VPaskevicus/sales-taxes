using SalesTaxes.Interfaces;
using System;

namespace SalesTaxes
{
    /// <summary>
    /// The class that is responsible for receipt printing.
    /// </summary>
    public class PrintReceipt : IPrintReceipt
    {
        #region Fields

        private IReceipt _receipt;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="receipt"></param>
        public PrintReceipt(IReceipt receipt)
        {
            this._receipt = receipt;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Print receipt to the console.
        /// </summary>
        public void Print()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("##########   RECEIPT   ##########");
            Console.WriteLine("---------------------------------\n");
            foreach (IProduct product in _receipt.Products)
            {
                {
                    product.Print();
                }
            }
            Console.WriteLine(string.Format($"Sales Taxes: {_receipt.SalesTaxes:F2}"));
            Console.WriteLine(string.Format($"Total: {_receipt.Subtotal:F2} \n"));
            Console.WriteLine("---------------------------------");

        }

        #endregion
    }
}
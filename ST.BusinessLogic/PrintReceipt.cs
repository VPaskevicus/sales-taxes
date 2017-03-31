using ST.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using static System.Console;

namespace ST.BusinessLogic
{
    /// <summary>
    /// The class that is responsible for receipt printing.
    /// </summary>
    public static class PrintReceipt
    {
        /// <summary>
        /// Create the receipt calculate tax an.
        /// </summary>
        /// <param name="listOfProducts">The list of product.</param>
        /// <param name="chargeRate">The charge rate.</param>
        public static void PrintReceiptWithCalculatedTax(IList<IProduct> listOfProducts, ChargeRate chargeRate)
        {
            if (listOfProducts == null || listOfProducts.Any(product => product == null))
                throw new ArgumentNullException(nameof(listOfProducts));
            if (chargeRate == null)
                throw new ArgumentNullException(nameof(chargeRate));

            //calculate tax
            listOfProducts = TaxCalculationLogic.CalculateTax(listOfProducts, chargeRate, recalculateAll: true);

            var receipt = new Receipt(listOfProducts);

            PrintReceiptToConsole(receipt);
        }

        /// <summary>
        /// Print the receipt to the console.
        /// </summary>
        /// <param name="receipt">The receipt</param>
        private static void PrintReceiptToConsole(Receipt receipt)
        {
            WriteLine("-----------------------------------------------");
            WriteLine("#################   RECEIPT   #################");
            WriteLine("-----------------------------------------------\n");
            foreach (var product in receipt.ListOfProducts)
            {
                WriteLine(product.ToString());
            }
            WriteLine("\n-----------------------------------------------\n");
            WriteLine("Sales Tax:".PadRight(30) + $"{ receipt.SalesTax:F2}".PadLeft(17));
            WriteLine("Subtotal:".PadRight(30) + $"{receipt.Subtotal:F2}".PadLeft(17));
            WriteLine("Subtotal inc Tax:".PadRight(30) + $"{receipt.SubtotalIncTax:F2}".PadLeft(17));
            WriteLine("\n-----------------------------------------------");
        }
    }
}
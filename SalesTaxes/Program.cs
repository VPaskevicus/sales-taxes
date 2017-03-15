using ST.BusinessLogic;
using ST.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace ST.ConsoleApp
{
    class Program
    {
        private const string ImportedProductIdentifier = "imported";
        private const string PriceIdentifier = "at";
        private const string TriggerReceiptCreation = "-1";

        static void Main(string[] args)
        {
            //Create tax rate based on the user input
            ITaxRate taxRate = CreateTaxRate();

            if (taxRate != null)
            {
                try
                {
                    List<IProduct> listOfProducts = AddProductsBasedOnUserInput(taxRate);

                    IReceipt receipt = CreateReceipt(listOfProducts);
                    if (receipt != null)
                    {
                        PrintReceipt(receipt);
                    }
                }

                catch (Exception)
                {
                    Console.WriteLine("--- WARNING --- Something went wrong!!!\n");

                    //TODO: Error handling for:
                    //  product line input
                    //  product creation
                    //  receipt creation
                    //  receipt printing
                }
            }

            Console.ReadKey();
        }

        private static List<IProduct> AddProductsBasedOnUserInput(ITaxRate taxRate)
        {
            List<IProduct> listOfProducts = new List<IProduct>();

            bool importedProduct = false;
            decimal productPrice = 0;
            int productQuantity = 0;
            List<string> productDescription = new List<string>();

            bool productsAdded = false;

            while (!productsAdded)
            {
                Console.Write("Enter product line or (-1 to print): ");
                string productLine = Console.ReadLine();
                if (productLine.Equals(TriggerReceiptCreation))
                {
                    productsAdded = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(productLine))
                    {
                        throw new ArgumentNullException();
                    }
                    else
                    {
                        string[] words = productLine.Split(' ');

                        //set the quantity
                        if (int.TryParse(words[0], out productQuantity))
                        {
                            //if quantity set, loop from second word
                            for (int i = 1; i < words.Length; i++)
                            {
                                //determine if the product line includes "imported"
                                if (words[i].Equals(ImportedProductIdentifier))
                                {
                                    importedProduct = true;
                                }
                                else
                                {
                                    //identify product price
                                    if (words[i].Equals(PriceIdentifier))
                                    {
                                        decimal.TryParse(words[i + 1], out productPrice);
                                        break;
                                    }
                                    else
                                    {
                                        //add description
                                        productDescription.Add(words[i]);
                                    }
                                }

                            }
                        }
                        else
                        {
                            //if quantity not specified set as default to 1
                            productQuantity = 1;
                            for (int i = 0; i < words.Length; i++)
                            {
                                //determine if the product line includes "imported"
                                if (words[i].Equals(ImportedProductIdentifier))
                                {
                                    importedProduct = true;
                                }
                                else
                                {
                                    //determine if the product line string include "imported"
                                    if (words[i].Equals(PriceIdentifier))
                                    {
                                        decimal.TryParse(words[i + 1], out productPrice);
                                        break;
                                    }
                                    else
                                    {
                                        //add description
                                        productDescription.Add(words[i]);
                                    }
                                }
                            }
                        }
                    }

                    //ask user for the product type
                    int productType = AskUserToSetProductType();

                    if (productType >= 0)
                    {
                        IProduct product;

                        if (importedProduct)
                        {
                            product = new ImportedProduct(string.Join(" ", productDescription), productPrice, productQuantity, (ProductType)productType, taxRate);
                        }
                        else
                        {
                            product = new DomesticProduct(string.Join(" ", productDescription), productPrice, productQuantity, (ProductType)productType, taxRate);
                        }
                        listOfProducts.Add(product);

                        //reset product fiels
                        importedProduct = false;
                        productPrice = 0;
                        productQuantity = 0;
                        productDescription.Clear();
                    }
                }
            }

            return listOfProducts;
        }

        private static ITaxRate CreateTaxRate()
        {
            bool taxRateCreated = false;
            ITaxRate taxRate = null;

            while (taxRateCreated == false)
            {
                try
                {
                    Console.Write("Enter your basic tax rate (%): ");

                    int basicTaxRate = int.Parse(Console.ReadLine());


                    Console.Write("Please enter your imported tax rate (%): ");

                    int importedTaxRate = int.Parse(Console.ReadLine());


                    taxRate = new TaxRate(basicTaxRate, importedTaxRate);

                    taxRateCreated = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("--- WARNING --- Invalid value format for the tax rate!\n");
                }
                catch (Exception)
                {
                    Console.WriteLine("--- WARNING --- Something else went wrong!!!\n");
                }
            }

            return taxRate;
        }

        private static int AskUserToSetProductType()
        {
            bool typeSet = false;
            int productType = -1;

            while (!typeSet)
            {
                Console.WriteLine("Set the type of the product: ");
                string[] productTypeNames = Enum.GetNames(typeof(ProductType));
                for (int i = 0; i < productTypeNames.Length; i++)
                {
                    Console.WriteLine($"{i} - {productTypeNames[i]}");
                }

                int.TryParse(Console.ReadLine(), out productType);
                string[] d = Enum.GetNames(typeof(ProductType));
                if (productType >= 0 && productType < d.Length)
                {
                    typeSet = true;
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
            }
            return productType;
        }

        #region Create and print receipt

        private static IReceipt CreateReceipt(List<IProduct> list)
        {
            if (list != null && list.Count > 0)
            {
                return new Receipt(list.ToArray());
            }
            return null;
        }

        private static void PrintReceipt(IReceipt receipt)
        {
            if (receipt != null && receipt.Products != null && receipt.SalesTaxes >= 0 && receipt.Subtotal >= 0)
            {
                IPrintReceipt printReceipt = new PrintReceipt(receipt);
                printReceipt.Print();
            }
        }

        #endregion

    }
}

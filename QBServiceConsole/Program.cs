using System;
using System.Collections.Generic;

namespace QBServiceConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Quickbooks Invoice/Bill sync service started!");
            QuickBook quickBook = new QuickBook();
            var companiesQbDetails = quickBook.GetCompaniesQuickbookDetails();
            foreach (var companyQbDetails in companiesQbDetails)
            {
                Console.WriteLine($"Processing company with ID: {companyQbDetails.CompanyID}!");
                quickBook.QbInfo = companyQbDetails;
                if (string.IsNullOrEmpty(quickBook.QbInfo.RefreshToken) || string.IsNullOrEmpty(quickBook.QbInfo.RealmID))
                {
                    // Selected company does not contian quickbook credentials...
                    Console.WriteLine($"QuickBook credentials for company with ID: {quickBook.QbInfo.CompanyID} not found!");
                    continue;
                }

                quickBook.SetServiceContext();

                List<int> processedInvoices = new List<int>();
                List<int> processedBills = new List<int>();

                var serviceExecution = quickBook.GetServiceExecutionDateFromDB();

                DateTime? invoiceUpdateDateInDb = Convert.ToDateTime(serviceExecution.InvoiceUpdateDate);
                invoiceUpdateDateInDb = DateTime.SpecifyKind(invoiceUpdateDateInDb.Value, DateTimeKind.Utc);

                DateTime? billUpdateDateInDb = Convert.ToDateTime(serviceExecution.BillUpdateDate);
                billUpdateDateInDb = DateTime.SpecifyKind(billUpdateDateInDb.Value, DateTimeKind.Utc);

                DateTime? invoiceUpdateDate = invoiceUpdateDateInDb;
                DateTime? billUpdateDate = billUpdateDateInDb;

                Console.WriteLine($"Invoices last updated at: {invoiceUpdateDate.Value:MM-dd-yyyy HH:mm:ss} for company with ID: {companyQbDetails.CompanyID}!");
                Console.WriteLine("Synchronizing invoices...");

                /* Process Invoice */
                var invoices = quickBook.GetInvoices(invoiceUpdateDate);
                try
                {
                    foreach (var invoice in invoices)
                    {
                        int invoiceNumber = Convert.ToInt32(invoice.CustomField[0].AnyIntuitObject);
                        if (invoiceNumber != 0)
                        {
                            Console.WriteLine($"Syncing invoice number: {invoiceNumber}...");
                            // Fail safe check to make sure that there is only one invoiceNumber per QuickBook invoice...
                            if (!processedInvoices.Contains(invoiceNumber))
                            {
                                bool result = quickBook.MarkInvoiceAsPaidInDB(invoiceNumber, Convert.ToInt32(invoice.DocNumber), invoice.TotalAmt);
                                if (!result)
                                {
                                    Console.WriteLine("An error occurred while trying to mark invoice as paid in DB!");
                                    throw new Exception();
                                }
                                invoiceUpdateDate = invoice.MetaData.LastUpdatedTime.ToUniversalTime();
                                processedInvoices.Add(invoiceNumber);
                                Console.WriteLine($"Successfully synchronized invoice number: {invoiceNumber}!");
                            }
                            else
                            {
                                // No need to update in Database...
                                invoiceUpdateDate = invoice.MetaData.LastUpdatedTime.ToUniversalTime();
                                Console.WriteLine($"Invoice number: {invoiceNumber} is already synchronized!");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred!\n{e.StackTrace}");
                }

                if (invoiceUpdateDate != null && invoiceUpdateDate != invoiceUpdateDateInDb && processedInvoices.Count > 0)
                {
                    bool result = quickBook.SetInvoiceUpdateDateInDB(invoiceUpdateDate.Value);
                    if (result)
                    {
                        Console.WriteLine($"Invoices marked as updated till {invoiceUpdateDate} for company with ID: {quickBook.QbInfo.CompanyID}!");
                    }
                    else
                    {
                        Console.WriteLine($"An error occurred while inserting [invoiceUpdateDate = {invoiceUpdateDate}] date in database for company with ID: {quickBook.QbInfo.CompanyID}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Synchronized invoices. Company with ID: {quickBook.QbInfo.CompanyID} is already up-to-date!");
                }

                Console.WriteLine($"Bills last updated at: {billUpdateDate.Value:MM/dd/yyyy HH:mm:ss} for company with ID: {companyQbDetails.CompanyID}!");
                Console.WriteLine("Synchronizing bills...");

                /* Process Bills */
                var bills = quickBook.GetBills(billUpdateDate);
                try
                {
                    foreach (var bill in bills)
                    {
                        int billID = Convert.ToInt32(bill.DocNumber);
                        if (billID != 0)
                        {
                            Console.WriteLine($"Syncing bill ID: {billID}...");
                            // Fail safe check to make sure that there is only one invoiceNumber per QuickBook invoice...
                            if (!processedBills.Contains(billID))
                            {
                                bool result = quickBook.MarkBillAsPaidInDB(billID, Convert.ToInt32(bill.DocNumber), bill.TotalAmt);
                                if (!result)
                                {
                                    Console.WriteLine("An error occurred while trying to mark bill as paid in DB!");
                                    throw new Exception();
                                }
                                billUpdateDate = bill.MetaData.LastUpdatedTime.ToUniversalTime();
                                processedBills.Add(billID);
                                Console.WriteLine($"Successfully synchronized bill ID: {billID}!");
                            }
                            else
                            {
                                // No need to update in Database...
                                billUpdateDate = bill.MetaData.LastUpdatedTime.ToUniversalTime();
                                Console.WriteLine($"Bill ID: {billID} is already synchronized!");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred!\n{e.StackTrace}");
                }

                if (billUpdateDate != null && billUpdateDate != billUpdateDateInDb && processedBills.Count > 0)
                {
                    bool result = quickBook.SetBillUpdateDateInDB(billUpdateDate.Value);
                    if (result)
                    {
                        Console.WriteLine($"Bills marked as updated till {billUpdateDate} for company with ID: {quickBook.QbInfo.CompanyID}!");
                    }
                    else
                    {
                        Console.WriteLine($"An error occurred while inserting [billUpdateDate = {billUpdateDate}] date in database for company with ID: {quickBook.QbInfo.CompanyID}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Synchronized bills. Company with ID: {quickBook.QbInfo.CompanyID} is already up-to-date!");
                }
            }
        }
    }
}

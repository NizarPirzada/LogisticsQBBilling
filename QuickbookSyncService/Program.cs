using Intuit.Ipp.Data;
using System;

namespace QuickbookSyncService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started Quickbook Sync Service!");

            int offset = -1;
            while (true)
            {
                ++offset;
                var service = new Service(offset);
                if (service.QbInfo == null)
                {
                    // No more records in database...
                    break;
                }
                else if (string.IsNullOrEmpty(service.QbInfo.RefreshToken) || string.IsNullOrEmpty(service.QbInfo.RealmID))
                {
                    // Selected company does not contian quickbook credentials...
                    Console.WriteLine($"QuickBook credentials for company with ID: {service.QbInfo.CompanyID} not found!");
                    continue;
                }

                DateTime syncDate = DateTime.Now;
                var customers = service.GetCustomersFromDb();
                foreach (var customer in customers)
                {
                    Customer qbCustomer = service.CreateCustomerInQuickbooks(customer.Name);
                    if (qbCustomer == null)
                    {
                        Console.WriteLine($"An error occurred while creating {customer.Id} - {customer.Name} customer in Quickbooks!");
                        continue;
                    }
                    Console.WriteLine($"{customer.Id} - {customer.Name} customer created in Quickbooks!");
                    syncDate = qbCustomer.MetaData.CreateTime;
                }

                Console.WriteLine($"Customers Synchronized at {syncDate} for company with ID: {service.QbInfo.CompanyID}!");
                Console.WriteLine($"Company with ID: {service.QbInfo.CompanyID} is synchronized with Quickbooks!");
            }
        }
    }
}

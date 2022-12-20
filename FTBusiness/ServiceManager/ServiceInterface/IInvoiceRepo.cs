using FTDTO.Invoice;
using System;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IInvoiceRepo
    {
        dynamic CreateInvoice(int jobID, int jobProductID, DateTime startDate, DateTime endDate, string qbInvoiceID, string userEmail);
        dynamic GetInvoices(InvoiceParameterDTO param);
        dynamic GetInvoicesPagingMetadata(InvoiceParameterDTO param);
        dynamic GetInvoice(int id);
        dynamic GetInvoiceDetail(int itemID);
        dynamic GetInvoicesHistory(int invoiceId);
        dynamic GetInvoiceDetail(string invoiceNumber);
        dynamic GetUnpaidInvoiceList(int customerId);
        dynamic GetReadyForPayment();
        dynamic GetInvoiceListByStatus(int status, string userEmail);
        dynamic GetInvoiceList(DateTime startDate, DateTime endDate);
        dynamic GetInvoicesNotMarkedForPayment(DateTime startDate, DateTime endDate, string userEmail);
        dynamic GetJobListForInvoice(DateTime startDate, DateTime endDate, string userEmail);
        dynamic GetTentativeInvoice(int jobID, int jobProductID, DateTime startDate, DateTime endDate);
        dynamic GetInvoiceDetails(int invoiceNumber, string userEmail);
        dynamic GenerateInvoiceDetails(int jobID, int jobProductID, int invoiceID, string userEmail, DateTime? startDate, DateTime? endDate);
        dynamic MarkInvoiceAsPaid(string checkNumber, int invoiceID);
        dynamic MarkInvoiceAsFunded(int invoiceID, string userEmail);
        dynamic SetIsReadyForPayment(int invoiceID, string userEmail);
        dynamic MarkInvoiceReadyForPayment(int invoiceID);
        dynamic UpdateInvoice(int userId, int truckID, int defaultDriverID, string code, string description);
        dynamic UpdateVoidStatus(int invoiceID, bool status, string userEmail);
        dynamic UpdateFundedStatus(int invoiceID, bool status, string userEmail);
        dynamic UpdateInvoice(int ticketID, string userEmail);
        dynamic GetInvoiceDetailsQB(int jobID, int jobProductID, DateTime startDate, DateTime endDate);
    }
}

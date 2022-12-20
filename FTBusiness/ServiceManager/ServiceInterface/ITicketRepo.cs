using FTDTO.Ticket;
using System;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface ITicketRepo
    {
        dynamic GetTicketById(int id);
        dynamic GetTickets(int ticketCode);
        dynamic GetAllTickets(int jobId);
        dynamic GetTicketsNotInvoiced1(DateTime endDate);
        dynamic GetTicketsNotPaid1(DateTime endDate);
        dynamic GetTicketsNotInvoicedByJob(int jobId);
        dynamic GetTicketsNotInvoicedByTicketNumber(string ticketNumber);
        dynamic GetTicketPaymentCount1(int ticketId);
        dynamic GetTicketLineItems1(int ticketId);
        dynamic GetTicketLineItemDetails(int ticketLineItemId);
        dynamic GetTicketDetails(int ticketId);
        dynamic GetTicketDetails(string code, int jobId);
        dynamic GetTicketsHistory(int ticketId);
        dynamic UpdateTicket(string userEmail, int ticketId, int jobId, int jobProductId, string code, string description, string creationDate);
        dynamic UpdateTicketLineItem(string userEmail, int ticketLineItemId, int ticketId, int driverId, int truckId, double quantity, double pricePerUnit, double driverPrice);
        public dynamic DeleteTicketLineItemDetail(int ticketLineItemId);
        dynamic GetTicketsNotInvoicedPaging(string userEmail, DateTime endDate, int offset, int limit, bool invoiced = false);
        dynamic GetTicketsSummary(string userEmail);
        dynamic GetPayableTickets(PayableTicketFilterDTO filters, string userEmail);
        dynamic GetPayableTicketItems(PayableTicketFilterDTO filters, string userEmail);
        dynamic DeleteTicket(int id);
    }
}

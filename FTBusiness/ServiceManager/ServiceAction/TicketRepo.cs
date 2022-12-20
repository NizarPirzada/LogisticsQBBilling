using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.Ticket;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class TicketRepo : Repository<Ticket>, ITicketRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public TicketRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetTicketById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TicketID", id)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_By_Id", parameters);
        }

        public dynamic GetTickets(int ticketCode)
        {
            SqlParameter[] spParams = new SqlParameter[]
           {
                new SqlParameter("@Ticket_Code", ticketCode)
           };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Find_Tickets", spParams);
        }


        public dynamic GetAllTickets(int itemID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", itemID)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_List", spParams);
        }

        public dynamic GetTicketsNotInvoiced1(DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@End_Date", endDate)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Tickets_Not_Invoiced", spParams);
        }

        public dynamic GetTicketsNotInvoicedPaging(string userEmail, DateTime endDate, int offset, int limit, bool invoiced = false)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@End_Date", endDate),
                new SqlParameter("@Offset", offset),
                new SqlParameter("@Limit", limit),
                new SqlParameter("@Invoiced", invoiced)
            };

            return adoRepo.GetResultPaging(currentContext.Database.GetConnectionString(), "sproc_Get_Tickets_Not_Invoiced_Paging", spParams);
        }

        public dynamic GetTicketsNotInvoicedByJob(int jobId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_Id", jobId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Tickets_Not_Invoiced_By_Job", spParams);
        }

        public dynamic GetTicketsNotInvoicedByTicketNumber(string ticketNumber)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_Number", ticketNumber)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Tickets_Not_Invoiced_By_Code", spParams);
        }


        public dynamic GetTicketsNotPaid1(DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@End_Date", endDate)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Tickets_Not_Paid", spParams);
        }

        public dynamic GetTicketPaymentCount1(int ticketId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_ID", ticketId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_Payment_Count", spParams);
        }

        public dynamic GetTicketLineItems1(int ticketId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_ID", ticketId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_Line_Items", spParams);
        }

        public dynamic GetTicketLineItemDetails(int ticketLineItemId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_Line_Item_ID", ticketLineItemId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_Line_Item_Detail", spParams);
        }

        public dynamic GetTicketDetails(int ticketId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_ID", ticketId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_Detail", spParams);
        }

        public dynamic GetTicketDetails(string code, int jobId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Code", code),
                new SqlParameter("@JobID", jobId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_Detail", spParams);
        }

        public dynamic GetTicketsHistory(int ticketId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_ID", ticketId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Ticket_History", spParams);
        }


        public dynamic UpdateTicket(string userEmail, int ticketId, int jobId, int jobProductId, string code, string description, string creationDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_ID", ticketId),
                new SqlParameter("@Job_ID", jobId),
                new SqlParameter("@Job_Product_ID", jobProductId),
                new SqlParameter("@Code", code),
                new SqlParameter("@Description", description),
                new SqlParameter("@Creation_Date", creationDate),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Ticket", spParams);
        }


        public dynamic UpdateTicketLineItem(string userEmail, int ticketLineItemId, int ticketId, int driverId, int truckId, double quantity, double pricePerUnit, double driverPrice)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_Line_Item_ID", ticketLineItemId),
                new SqlParameter("@Ticket_ID", ticketId),
                new SqlParameter("@Driver_ID", driverId),
                new SqlParameter("@Truck_ID", truckId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Price_Per_Unit", pricePerUnit),
                new SqlParameter("@Driver_Price_Per_Unit", driverPrice),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Ticket_Line_Item", spParams);
        }

        public dynamic DeleteTicketLineItemDetail(int ticketLineItemId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Ticket_Line_Item_ID", ticketLineItemId)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Remove_Ticket_Line_Item", spParams);
        }

        public dynamic GetTicketsSummary(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@UserEmail", userEmail)
           };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Tickets_Summary", parameters);
        }

        public dynamic GetPayableTickets(PayableTicketFilterDTO filters, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DriverType", filters.DriverType),
                new SqlParameter("@StartDate", filters.StartDate),
                new SqlParameter("@EndDate", filters.EndDate),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Payable_Tickets", parameters);
        }

        public dynamic GetPayableTicketItems(PayableTicketFilterDTO filters, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DriverType", filters.DriverType),
                new SqlParameter("@StartDate", filters.StartDate),
                new SqlParameter("@EndDate", filters.EndDate),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Payable_Ticket_Items", parameters);
        }

        public dynamic DeleteTicket(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TicketID", id)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Delete_Ticket", parameters);
        }
    }
}

using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.Invoice;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class InvoiceRepo : Repository<Invoice>, IInvoiceRepo
    {
        readonly SLCContext currentContext;
        readonly AdoRepository adoRepo;
        public InvoiceRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetInvoices(InvoiceParameterDTO param)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Paid", param.Paid),
                new SqlParameter("@Funded", param.Funded),
                new SqlParameter("@StartDate", param.StartDate),
                new SqlParameter("@EndDate", param.EndDate),
                new SqlParameter("@Offset", param.Offset),
                new SqlParameter("@Limit", param.Limit)
            };
            return adoRepo.GetResultPaging(currentContext.Database.GetConnectionString(), "sproc_Get_Invoices", parameters);
        }

        public dynamic GetInvoicesPagingMetadata(InvoiceParameterDTO param)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Paid", param.Paid),
                new SqlParameter("@Funded", param.Funded),
                new SqlParameter("@StartDate", param.StartDate),
                new SqlParameter("@EndDate", param.EndDate),
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoices_Paging_Metadata", parameters);
        }

        public dynamic GetInvoice(int id)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", id)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice", parameters);
        }

        public dynamic GetInvoicesNotMarkedForPayment(DateTime startDate, DateTime endDate, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoices_Not_Marked_For_Payment", spParams);
        }

        public dynamic MarkInvoiceReadyForPayment(int invoiceID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Invoice_ID", invoiceID)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Mark_Ready_For_Payment", spParams);
        }
        public dynamic MarkInvoiceAsPaid(string checkNumber, int invoiceID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Invoice_ID", invoiceID),
                new SqlParameter("@Check_Number", checkNumber)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Mark_Invoice_Paid", spParams);
        }

        public dynamic MarkInvoiceAsFunded(int invoiceID, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", invoiceID),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Mark_Invoice_As_Funded", parameters);
        }

        public dynamic SetIsReadyForPayment(int invoiceID, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", invoiceID),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Set_Is_Ready_For_Payment", parameters);
        }

        public dynamic CreateInvoice(int jobID, int jobProductID, DateTime startDate, DateTime endDate, string qbInvoiceID, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobID),
                new SqlParameter("@Job_Product_ID", jobProductID),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate),
                new SqlParameter("@QbInvoiceID", qbInvoiceID),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Create_Invoice", spParams);
        }

        public dynamic GetInvoiceDetailsQB(int jobID, int jobProductID, System.DateTime startDate, System.DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobID),
                new SqlParameter("@Job_Product_ID", jobProductID),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate)

            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_QB", spParams);
        }

        public dynamic GetInvoiceList(System.DateTime startDate, System.DateTime endDate)
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_List", null);
        }

        public dynamic GetJobListForInvoice(DateTime startDate, DateTime endDate, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Jobs_Ready_For_Invoice", spParams);
        }

        public dynamic GetTentativeInvoice(int jobID, int jobProductID, System.DateTime startDate, System.DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Job_ID", jobID),
                new SqlParameter("@Job_Product_ID", jobProductID),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Tentative_Invoice", spParams);
        }

        public dynamic GetInvoiceDetail(int itemID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Invoice_ID", itemID)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_Detail", spParams);
        }


        public dynamic GetInvoicesHistory(int invoiceId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Invoice_ID", invoiceId)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_History", spParams);
        }

        public dynamic GetUnpaidInvoiceList(int customerId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Customer_ID", customerId)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Unpaid_Invoices", spParams);
        }

        public dynamic GetReadyForPayment()
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_Ready_For_Payment", null);
        }

        public dynamic GetInvoiceListByStatus(int status, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Status", status),
               new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoices_By_Status", spParams);
        }

        public dynamic GetInvoiceDetail(string invoiceNumber)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Number", invoiceNumber)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_Detail", spParams);
        }

        public dynamic GetInvoiceDetails(int invoiceNumber, string userEmail)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceNumber", invoiceNumber),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Invoice_Details", parameters);
        }

        public dynamic GenerateInvoiceDetails(int jobID, int jobProductID, int invoiceID, string userEmail, DateTime? startDate, DateTime? endDate)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@JobID", jobID),
                new SqlParameter("@JobProductID", jobProductID),
                new SqlParameter("@InvoiceID", invoiceID),
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Generate_Invoice_Details", parameters);
        }

        public dynamic UpdateInvoice(int userId, int truckID, int defaultDriverID, string code, string description)
        {
            SqlParameter[] spParams;
            if (defaultDriverID > 0)
            {
                spParams = new SqlParameter[]
                {
                    new SqlParameter("@Truck_ID", truckID),
                    new SqlParameter("@Default_Driver_ID", defaultDriverID),
                    new SqlParameter("@Code", code),
                    new SqlParameter("@Description", description),
                    new SqlParameter("@Created_By", userId),
                    new SqlParameter("@Creation_Date", DateTime.UtcNow),
                    new SqlParameter("@Updated_By", userId),
                    new SqlParameter("@Last_Updated_Date", DateTime.UtcNow)
                };
            }
            else
            {
                spParams = new SqlParameter[]
                {
                    new SqlParameter("@Truck_ID", truckID),
                    new SqlParameter("@Code", code),
                    new SqlParameter("@Description", description)
                };
            }
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Invoice_Truck", spParams);
        }

        public dynamic UpdateVoidStatus(int invoiceID, bool status, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", invoiceID),
                new SqlParameter("@Status", status),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Update_Invoice_Void_Status", parameters);
        }

        public dynamic UpdateFundedStatus(int invoiceID, bool status, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", invoiceID),
                new SqlParameter("@Status", status),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Update_Invoice_Funded_Status", parameters);
        }

        public dynamic UpdateInvoice(int ticketID, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TicketID", ticketID),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Update_Invoice", parameters);
        }
    }
}

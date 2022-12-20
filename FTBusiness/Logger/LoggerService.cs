using FTBusiness.BaseRepository;
using FTData.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTBusiness.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly SLCContext _ctx;
        private readonly AdoRepository _adoRepo;

        public LoggerService(SLCContext ctx, AdoRepository adoRepo)
        {
            _ctx = ctx;
            _adoRepo = adoRepo;
        }

        public dynamic InsertLog(int statusCode, string message, string stackTrace, string innerExceptionMessage, string innerExceptionStackTrace, string path, string level, string userEmail, DateTimeOffset timestamp)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StatusCode", statusCode),
                new SqlParameter("@Message", message),
                new SqlParameter("@StackTrace", stackTrace),
                new SqlParameter("@InnerExceptionMessage", innerExceptionMessage),
                new SqlParameter("@InnerExceptionStackTrace", innerExceptionStackTrace),
                new SqlParameter("@Path", path),
                new SqlParameter("@Level", level),
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@Timestamp", timestamp)
            };

            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Insert_Log", parameters);
        }
    }
}

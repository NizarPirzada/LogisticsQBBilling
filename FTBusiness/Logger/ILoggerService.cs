using System;

namespace FTBusiness.Logger
{
    public interface ILoggerService
    {
        dynamic InsertLog(int statusCode, string message, string stackTrace, string innerExceptionMessage, string innerExceptionStackTrace, string path, string level, string userEmail, DateTimeOffset timestamp);
    }
}

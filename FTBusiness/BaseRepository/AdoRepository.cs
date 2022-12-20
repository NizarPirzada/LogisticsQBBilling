using FTData.BaseTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Dynamic;
using Microsoft.Azure.Services.AppAuthentication;
using System.Text.Json;

namespace FTBusiness.BaseRepository
{
    public class DataReponse
    {
        public int RecordsCount { get; set; }
        public dynamic Data { get; set; }
    }

    public static class DataSetExtensions
    {
        public static List<dynamic> ToDynamic(this DataSet ds)
        {
            var dSList = new List<dynamic>();
            foreach (DataTable dt in ds.Tables)
            {
                var dTList = new List<ExpandoObject>();
                foreach (DataRow row in dt.Rows)
                {
                    dynamic dRObj = new ExpandoObject();
                    dTList.Add(dRObj);
                    foreach (DataColumn column in dt.Columns)
                    {
                        var dic = (IDictionary<string, object>)dRObj;
                        dic[column.ColumnName] = Convert.IsDBNull(row[column]) ? "" : row[column];
                    }
                }
                dSList.Add(dTList);
            }
            return dSList;
        }
    }

    public static class DataTableExtensions
    {
        public static List<dynamic> ToDynamic(this DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = Convert.IsDBNull(row[column]) ? "" : row[column];
                }
            }
            return dynamicDt;
        }
    }

    public class AdoRepository
    {

        public dynamic GetResult(string connString, string procName, params SqlParameter[] paramters)
        {
            dynamic result = null;
            var dataset = ExecuteProcedureReturnDataSet(connString, procName, paramters);
            if (dataset?.Tables.Count > 0)
            {
                result = dataset.Tables[0].ToDynamic();
            }
            return result;
        }

        public dynamic GetResults(string connString, string procName, params SqlParameter[] paramters)
        {
            dynamic result = null;
            var dataset = ExecuteProcedureReturnDataSet(connString, procName, paramters);
            if (dataset?.Tables.Count > 0)
            {
                result = dataset.ToDynamic();
            }
            return result;
        }

        public dynamic GetResultPaging(string connString, string procName, params SqlParameter[] paramters)
        {
            dynamic result = null;
            //KeyValuePair<int, dynamic> test = new KeyValuePair<int, dynamic>();
            //test.Key
            var dataset = ExecuteProcedureReturnDataSet(connString, procName, paramters);
            if (dataset?.Tables.Count > 0 && dataset?.Tables[0].Rows.Count > 0)
            {
                int rowCount = Convert.ToInt32(dataset.Tables[1].Rows[0][0]);
                ////result = new KeyValuePair<int, dynamic>(rowCount, dataset.Tables[0].ToDynamic());
                //result = dataset.Tables[0].ToDynamic();
                DataReponse responseObj = new DataReponse();
                responseObj.RecordsCount = rowCount;
                //var dataDictionary = new KeyValuePair<KeyValuePair<string, int>, List<object>>(new KeyValuePair<string, int>( "RecordsCount",rowCount ), dataset.Tables[0].ToDynamic());
                //var dataDictionary = new Dictionary<dynamic, List<object>>() { { ob, dataset.Tables[0].ToDynamic() } };
                //dataDictionary.Add(new Dictionary<string, List<object>>() });
                responseObj.Data = dataset.Tables[0].ToDynamic();
                result = responseObj;
                //result.Key = rowCount;
            }
            return result;
        }


        private DataSet ExecuteProcedureReturnDataSet(string connString, string procName, params SqlParameter[] paramters)
        {
            DataSet result = null;
            using (var sqlConnection = new SqlConnection(connString))
            {
                //sqlConnection.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;

                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = procName;
                        command.CommandTimeout = 120;
                        if (paramters != null)
                        {
                            command.Parameters.AddRange(paramters);
                        }
                        result = new DataSet();
                        sda.Fill(result);
                    }
                }
            }
            return result;
        }

        public int ExecuteNonQuery(string connString, string procName, params SqlParameter[] paramters)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                var command = new SqlCommand(procName, sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = procName;
                if (paramters != null)
                {
                    command.Parameters.AddRange(paramters);
                }

                var result = Convert.ToInt32(command.ExecuteScalar());
                //var result = command.ExecuteNonQuery();
                sqlConnection.Close();
                return result;
            }
        }

    }
}


using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Dapper.SqlMapper;

namespace Shared.DataAccess
{
    public abstract class DapperBaseService(IConfiguration _configuration)
    {
        protected async Task<List<T>> GetQueryResultAsync<T>(string sQuery, object parameters = null!, IDbTransaction transaction = null!)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
            using var sqlConnection = new SqlConnection(ConnectionString);
            {
                try
                {
                    sqlConnection.Open();
                    var command = new CommandDefinition(sQuery, parameters, transaction);
                    var result = await sqlConnection.QueryAsync<T>(command);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    // _logger.LogError("Error on Database Operation. Error Details As: {StackTrace}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        protected async Task<T> GetQueryFirstOrDefaultResultAsync<T>(string sQuery, object parameters, IDbTransaction transaction = null!)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
            using var sqlConnection = new SqlConnection(ConnectionString);
            {
                try
                {
                    sqlConnection.Open();
                    var command = new CommandDefinition(sQuery, parameters, transaction);
                    var result = await sqlConnection.QueryFirstOrDefaultAsync<T>(command);
                    return result;
                }
                catch (Exception ex)
                {
                    // _logger.LogError("Error on Database Operation. Error Details As: {StackTrace}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        protected async Task<int> ExecuteAsync(string sQuery, object parameters, IDbTransaction transaction = null!)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
            using var sqlConnection = new SqlConnection(ConnectionString);
            {
                try
                {
                    sqlConnection.Open();
                    var command = new CommandDefinition(sQuery, parameters, transaction);
                    var rowsAffected = await sqlConnection.ExecuteAsync(command);
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    // _logger.LogError("Error on Database Operation. Error Details As: {StackTrace}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        protected async Task<GridReader> GetFromMultipleQuery(string sQuery, object parameters, IDbTransaction transaction = null!)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var sqlConnection = new SqlConnection(ConnectionString);


            var command = new CommandDefinition(sQuery, parameters, transaction);
            var result = await sqlConnection.QueryMultipleAsync(command);
            return result;
        }
        protected async Task<DataTable> Execute(string sQuery)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
            using var connection = new SqlConnection(ConnectionString);
            {
                connection.Open();
                var dt = new DataTable();
                var cmd = new SqlCommand(sQuery, connection) { CommandType = CommandType.Text, CommandTimeout = 0 };
                var dr = await cmd.ExecuteReaderAsync();
                dt.Load(dr);
                cmd.Dispose();
                connection.Close();
                return dt;
            }
        }
        protected async Task<DataSet> ExecuteQuery(string sQuery)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
            using var connection = new SqlConnection(ConnectionString);
            {
                connection.Open();
                var cmd = new SqlCommand() { CommandText = sQuery, Connection = connection, CommandTimeout = 0 };
                var ds = new DataSet();
                var da = new SqlDataAdapter(cmd.CommandText, cmd.Connection) { SelectCommand = cmd };
                await Task.Run(() => da.Fill(ds));
                cmd.Dispose();
                da.Dispose();
                return ds;
            }
        }
    }
}

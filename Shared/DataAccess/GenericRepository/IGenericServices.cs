using Shared.Model;
using System.Data;

namespace Shared.DataAccess.GenericRepository
{
    public interface IGenericServices
    {
        Task<List<T>> GetAllAsync<T>(string spName, object obj, CommandType queryType = CommandType.StoredProcedure);
        Task<T> GetAsync<T>(string spName, object obj, CommandType queryType = CommandType.StoredProcedure);
        Task<List<object>> GetFromMultipleQuery<T0, T1>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        Task<List<object>> GetFromMultipleQuery<T0, T1, T2>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        Task<List<object>> GetFromMultipleQuery<T0, T1, T2, T3>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        Task<SystemResponseModel> UpdateAsync(string spName, object input, CommandType queryType = CommandType.StoredProcedure);
        Task<SystemResponseModel> InsertAsync(string spName, object input, CommandType queryType = CommandType.StoredProcedure);
        Task<SystemResponseModel> DeleteAsync(string spName, object input, CommandType queryType = CommandType.StoredProcedure);
    }
}

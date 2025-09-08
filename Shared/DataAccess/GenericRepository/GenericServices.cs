using Shared.DataAccess.Dapper;
using Shared.Model;
using System.Data;

namespace Shared.DataAccess.GenericRepository
{
    public class GenericServices :IGenericServices
    {
        private readonly IDapperServices _dapperService;

        public GenericServices(IDapperServices dapperService)
        {
            _dapperService = dapperService;
        }

        public async Task<List<T>> GetAllAsync<T>(string spName, object obj, CommandType queryType)
            => await _dapperService.GetQueryResultAsync<T>(spName, obj, queryType);


        public async Task<T> GetAsync<T>(string spName, object obj, CommandType queryType)
            => await _dapperService.GetQueryFirstOrDefaultResultAsync<T>(spName, obj, queryType);

        public async Task<List<object>> GetFromMultipleQuery<T0, T1>(string sqlQuery, object sqlParam, CommandType queryType)
             => await _dapperService.GetFromMultipleQuery<T0, T1>(sqlQuery, sqlParam, queryType);

        public async Task<List<object>> GetFromMultipleQuery<T0, T1, T2>(string sqlQuery, object sqlParam, CommandType queryType)
            => await _dapperService.GetFromMultipleQuery<T0, T1, T2>(sqlQuery, sqlParam, queryType);

        public async Task<List<object>> GetFromMultipleQuery<T0, T1, T2, T3>(string sqlQuery, object sqlParam, CommandType queryType)
            => await _dapperService.GetFromMultipleQuery<T0, T1, T2, T3>(sqlQuery, sqlParam, queryType);

        public async Task<SystemResponseModel> UpdateAsync(string spName, object input, CommandType queryType)
        {
            return await _dapperService.ExecuteAsync(spName, input, queryType);
        }

        public async Task<SystemResponseModel> InsertAsync(string spName, object input, CommandType queryType)
        {
            return await _dapperService.ExecuteAsync(spName, input, queryType);
        }

        public async Task<SystemResponseModel> DeleteAsync(string spName, object input, CommandType queryType)
        {
            return await _dapperService.ExecuteAsync(spName, input, queryType);
        }
    }
}

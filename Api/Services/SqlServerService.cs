using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Api.Services
{
    public class SqlServerService(string connectionString) : ISqlServerService
    {
        private readonly string _connectionString = connectionString;

        public async Task<string> GetDataAsync()
        {
            // Example: fetch a single value from SQL Server
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT TOP 1 name FROM sys.databases", conn);
            var result = await cmd.ExecuteScalarAsync();
            return result?.ToString() ?? string.Empty;
        }

        public async Task<List<List<dynamic>>> SelectTopReputableByLocation(
            int top,
            string startsWith,
            string contains,
            string endsWith)
        {
            var results = new List<List<dynamic>>();
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                await GetQueryText(nameof(SelectTopReputableByLocation))
                , conn);
            cmd.Parameters.Add(
                new SqlParameter($"@{nameof(top)}"
                , System.Data.SqlDbType.Int)
                { Value = top });
            cmd.Parameters.Add(
                new SqlParameter($"@{nameof(startsWith)}"
                , System.Data.SqlDbType.NVarChar)
                { Value = startsWith });
            cmd.Parameters.Add(
                new SqlParameter($"@{nameof(contains)}"
                , System.Data.SqlDbType.NVarChar)
                { Value = contains });
            cmd.Parameters.Add(
                new SqlParameter($"@{nameof(endsWith)}"
                , System.Data.SqlDbType.NVarChar)
                { Value = endsWith });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                dynamic[] row = new dynamic[reader.FieldCount];
                reader.GetValues(row);
                results.Add([.. row]);
            }
            return results;
        }

        public async Task<List<List<dynamic>>> SelectTopVotedPosts(int top)
        {
            var results = new List<List<dynamic>>();
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                await GetQueryText(nameof(SelectTopVotedPosts))
                , conn);
            cmd.Parameters.Add(
                new SqlParameter($"@{nameof(top)}"
                , System.Data.SqlDbType.Int)
                { Value = top });
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                dynamic[] row = new dynamic[reader.FieldCount];
                reader.GetValues(row);
                results.Add([.. row]);
            }
            return results;
        }

        private async Task<string> GetQueryText(string queryname)
        {
            string filePath = $"{queryname}.sql";
            if (!File.Exists(filePath)) throw new FileNotFoundException(
                message: $"{nameof(FileNotFoundException)}:{filePath}",
                fileName: filePath);
            return await File.ReadAllTextAsync(filePath);
        }
    }
}

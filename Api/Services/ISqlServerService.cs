namespace Api.Services
{
    public interface ISqlServerService
    {
        // Example method to query SQL Server
        Task<string> GetDataAsync();
        public Task<List<List<dynamic>>> SelectTopReputableByLocation(
            int top,
            string startsWith,
            string contains,
            string endsWith);

        public Task<List<List<dynamic>>> SelectTopVotedPosts(
            int top);
    }
}

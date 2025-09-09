namespace Backend.Services
{
    public interface ISqlServerService
    {
        public Task<List<List<dynamic>>> SelectTopReputableByLocation(
            int top,
            string startsWith,
            string contains,
            string endsWith);

        public Task<List<List<dynamic>>> SelectTopVotedPosts(
            int top);
    }
}

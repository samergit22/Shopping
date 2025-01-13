namespace HirePlatform.IServices
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cachKey, object response, TimeSpan timeSpan);
        Task<string> GetCacheResponseAsync(string cachKey);
    }
}

namespace Framework.Core.Interfaces
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T data);
       
        T Set<T>(string key, T data, bool returnIfExists);
       
        T Get<T>(string key);
        
        void Remove(string key);
    }
}

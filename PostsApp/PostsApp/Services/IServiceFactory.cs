namespace PostsApp.Services
{
    public interface IServiceFactory<T>
    {
        T UserInitiated { get; }
        T Background { get; }
        T Speculative { get; }
        T Offline { get; }
    }
}
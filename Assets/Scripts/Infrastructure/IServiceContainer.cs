public interface IServiceContainer
{
    void FixedUpdate();
    TService Get<TService>() where TService : class, IService;
    void RegisterService<TType>(TType service) where TType : IService;
    void Update();
}

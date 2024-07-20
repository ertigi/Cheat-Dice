public interface IService
{

}

public interface IUpdateService : IService
{
    void Update();
}

public interface IFixedUpdateService : IService
{
    void FixedUpdate();
}
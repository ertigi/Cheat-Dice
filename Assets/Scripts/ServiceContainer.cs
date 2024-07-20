using System;
using System.Collections.Generic;

public class ServiceContainer : IServiceContainer
{
    private Dictionary<Type, IService> _servicesMap;

    private List<IUpdateService> _updaptableServices;
    private List<IFixedUpdateService> _fixedUpdaptableServices;

    public ServiceContainer()
    {
        _servicesMap = new Dictionary<Type, IService>();
        _updaptableServices = new List<IUpdateService>();
        _fixedUpdaptableServices = new List<IFixedUpdateService>();
    }

    public void RegisterService<TType>(TType service) where TType : IService
    {
        _servicesMap.Add(typeof(TType), service);

        if (service is IUpdateService)
            _updaptableServices.Add(service as IUpdateService);
        if (service is IFixedUpdateService)
            _fixedUpdaptableServices.Add(service as IFixedUpdateService);
    }

    public TService Get<TService>() where TService : class, IService
    {
        if (!_servicesMap.ContainsKey(typeof(TService)))
            throw new NoRegisteredServiceException($"No registered service with type {typeof(TService)}");

        return _servicesMap[typeof(TService)] as TService;
    }

    public void Update()
    {
        if (_updaptableServices.Count != 0)
            foreach (IUpdateService service in _updaptableServices)
                service.Update();
    }

    public void FixedUpdate()
    {
        if (_fixedUpdaptableServices.Count != 0)
            foreach (IFixedUpdateService service in _fixedUpdaptableServices)
                service.FixedUpdate();
    }
}

[Serializable]
public class NoRegisteredServiceException : Exception
{
    public NoRegisteredServiceException(string message) : base(message) { }
}
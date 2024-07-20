using UnityEngine;

public class EnteringPoint : MonoBehaviour, ICoroutineRunner
{
    private ServiceContainer _serviceContainer;

    private void Awake()
    {
        _serviceContainer = new ServiceContainer();


    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }
}

using UnityEngine;

public class EnteringPoint : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private Dice _dicePrefab;
    [SerializeField] private ThrowSettings _throwSettings;

    private ServiceContainer _serviceContainer;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _serviceContainer = new ServiceContainer();

        InitializeServices();
    }

    private void Update()
    {
        _serviceContainer.Update();
    }

    private void FixedUpdate()
    {
        _serviceContainer.FixedUpdate();
    }

    private void InitializeServices()
    {
        _serviceContainer.RegisterService<IThrowRecorder>(new ThrowRecorder(this));
        _serviceContainer.RegisterService<IDiceContriller>(new DiceContriller(_dicePrefab, _throwSettings, _serviceContainer.Get<IThrowRecorder>()));
    }
}

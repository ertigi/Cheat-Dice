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

        _serviceContainer.RegisterService(new ThrowRecorder(this));
        _serviceContainer.RegisterService(new DiceContriller(_dicePrefab, _throwSettings, _serviceContainer.Get<ThrowRecorder>()));
    }

    private void Update()
    {
        _serviceContainer.Update();
    }

    private void FixedUpdate()
    {
        _serviceContainer.FixedUpdate();
    }
}

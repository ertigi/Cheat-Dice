using UnityEngine;

public class EnteringPoint : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private Dice _dicePrefab;
    [SerializeField] private ThrowSettings _throwSettings;
    [SerializeField] private RecorderSettings _recorderSettings;

    private ServiceContainer _serviceContainer;

    private void Awake()
    {
        _serviceContainer = new ServiceContainer();

        _serviceContainer.RegisterService(new DiceContriller(_dicePrefab, _throwSettings, _recorderSettings, this));
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

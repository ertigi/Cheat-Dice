using System.Collections.Generic;
using UnityEngine;

public class DiceContriller : IService, IUpdateService
{
    private DiceFactory _diceFactory;
    private List<Dice> _dices;
    private ThrowSettings _throwSettings;

    public DiceContriller(Dice dicePrefab, ThrowSettings throwSettings)
    {
        _diceFactory = new DiceFactory(dicePrefab);
        _dices = new List<Dice>();
        _throwSettings = throwSettings;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowInfo throwInfo = new ThrowInfo();

            int rand = Random.Range(2, _throwSettings.StartPositions.Count + 1);

            for (int i = 0; i < rand; i++)
            {
                throwInfo.TargetFaces.Add(1);
            }

            ThrowDices(throwInfo);
        }
    }

    public void ThrowDices(ThrowInfo throwInfo)
    {
        if (_dices.Count > 0)
            RemoveOldDices();

        //  if (_recorder.isPlayAnimation)
        //      _recorder.BreakAnimation();     

        CreateDices(throwInfo.TargetFaces.Count);
        InitThrow();

        // _recorder.Record(_dices);
        // dice view rotate
        // _recorder.Play()
    }

    private void CreateDices(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _dices.Add(_diceFactory.Create());
        }
    }

    private void RemoveOldDices()
    {
        for (int i = 0; i < _dices.Count; i++)
        {
            _diceFactory.Destroy(_dices[i]);
        }

        _dices.Clear();
    }

    private void InitThrow()
    {
        Vector3 position, force, torgue;
        Quaternion rotation;

        for (int i = 0; i < _dices.Count; i++)
        {
            position = _throwSettings.StartPositions[i] + Vector3.forward * Random.Range(-2, 1);

            force = _throwSettings.ThrowDirection;
            force *= _throwSettings.ThrowForce * Random.Range(1f, 1f + _throwSettings.ThrowForceRandScale);

            torgue = new Vector3(Random.Range(-_throwSettings.MaxTorqueForce, _throwSettings.MaxTorqueForce),
                                 Random.Range(-_throwSettings.MaxTorqueForce, _throwSettings.MaxTorqueForce),
                                 Random.Range(-_throwSettings.MaxTorqueForce, _throwSettings.MaxTorqueForce));

            rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360),
                                                    Random.Range(0, 360),
                                                    Random.Range(0, 360)));

            _dices[i].Throw(position, rotation, force, torgue);
        }
    }
}

public class ThrowInfo
{
    public List<int> TargetFaces;

    public ThrowInfo()
    {
        TargetFaces = new List<int>();
    }
}

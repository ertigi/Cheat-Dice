using System.Collections.Generic;

public class DiceContriller : IService
{
    private DiceFactory _diceFactory;
    private List<Dice> _dices;

    public DiceContriller(Dice dicePrefab)
    {
        _diceFactory = new DiceFactory(dicePrefab);
        _dices = new List<Dice>();
    }

    public void ThrowDice(ThrowInfo throwInfo)
    {
        if (_dices.Count > 0)
            RemoveOldDices();

        //  if (_recorder.isPlayAnimation)
        //      _recorder.BreakAnimation();     

        CreateDices(throwInfo);

        // _recorder.Record(_dices);
        // dice view rotate
        // _recorder.Play()
    }

    private void CreateDices(ThrowInfo throwInfo)
    {
        foreach (var item in throwInfo.TargetFaces)
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
}

public class ThrowInfo
{
    public List<int> TargetFaces;

    public ThrowInfo()
    {
        TargetFaces = new List<int>();
    }
}

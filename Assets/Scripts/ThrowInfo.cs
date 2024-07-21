using System.Collections.Generic;

public class ThrowInfo : IThrowInfo
{
    private List<int> _targetFaces;

    public List<int> TargetFaces => _targetFaces;

    public ThrowInfo()
    {
        _targetFaces = new List<int>();
    }
}
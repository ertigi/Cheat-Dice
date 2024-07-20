using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowSettings", menuName = "Add/ThrowSettings", order = 1)]
public class ThrowSettings : ScriptableObject
{
    [SerializeField] private Vector3 _throwDirection;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _maxTorqueForce;
    [Range(0f, 1f)][SerializeField] private float _throwForceRandScale;
    [SerializeField] private List<Vector3> _startPositions = new List<Vector3>();

    public Vector3 ThrowDirection => _throwDirection.normalized;
    public float ThrowForce => _throwForce;
    public float ThrowForceRandScale => _throwForceRandScale;
    public float MaxTorqueForce => _maxTorqueForce;
    public List<Vector3> StartPositions => _startPositions;
}
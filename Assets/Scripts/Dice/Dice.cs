using System;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public bool IsSleeping => _rigidbody.IsSleeping();

    public void EnablePhysics(bool value)
    {
        _rigidbody.useGravity = value;
        _rigidbody.isKinematic = !value;
    }

    public void Throw(Vector3 position, Quaternion rotation, Vector3 force, Vector3 torgue)
    {
        transform.SetPositionAndRotation(position, rotation);
        _rigidbody.velocity = force;
        _rigidbody.AddTorque(torgue);
    }
}
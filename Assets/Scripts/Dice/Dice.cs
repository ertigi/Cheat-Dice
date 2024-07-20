using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    public void EnablePhysics(bool value)
    {
        _rigidbody.useGravity = value;
        _rigidbody.isKinematic = !value;
    }
}
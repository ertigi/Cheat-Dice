using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _view;
    [SerializeField] private List<Transform> _diceFaces = new List<Transform>();
    private int _upperFace;
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

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    public void ResetDiceView()
    {
        _view.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void FindUpperFace()
    {
        _upperFace = 0;
        for (int i = 1; i < _diceFaces.Count; i++)
        {
            if (_diceFaces[i].transform.position.y > _diceFaces[_upperFace].transform.position.y)
                _upperFace = i;
        }
    }

    public void RotateToFace(int targetFaceIndex)
    {
        if (targetFaceIndex == _upperFace)
            return;

        Vector3 currentTopFacePosition = _diceFaces[_upperFace].localPosition;
        Vector3 targetFacePosition = _diceFaces[targetFaceIndex].localPosition;

        Vector3 rotationAxis = Vector3.Cross(currentTopFacePosition, targetFacePosition);
        float rotationAngle = Vector3.SignedAngle(currentTopFacePosition, targetFacePosition, rotationAxis);

        if (rotationAngle == 180)
        {
            if (currentTopFacePosition.x != 0)
                rotationAxis.y = 1;
            else if (currentTopFacePosition.y != 0)
                rotationAxis.z = 1;
            else if (currentTopFacePosition.z != 0)
                rotationAxis.x = 1;
        }

        rotationAxis = -rotationAxis.normalized * rotationAngle;

        _view.rotation *= Quaternion.Euler(rotationAxis);
    }
}
using UnityEngine;

[CreateAssetMenu( fileName = "RecorderSettings", menuName = "Add/RecorderSettings", order = 1)]
public class RecorderSettings : ScriptableObject
{
    [SerializeField] private float _animationSpeed = 1f;

    public float AnimationSpeed => _animationSpeed;
}
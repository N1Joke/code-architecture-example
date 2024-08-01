using UnityEngine;

[CreateAssetMenu(fileName = "CubePreset", menuName = "ScriptableObjects/CubePreset", order = 1)]
public class CubePreset : ScriptableObject
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _distance = 2;
    [SerializeField] private CubeView _cubePrefab;

    public float Speed => _speed;
    public float Distance => _distance;
    public CubeView CubePrefab => _cubePrefab;
}

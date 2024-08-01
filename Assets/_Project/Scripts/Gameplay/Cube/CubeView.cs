using UnityEngine;

public class CubeView : MonoBehaviour
{
    [SerializeField] private GameObject _model;

    public GameObject Model => _model;
}

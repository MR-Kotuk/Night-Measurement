using Unity.AI.Navigation;
using UnityEngine;

public class Measurements : MonoBehaviour
{
    [Header("Measurements")]
    [SerializeField] private GameObject _measurement;
    [SerializeField] private GameObject _anotherMeasurement;
    [Space]

    [Header("Scripts")]
    [SerializeField] private NavMeshSurface _surfcase;

    private void Start()
    {
        _measurement.SetActive(true);
        _anotherMeasurement.SetActive(false);
    }

    public void SwitchMeasurement()
    {
        Handheld.Vibrate();
        
        _measurement.SetActive(!_measurement.activeSelf);
        _anotherMeasurement.SetActive(!_anotherMeasurement.activeSelf);

        _surfcase.BuildNavMesh();
    }
}

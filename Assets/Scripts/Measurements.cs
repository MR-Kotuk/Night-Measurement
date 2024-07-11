using Unity.AI.Navigation;
using UnityEngine;
using System.Collections;
using CandyCoded.HapticFeedback;

public class Measurements : MonoBehaviour
{
    [Header("Measurements")]
    [SerializeField] private GameObject _measurement;
    [SerializeField] private GameObject _anotherMeasurement;
    [Space]

    [Header("Shader Activate/Disactivate")]
    [SerializeField] private Material _mesurementMaterial;
    [SerializeField] private Material _anotherMesurementMaterial;

    [SerializeField] private float _dissolveSpeed;
    [SerializeField] private float _dissolveDelay;

    [SerializeField] private float _maxAmount;
    [SerializeField] private float _minAmount;
    [Space]

    [Header("Scripts")]
    [SerializeField] private NavMeshSurface _surfcase;

    private bool isMeasurement = true;
    private bool isCanSwitch;

    public void MeasurementStartActive()
    {
        isCanSwitch = true;

        _measurement.SetActive(true);
        _anotherMeasurement.SetActive(true);

        MeasurementSwitchDissolve(true);
    }

    public void SwitchMeasurement()
    {
        if (isCanSwitch)
        {
            HapticFeedback.LightFeedback();

            isMeasurement = !isMeasurement;

            MeasurementSwitchDissolve(isMeasurement);
        }
    }

    private void MeasurementSwitchDissolve(bool isActive)
    {
        SwitchActive(isMeasurement, true);

        StartCoroutine(Dissolve(isActive ? _anotherMesurementMaterial : _mesurementMaterial, -_dissolveSpeed, _minAmount));
        StartCoroutine(Dissolve(isActive ? _mesurementMaterial : _anotherMesurementMaterial, _dissolveSpeed, _maxAmount));

        SwitchActive(!isMeasurement, false);

        _surfcase.BuildNavMesh();
    }

    private void SwitchActive(bool isMeasurement, bool isActive)
    {
        if (isMeasurement)
            _measurement.SetActive(isActive);
        else
            _anotherMeasurement.SetActive(isActive);
    }

    private IEnumerator Dissolve(Material material, float speed, float target)
    {
        float dissolveAmount = material.GetFloat("_Height");

        while ((speed > 0 && dissolveAmount < target) || (speed < 0 && dissolveAmount > target))
        {
            dissolveAmount += speed;
            material.SetFloat("_Height", dissolveAmount);
            yield return new WaitForSeconds(_dissolveDelay);
        }
    }
}

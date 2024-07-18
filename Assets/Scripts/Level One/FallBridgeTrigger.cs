using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class FallBridgeTrigger : MonoBehaviour
{
    [Header("Part of Bridge Fall Settings")]
    [SerializeField] private float _fallDelay;
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _measurementDelay;

    [SerializeField] private List<GameObject> _fallingObjects;

    [Header("Scripts")]
    [SerializeField] private Measurements _measurements;
    [SerializeField] private NavMeshSurface _surfcase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
            Fall(_fallingObjects);
    }

    private void Update()
    {
        _surfcase.BuildNavMesh();
    }

    private void Fall(List<GameObject> objects)
    {
        foreach (GameObject fallPart in objects)
        {
            IFallObject fallObject = fallPart.GetComponent<IFallObject>();

            if (fallObject != null)
            {
                fallObject.FallDelay = _fallDelay;
                fallObject.isFall = true;
            }

            Destroy(fallPart, _destroyTime);
        }

        Invoke("ActiveMeasurements", _measurementDelay);
    }

    private void ActiveMeasurements()
    {
        _measurements.MeasurementStartActive();
        Destroy(gameObject);
    }
}

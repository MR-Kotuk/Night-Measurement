using UnityEngine;

public interface IFallObject
{
    [HideInInspector] public float FallDelay { get; set; }
    [HideInInspector] public bool isFall { get; set; }

    public void Fall();
}

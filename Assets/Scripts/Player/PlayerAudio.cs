using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Steps")]
    [SerializeField] private AudioSource _stepsSource;
    [SerializeField] private AudioClip[] _steps;

    public void Step()
    {
        System.Random random = new System.Random();
        AudioClip step = _steps[random.Next(0, _steps.Length)];

        _stepsSource.clip = step;
        _stepsSource.Play();
    }

    public void Touch()
    {
        _stepsSource.clip = _steps[0];
        _stepsSource.Play();
    }
}
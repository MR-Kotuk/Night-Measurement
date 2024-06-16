using UnityEngine;

public class GameInput : MonoBehaviour
{
    [Header("KeyCode")]
    [SerializeField] private int _move;
    [Space]

    [Header("Delay")]
    [SerializeField] private float _delayOfMeasurement;
    [Space]

    [Header("Scripts")]
    [SerializeField] private PlayerMovement _playerMove;
    [SerializeField] private Measurements _measurements;

    private bool isHolding;
    private float _mouseDownTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_move))
        {
            _mouseDownTime = Time.time;
            isHolding = true;
        }

        if (Input.GetMouseButton(_move) && isHolding)
        {
            float heldTime = Time.time - _mouseDownTime;

            if (heldTime >= _delayOfMeasurement)
            {
                _measurements.SwitchMeasurement();
                isHolding = false;
            }
        }

        if (Input.GetMouseButtonUp(_move))
        {
            if (isHolding)
            {
                _playerMove.Move();
                isHolding = false;
            }
        }
    }
}
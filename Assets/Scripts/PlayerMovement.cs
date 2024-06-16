using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;

    [SerializeField] private float _secondsToRun;

    [SerializeField] private GameObject _pointObject;

    private NavMeshAgent _navAgent;

    private float _frames;
    private bool isMove;
    [Space]

    [Header("Layers")]
    [SerializeField] private LayerMask _canMove;

    private PlayerAnimations _animator;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimations>();
        _navAgent = GetComponent<NavMeshAgent>();

        _navAgent.speed = _speed;
    }

    private void Update()
    {
        RatingFramesOfMoving();
        _animator.SwitchMove(_navAgent.velocity.magnitude != 0, _navAgent.speed);
    }

    public void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit) && IsPosInNavMesh(raycastHit.collider.gameObject))
        {
            _pointObject.transform.position = raycastHit.point;

            _navAgent.SetDestination(raycastHit.point);

            if (isMove)
            {
                _navAgent.speed = _runSpeed;
                _frames = 0;
            }
            else
                _navAgent.speed = _speed;

            isMove = true;
        }
    }

    private bool IsPosInNavMesh(GameObject pos)
    {
        NavMeshHit hit;

        if (NavMesh.SamplePosition(pos.transform.position, out hit, 10f, NavMesh.AllAreas))
            return pos.layer == Mathf.Log(_canMove.value, 2);

        return false;
    }

    private void RatingFramesOfMoving()
    {
        if (_frames + Time.deltaTime >= _secondsToRun || _navAgent.destination == transform.position)
        {
            isMove = false;
            _frames = 0;
        }
        else if (isMove)
            _frames += Time.deltaTime;
    }
}

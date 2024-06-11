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
        Move();
    }

    private void Move()
    {
        RatingFramesOfMoving();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.gameObject.layer == Mathf.Log(_canMove.value, 2) && IsPosInNavMesh(raycastHit.point))
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


        _animator.SwitchWalk(_navAgent.velocity.magnitude != 0);
    }

    private bool IsPosInNavMesh(Vector3 pos)
    {
        NavMeshHit hit;

        if (NavMesh.SamplePosition(pos, out hit, 0.1f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path);

            if (path.status == NavMeshPathStatus.PathComplete)
                return true;
        }

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

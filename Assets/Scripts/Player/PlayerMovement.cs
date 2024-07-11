using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;

    [SerializeField] private float _secondsToRun;
    [Space]

    [Header("Touch Point")]
    [SerializeField] private GameObject _pointObject;
    [SerializeField] private float _distOff;
    [Space]

    [Header("Game")]
    [SerializeField] private float _minPosY;

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

        _pointObject.SetActive(false);
        _navAgent.speed = _speed;
    }

    private void Update()
    {
        RatingFramesOfMoving();
        _animator.SwitchMove(_navAgent.velocity.magnitude != 0, _navAgent.speed);

        if (Vector3.Distance(transform.position, _pointObject.transform.position) <= _distOff)
            _pointObject.SetActive(false);

        if (transform.position.y <= _minPosY)
            SceneManager.LoadScene("LevelOne");
    }

    public void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit) && IsPosInNavMesh(raycastHit.collider.gameObject))
        {
            _pointObject.SetActive(false);
            _pointObject.SetActive(true);

            _pointObject.transform.position = raycastHit.point;
            _pointObject.transform.position = new Vector3(_pointObject.transform.position.x, _pointObject.transform.position.y + 0.1f, _pointObject.transform.position.z);

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
        return pos.layer == Mathf.Log(_canMove.value, 2);
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

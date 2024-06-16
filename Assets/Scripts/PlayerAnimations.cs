using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private const string _move = "isMove";
    private const string _moveSpeed = "MoveSpeed";

    public void SwitchMove(bool isMove, float speed)
    {
        _playerAnimator.SetBool(_move, isMove);
        _playerAnimator.SetFloat(_moveSpeed, speed);
    }
}

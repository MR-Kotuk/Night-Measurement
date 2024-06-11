using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private const string _walk = "isWalk";

    public void SwitchWalk(bool isWalk)
    {
        _playerAnimator.SetBool(_walk, isWalk);
    }
}

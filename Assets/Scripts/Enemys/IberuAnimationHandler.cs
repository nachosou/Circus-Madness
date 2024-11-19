using UnityEngine;

public class IberuAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] private string attackBoolName = "isAttacking";

    public void SetIberuAttackingBoolAnimation(bool isAttacking)
    {
        animator.SetBool(attackBoolName, isAttacking);
    }
}

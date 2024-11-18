using UnityEngine;

public class IberuAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetIberuAttackingBoolAnimation(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }
}

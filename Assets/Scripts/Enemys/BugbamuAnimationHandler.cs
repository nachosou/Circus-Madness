using UnityEngine;

public class BugbamuAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetBugbamuAttackingBoolAnimation(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    public void SetBugbamuExplodingBoolAnimation(bool isExploding)
    {
        animator.SetBool("isExploding", isExploding);
    }
}

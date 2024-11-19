using UnityEngine;

public class BugbamuAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] private string attackBoolName = "isAttacking";
    [SerializeField] private string explodeBoolName = "isExploding";

    public void SetBugbamuAttackingBoolAnimation(bool isAttacking)
    {
        animator.SetBool(attackBoolName, isAttacking);
    }

    public void SetBugbamuExplodingBoolAnimation(bool isExploding)
    {
        animator.SetBool(explodeBoolName, isExploding);
    }
}

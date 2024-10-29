using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void SetGrapplingBoolAnimation(bool isGrappling)
    {
        animator.SetBool("isGrappling", isGrappling);
    }
}

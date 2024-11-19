using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] private string grapplingBoolName = "isGrappling";

    public void SetGrapplingBoolAnimation(bool isGrappling)
    {
        animator.SetBool(grapplingBoolName, isGrappling);
    }
}

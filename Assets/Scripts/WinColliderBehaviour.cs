using UnityEngine;

public class WinColliderBehaviour : MonoBehaviour
{
    [SerializeField] private WinHandler winHandler;
    [SerializeField] private string playerTagName = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag(playerTagName))
        {
            winHandler.hasPlayerWon = true;
        }
    }
}

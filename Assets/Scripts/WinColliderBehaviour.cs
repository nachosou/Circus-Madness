using UnityEngine;

public class WinColliderBehaviour : MonoBehaviour
{
    [SerializeField] private WinHandler winHandler;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            winHandler.hasPlayerWon = true;
        }
    }
}

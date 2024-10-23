using UnityEngine;

public class WinColliderBehaviour : MonoBehaviour
{
    [SerializeField] private LevelController levelController;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            levelController.AdvanceToNextLevel();
        }
    }
}

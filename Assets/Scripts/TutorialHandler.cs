using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    private float tutorialCoolDown = 0.1f;

    private void Start()
    {
        tutorialCanvas.SetActive(false);   
    }

    private void Update()
    {
        tutorialCoolDown -= Time.deltaTime;

        if (tutorialCoolDown < 0)
        {
            ActivateTutorialCanvas();
            Destroy(gameObject);
        }
    }

    private void ActivateTutorialCanvas()
    {
        tutorialCanvas.SetActive(true);
    }
}

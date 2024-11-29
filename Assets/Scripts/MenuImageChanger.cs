using UnityEngine;
using UnityEngine.UI;

public class MenuImageChanger : MonoBehaviour
{
    [SerializeField] private const int imagesCuantity = 2;

    [SerializeField] private Image[] images = new Image[imagesCuantity];
    [SerializeField] private Image displayImage;

    [SerializeField] private float timeBetweenImages = 1f;
    private float timer;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateDisplayedImage();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        IterateBetweenImages();
    }

    private void IterateBetweenImages()
    {
        if (timer > timeBetweenImages) 
        {             
            if (currentIndex == 0)
            {
                currentIndex = 1;
            }
            else if (currentIndex == 1) 
            {
                currentIndex = 0;
            }

            UpdateDisplayedImage();
            timer = 0f;
        }
    }    

    private void UpdateDisplayedImage()
    {
        displayImage.sprite = images[currentIndex].sprite;
    }
}

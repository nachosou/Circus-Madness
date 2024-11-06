using UnityEngine;

public class SpiralRotation : MonoBehaviour
{
    public float rotationSpeed; 
    private float currentRotation = 0f;

    private void Update()
    {
        currentRotation += rotationSpeed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
    }
}


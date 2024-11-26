using UnityEngine;

public class MainMenuClock : MonoBehaviour
{
    public float rotationSpeed; 
    public float minAngle;     
    public float maxAngle;
    private float pingPongLength = 1;

    private void Update()
    {
        RotateBetweenLimits();
    }

    private void RotateBetweenLimits()
    {
        float angle = Mathf.Lerp(minAngle, maxAngle, Mathf.PingPong(Time.time * rotationSpeed, pingPongLength));
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}

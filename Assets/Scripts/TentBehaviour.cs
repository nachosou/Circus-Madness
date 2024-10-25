using UnityEngine;

public class TentBehaviour : MonoBehaviour
{
    private Vector3 rotationAxis = new Vector3(0, 1, 0);
    public float rotateSpeed;

    private void Update()
    {
        RotateTent();
    }

    private void RotateTent()
    {
        transform.Rotate(rotationAxis * rotateSpeed * Time.deltaTime);
    }
}

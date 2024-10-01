using UnityEngine;

public class RisingFloor : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        Elevate();
    }

    void Elevate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

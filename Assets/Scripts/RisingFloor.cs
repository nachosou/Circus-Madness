using UnityEngine;

public class RisingFloor : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Elevate();
    }

    public void Elevate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(200);
            }
        }
    }
}

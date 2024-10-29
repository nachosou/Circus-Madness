using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    public float force;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {           
            HealthSystem playerHealth = collision.transform.GetComponent<HealthSystem>();

            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            if (rb != null)
            {
                Vector3 impulseDirection = (collision.transform.position - transform.position).normalized;

                rb.AddForce(impulseDirection * force, ForceMode.Impulse);
            }

            Destroy(gameObject);  
        }
        else
        {
            Destroy(gameObject, 1f); 
        }
    }
}

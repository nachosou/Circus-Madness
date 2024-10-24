using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    public Vector3 force;

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

            rb.AddForce(transform.TransformDirection(-force), ForceMode.Impulse);

            Destroy(gameObject);  
        }
        else
        {
            Destroy(gameObject, 1f); 
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage; 

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.transform.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);  
        }
        else
        {
            Destroy(gameObject, 2f); 
        }
    }
}

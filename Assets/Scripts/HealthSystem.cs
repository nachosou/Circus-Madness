using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject, 0.1f);
    }
}


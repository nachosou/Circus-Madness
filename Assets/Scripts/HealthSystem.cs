using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        gameObject.SetActive(false);
    }
}


using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    private float maxHealth; 

    private void Start()
    {
        maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    //public void ResetHealth()
    //{
    //    health = maxHealth;
    //    // Asegúrate de que el enemigo esté activo antes de restablecer la salud
    //    if (!gameObject.activeSelf)
    //    {
    //        gameObject.SetActive(true);
    //    }
    //}

    protected void Die()
    {
        gameObject.SetActive(false);
    }
}


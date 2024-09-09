using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}


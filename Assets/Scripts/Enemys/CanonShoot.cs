using UnityEngine;

public class CanonShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform target;
    [SerializeField] GameObject projectilePrefab;

    public float shootCoolDown;
    public float shootDistance;
    public float damage;

    private float shootTimer;
    private bool isAttacking;

    private void Start()
    {
        isAttacking = false;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (IsPlayerInRange())
        {
            Shoot();
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, target.position) <= shootDistance;
    }

    private void Shoot()
    {
        if (shootTimer <= 0f)
        {
            ShootAction();
            shootTimer = shootCoolDown;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void ShootAction()
    {
        isAttacking = true;

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = (target.position - shootPoint.position).normalized * Time.deltaTime;

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
        }
    }
}

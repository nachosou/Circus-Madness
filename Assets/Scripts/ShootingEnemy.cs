using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform target;     
    [SerializeField] GameObject projectilePrefab;   
    
    private IberuAnimationHandler animationHandler;

    public float shootCoolDown;             
    public float shootSpeed;             
    public float shootDistance;
    public float damage;

    private float shootTimer;
    private bool isAttacking;

    private void Start()
    {
        animationHandler = GetComponent<IberuAnimationHandler>();
        isAttacking = false;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (IsPlayerInRange())
        {
            LookAtPlayer(); 
            Shoot();
            animationHandler.SetIberuAttackingBoolAnimation(isAttacking);
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, target.position) <= shootDistance;
    }

    private void LookAtPlayer()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = target.transform.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }

    private void Shoot()
    {
        if (shootTimer <= 0.0f)
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

        Vector3 direction = (target.position - shootPoint.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * shootSpeed;
        }

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);  
        }
    }
}

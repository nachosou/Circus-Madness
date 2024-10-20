using System.Collections;
using UnityEngine;

public class ExplosiveEnemy : MonoBehaviour
{
    [SerializeField] Transform player;

    public float followDistance;
    public float followSpeed;
    public float startExplosionDistance;

    public float explosionForce;
    public float explosionRadius;
    public float damage;
    public float waitForExplosionTime;

    private bool hasToExplode;
    private bool isExploding;

    private void Start()
    {
        hasToExplode = false;
        isExploding = false;
    }

    private void Update()
    {
        CheckIfPlayerIsOnRange();
    }

    private void CheckIfPlayerIsOnRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        CheckEnemyNearEnoughToExplode(distanceToPlayer);

        if (distanceToPlayer <= followDistance && !hasToExplode && !isExploding)
        {
            SetLookAt();
            FollowPlayer();          
        }
        else if (hasToExplode && !isExploding)
        {
            StartCoroutine(ExplodeAfterDelay());
        }    
    }

    private void FollowPlayer()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.position += moveDirection * followSpeed * Time.deltaTime;
    }

    private void CheckEnemyNearEnoughToExplode(float distanceToPlayer)
    {
        if (distanceToPlayer < startExplosionDistance)
        {
            hasToExplode = true;
        }
    }

    private void SetLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }

    private IEnumerator ExplodeAfterDelay()
    {
        isExploding = true;
        yield return new WaitForSeconds(waitForExplosionTime);
        Explode();
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        Debug.Log($"Explotando, objetos detectados: {hitColliders.Length}");

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log($"Detectado: {hitCollider.name}");

            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Aplicando fuerza de explosión.");
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            HealthSystem playerHealth = hitCollider.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                Debug.Log("Aplicando daño al jugador.");
                playerHealth.TakeDamage((int)damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius); 
    }
}

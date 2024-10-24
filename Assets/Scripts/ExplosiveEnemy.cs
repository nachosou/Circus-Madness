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

        gameObject.GetComponent<Rigidbody>().position = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = hitCollider.GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = 1f;
                rb.drag = 0f;
                rb.angularDrag = 0.05f;

                rb.AddExplosionForce(explosionForce * 10, transform.position, explosionRadius);
            }

            HealthSystem playerHealth = hitCollider.GetComponentInParent<HealthSystem>();
            if (playerHealth != null)
            {
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

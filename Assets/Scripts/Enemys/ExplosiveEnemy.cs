using System.Collections;
using UnityEngine;
using AK.Wwise;

public class ExplosiveEnemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] ParticleSystem confetti;

    private BugbamuAnimationHandler bugbamuAnimationHandler;    

    public float followDistance;
    public float followSpeed;
    public float startExplosionDistance;

    public float explosionForce;
    private float explosionForceMultiplyer = 10;
    public float explosionRadius;
    public float damage;
    public float waitForExplosionTime;

    private bool hasToExplode;
    private bool isExploding;
    private bool isAttacking;

    private float conffetiAngle = -90;

    public AK.Wwise.Event wwiseEvent;

    private void Start()
    {
        hasToExplode = false;
        isExploding = false;
        bugbamuAnimationHandler = GetComponent<BugbamuAnimationHandler>();
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
            isAttacking = true;
            FollowPlayer();  
            bugbamuAnimationHandler.SetBugbamuAttackingBoolAnimation(isAttacking);
        }
        else if (hasToExplode && !isExploding)
        {
            isAttacking = false;
            isExploding = true;
            bugbamuAnimationHandler.SetBugbamuAttackingBoolAnimation(isAttacking);
            bugbamuAnimationHandler.SetBugbamuExplodingBoolAnimation(isExploding);
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
                rb.AddExplosionForce(explosionForce * explosionForceMultiplyer, transform.position, explosionRadius);
            }

            HealthSystem playerHealth = hitCollider.GetComponentInParent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
            }
        }

        Instantiate(confetti, transform.position, Quaternion.Euler(new Vector3(conffetiAngle, 0, 0)));
        wwiseEvent.Post(gameObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance); 
    }
}

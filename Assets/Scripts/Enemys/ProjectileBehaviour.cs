using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    public float force;
    private Vector3 moveDirection;
    private Rigidbody projectileRB;
    public float speed;

    private float restoreDragTime = 0.1f;
    private float destroyTimeDelay = 1.0f;

    [SerializeField] private string playerTagName = "Player";

    private void Start()
    {
        projectileRB = GetComponent<Rigidbody>();
        projectileRB.useGravity = false;
        projectileRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        projectileRB.AddForce(moveDirection * speed, ForceMode.Impulse);
    }

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(playerTagName))
        {
            HealthSystem playerHealth = collision.transform.GetComponent<HealthSystem>();

            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();

            PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            if (rb != null && playerMovement != null)
            {
                float auxDrag = playerMovement.playerData.groundDrag;
                playerMovement.playerData.groundDrag = 0;

                moveDirection.y = 0;

                rb.AddForce(moveDirection * force, ForceMode.Impulse);

                StartCoroutine(RestoreDrag(playerMovement, auxDrag));
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, destroyTimeDelay);
        }        
    }

    private IEnumerator RestoreDrag(PlayerMovement playerMovement, float originalDrag)
    {       
        playerMovement.playerData.groundDrag = originalDrag;
        yield return new WaitForSeconds(restoreDragTime);
    }
}

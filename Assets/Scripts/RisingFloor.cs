using UnityEngine;

public class RisingFloor : MonoBehaviour
{
    public float speed;
    [SerializeField] private string playerTagName = "Player";

    private void Update()
    {
        Elevate();
    }

    public void Elevate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTagName))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(200);
            }
        }
    }
}

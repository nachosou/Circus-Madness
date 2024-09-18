using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;

    public KeyCode reset = KeyCode.R;

    //HealthSystem healthSystem;

    private void Start()
    {
        //healthSystem = gameObject.GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            setLookAt();
        }

        //if (Input.GetKeyDown(reset))
        //{
        //    ResetEnemy();
        //}
    }

    void setLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.transform.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }

    //void ResetEnemy()
    //{
    //    if (!gameObject.activeSelf) 
    //    {
    //        Debug.Log("Resetting enemy");
            
    //        if (healthSystem != null)
    //        {
    //            healthSystem.ResetHealth();
    //        }
    //    }
    //}
}

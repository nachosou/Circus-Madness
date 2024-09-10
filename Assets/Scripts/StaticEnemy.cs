using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        setLookAt();
    }

    void setLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.transform.position;

        Vector3 vecLookAt = vec2 - vec1;
        
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }
}

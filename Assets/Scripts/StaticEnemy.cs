using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;

    public KeyCode reset = KeyCode.R;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            SetLookAt();
        }
    }

    void SetLookAt()
    {
        Vector3 vec1 = transform.position;
        Vector3 vec2 = player.transform.position;

        Vector3 vecLookAt = vec2 - vec1;
        vecLookAt.y = 0f;

        transform.forward = vecLookAt;
    }
}

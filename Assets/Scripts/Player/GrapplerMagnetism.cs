using UnityEngine;

public class GrapplerMagnetism : MonoBehaviour
{
    [SerializeField] BoxCollider magnetismPoint;

    public Vector3 GetMagnetismPoint(Vector3 position)
    {
        return magnetismPoint != null ? magnetismPoint.ClosestPoint(position) : transform.position;
    }    
}


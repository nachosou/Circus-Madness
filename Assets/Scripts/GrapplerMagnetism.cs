using UnityEngine;

public class GrapplerMagnetism : MonoBehaviour
{
    [SerializeField] Transform magnetismPoint;

    public Transform GetMagnetismPoint() => magnetismPoint;
}


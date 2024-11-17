using UnityEngine;
using AK.Wwise;

public class KnivesStepSound : MonoBehaviour
{
    public AK.Wwise.Event wwiseEvent; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wwiseEvent.Post(gameObject);
        }
    }
}


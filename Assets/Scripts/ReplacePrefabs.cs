using UnityEditor;
using UnityEngine;


public class ReplacePrefabs : MonoBehaviour
{
    [MenuItem("MyMenu/Change prefabs")]
    static void CollectGrappleableObjects()
    {    
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Trapeze.prefab");

        GameObject[] grappleableObjects = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        int grappleableLayer = LayerMask.NameToLayer("Grappleable");

        foreach (GameObject obj in grappleableObjects)
        {
            if (obj.layer == grappleableLayer)
            {
                Instantiate(prefab, obj.transform.position, obj.transform.rotation, obj.transform.parent);

                prefab.transform.localScale = obj.transform.localScale;
            }
        }      
    }
}

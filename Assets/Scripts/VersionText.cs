using UnityEngine;
using TMPro;

public class VersionText : MonoBehaviour
{
    public TMP_Text version;
    void Start()
    {
        version.text = Application.version;
    }
}

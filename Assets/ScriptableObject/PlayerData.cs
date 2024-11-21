using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public float groundDrag;
    public float airDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
}

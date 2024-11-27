using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathData", menuName = "ScriptableObjects/DeathData")]
public class DeathEventSO : ScriptableObject
{
    public Action OnPlayerDeath;
}

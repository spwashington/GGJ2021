using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Atribs", menuName = "Scriptables/Player", order = 1)]
public class PlayerAtribsSO : ScriptableObject
{
    [Header("Player Inputs")]
    public KeyCode GetAnDropItemButton;
    public KeyCode DashButton;

    [Header("Player Speed")]
    public float WalkSpeed;

    public float DashSpeed;
    public float DashDuration;
    public float DashCooldown;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Atribs", menuName = "Scriptables/Player", order = 1)]
public class PlayerAtribsSO : ScriptableObject
{
    [Header("Player Inputs")]
    public string GetAnDropItemButton;
    public string DashButton;

    [Header("Player Speed")]
    public float WalkSpeed;

    public float DashSpeed;
    public float DashDuration;
    public float DashCooldown;

    [Header("Player General Info")]
    public string Gender = "Male";

    [Header("Player InGame Info")]
    public Vector2 movement;
    public bool isHoldingItem;
}

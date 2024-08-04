using System.Collections;
using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth;
    public float speed;
    public float acceleration;
    public BasicAttack mainAttack;
    public float bulletVelocity;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public float dashingParticleTime;
}

using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SlashSpawnData", order = 1)]
public class SlashSpawnData : ScriptableObject
{
    public GameObject slashResource;
    public float cooldown;
    public float slashDamage;
    public float slashLifetime;
    public string slashType;
}
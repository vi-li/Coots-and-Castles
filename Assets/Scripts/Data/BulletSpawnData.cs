using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletSpawnData", order = 1)]
public class BulletSpawnData : ScriptableObject
{
    public GameObject bulletResource;

    public int numberOfBullets;
    public float minRotation;
    public float maxRotation;
    public bool isRandom;
    public bool isNotParent = true;
    public float cooldown;

    public Vector2 bulletVelocity;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLifetime;
    public string bulletType;
}
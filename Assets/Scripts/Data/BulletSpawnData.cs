using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletSpawnData", order = 1)]
public class BulletSpawnData : AttackSpawnData
{
    public Vector2 bulletVelocity;
    public float bulletSpeed;
    public int bulletCount;
    public float minRotation;
    public float maxRotation;
    public bool isRandom;
    public bool isNotParent = true;
}
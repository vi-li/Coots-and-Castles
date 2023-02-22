using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public List<BulletSpawnData> spawnDatas;
    public int index = 0;
    public bool isSequenceRandom;
    public bool spawnAutomatically;
    private float cooldown;
    private float[] rotations;

    void Start()
    {
        cooldown = GetSpawnData().cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAutomatically)
        {
            if (cooldown <= 0)
            {
                SpawnBullets();
                cooldown = GetSpawnData().cooldown;
                if (isSequenceRandom)
                {
                    index = Random.Range(0, spawnDatas.Count);
                }
                else
                {
                    index += 1;
                    if (index >= spawnDatas.Count) index = 0;
                }
                rotations = new float[GetSpawnData().bulletCount];
            }

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    public BulletSpawnData GetSpawnData()
    {
        return spawnDatas.ElementAt(index);
    }

    // Select a random rotation from min to max for each bullet
    public float[] RandomRotations()
    {
        for (int i = 0; i < GetSpawnData().bulletCount; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;
    }
    
    // This will set random rotations evenly distributed between the min and max Rotation.
    public float[] DistributedRotations()
    {
        for (int i = 0; i < GetSpawnData().bulletCount; i++)
        {
            var fraction = (float)i / ((float)GetSpawnData().bulletCount - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation; // We add minRotation to undo Difference
        }
        return rotations;
    }

    public void SpawnBullets()
    {
        BulletSpawnData spawnData = GetSpawnData();
        rotations = new float[spawnData.bulletCount];
        if (spawnData.isRandom)
        {
            RandomRotations();
        } else
        {
            DistributedRotations();
        }

        // Spawn Bullets
        GameObject[] spawnedBullets = new GameObject[spawnData.bulletCount];
        for (int i = 0; i < spawnData.bulletCount; i++)
        {
            spawnedBullets[i] = AttackManager.GetAttackFromPoolWithType(spawnData.type);
            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(spawnData.attackResource, transform);
                AttackManager.attacks.Add(spawnedBullets[i]);
            } else
            {
                spawnedBullets[i].transform.SetParent(transform);
            }

            spawnedBullets[i].transform.localPosition = Vector2.zero;

            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.SetRotation(rotations[i]);
            b.SetSpeed(spawnData.bulletSpeed);
            b.SetVelocity(spawnData.bulletVelocity);
            b.SetDamage(spawnData.damage);
            b.SetLifetime(spawnData.lifetime);

            if (spawnData.isNotParent)
            {
                spawnedBullets[i].transform.SetParent(null);
            }
        }
    }
}
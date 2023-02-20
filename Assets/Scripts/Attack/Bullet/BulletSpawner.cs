using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnData[] spawnDatas;
    public int index = 0;
    public bool isSequenceRandom;
    public bool spawnAutomatically;
    private float timer;
    private float[] rotations;

    void Start()
    {
        timer = GetSpawnData().cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAutomatically)
        {
            if (timer <= 0)
            {
                SpawnBullets();
                timer = GetSpawnData().cooldown;
                if (isSequenceRandom)
                {
                    index = Random.Range(0, spawnDatas.Length);
                }
                else
                {
                    index += 1;
                    if (index >= spawnDatas.Length) index = 0;
                }
                rotations = new float[GetSpawnData().numberOfBullets];

            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }

    BulletSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }

    // Select a random rotation from min to max for each bullet
    public float[] RandomRotations()
    {
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;
    }
    
    // This will set random rotations evenly distributed between the min and max Rotation.
    public float[] DistributedRotations()
    {
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            var fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation; // We add minRotation to undo Difference
        }
        return rotations;
    }

    public GameObject[] SpawnBullets()
    {
        BulletSpawnData spawnData = GetSpawnData();
        rotations = new float[spawnData.numberOfBullets];
        if (spawnData.isRandom)
        {
            RandomRotations();
        } else
        {
            DistributedRotations();
        }

        // Spawn Bullets
        GameObject[] spawnedBullets = new GameObject[spawnData.numberOfBullets];
        for (int i = 0; i < spawnData.numberOfBullets; i++)
        {
            spawnedBullets[i] = AttackManager.GetBulletFromPoolWithType(spawnData.bulletType);
            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(spawnData.bulletResource, transform);
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
            b.SetDamage(spawnData.bulletDamage);
            b.SetLifetime(spawnData.bulletLifetime);

            if (spawnData.isNotParent)
            {
                spawnedBullets[i].transform.SetParent(null);
            }
        }

        return spawnedBullets;
    }
}
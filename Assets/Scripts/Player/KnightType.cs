using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KnightType : PieceType
{
    public BulletSpawner bulletSpawner;

    private void Awake()
    {
        abilityTimer = 0;
        player = gameObject.GetComponent<Player>();

        bulletSpawner = gameObject.AddComponent<BulletSpawner>();
        bulletSpawner.spawnDatas = new List<BulletSpawnData>();

        // Use Knight Attack Asset
        string[] assetGuids = AssetDatabase.FindAssets("Knight_Attack_Player");
        string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[0]);
        bulletSpawner.spawnDatas.Add((BulletSpawnData)AssetDatabase.LoadAssetAtPath(assetPath, typeof(BulletSpawnData)));
    }

    public override void Attack()
    {
        if (abilityTimer <= 0)
        {
            bulletSpawner.SpawnBullets();
            abilityTimer = bulletSpawner.GetSpawnData().cooldown;
        }
    }
}

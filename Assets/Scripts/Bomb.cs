using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    
    public BulletSpawner spawner;
    void Start()
    {
        spawner = gameObject.GetComponent<BulletSpawner>();
        spawner.SpawnBullets();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

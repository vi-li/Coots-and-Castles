using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropUpgrades : MonoBehaviour
{
    public Powerup[] upgrade;
    public int size;
    public float dropChance; // Between 0 and 1

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drop()
    {
        int rngDrop = Random.Range(0, size);

        float rngNum = Random.Range(0f, 1f);

        if (rngNum <= dropChance)
        {
            Powerup instantiatedUpgrade = Instantiate(upgrade[rngDrop], transform.position, transform.rotation, null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Attack
{
    public string type;
    public bool oneTimeUse = true;

    void Start()
    {
        timer = lifetime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void Update()
    {
        DeltaMoveWithSpeed();
        TickTimer();
    }

    public void SetBulletType(string type)
    {
        this.type = type;
    }

    public string GetBulletType()
    {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && oneTimeUse)
        {
            gameObject.SetActive(false);
        }
    }
}
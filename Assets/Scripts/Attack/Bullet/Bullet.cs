using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackBase
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag != "PlayerAttack" && collision.tag == "Player" && oneTimeUse)
        {
            gameObject.SetActive(false);
        }
    }
}
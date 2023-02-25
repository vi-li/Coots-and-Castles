using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coots : Enemy
{
     public GameController control;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && invulnerabilityTimer <= 0)
        {
            float damage = collision.gameObject.GetComponent<AttackBase>().GetDamage();
            hp -= damage;
            print("Enemy Health: " + hp);

            if (hp <= 0)
            {
                print("Enemy died");
                Death();
            }

            invulnerabilityTimer = invulnerabilityCooldown;
        }
    }

    private void Death()
    {
        control.Victory();
        gameObject.SetActive(false);
        
    }
}

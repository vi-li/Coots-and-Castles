using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coots : Enemy
{
     public GameController control;
     public Healthbar health;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        health.setMaxHealth(startHp);
        health.setHealth(startHp);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && invulnerabilityTimer <= 0)
        {
            float damage = collision.gameObject.GetComponent<AttackBase>().GetDamage();
            hp -= damage;
            health.setHealth(hp);
            print("Enemy Health: " + hp);

            if (hp <= 0)
            {
                print("Enemy died");
                Death();
            }

            invulnerabilityTimer = invulnerabilityCooldown;
        }
    }

    new private void Death()
    {
        control.Victory();
        gameObject.SetActive(false);
    }
}

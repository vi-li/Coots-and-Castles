using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startHp;
    public float hp;

    public float invulnerabilityCooldown;
    public float invulnerabilityTimer;

    // Start is called before the first frame update
    public void Start()
    {
        hp = startHp;
    }

    public void Update()
    {
        TickTimer();
    }

    private void TickTimer()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
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

    public void Death()
    {
        gameObject.GetComponent<DropUpgrades>().Drop();
        gameObject.SetActive(false);
    }
}

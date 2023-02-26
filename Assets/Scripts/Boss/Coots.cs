using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coots : Enemy
{
     public GameController control;
     public TextMeshProUGUI bossHP;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
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
            bossHP.gameObject.SetActive(true);
            float damage = collision.gameObject.GetComponent<AttackBase>().GetDamage();
            hp -= damage;
            bossHP.GetComponent<TextMeshProUGUI>().text = "Coots HP: " + hp;
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

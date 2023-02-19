using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startHp;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        print("set enemy hp");
        hp = startHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            float damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
            hp -= damage;
            print("Enemy Health: " + hp);

            if (hp <= 0)
            {
                print("Enemy died");
            }
        }
    }
}

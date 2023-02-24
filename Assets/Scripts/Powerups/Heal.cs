using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Heal")]

public class Heal : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        float HP = player.hp;
        float startHP = player.startHp;
        HP += amount;
        if(HP > startHP){
            HP = startHP;
        }
        player.hp = HP;
        player.setHealthBar(HP);
        Debug.Log(target.GetComponent<Player>().hp);
    }
}

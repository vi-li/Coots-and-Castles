using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Heal")]

public class Heal : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target){
        float HP = target.GetComponent<Player>().hp;
        float startHP = target.GetComponent<Player>().startHp;
        HP += amount;
        if(HP > startHP){
            HP = startHP;
        }
        target.GetComponent<Player>().hp = HP;
        Debug.Log(target.GetComponent<Player>().hp);
    }
    
}

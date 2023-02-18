using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Heal")]

public class Heal : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target){
        target.GetComponent<Player>().hp.value += amount;

    }
    
}

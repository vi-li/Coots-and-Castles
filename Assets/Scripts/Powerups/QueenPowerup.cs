using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/QueenPowerup")]
public class QueenPowerup : PowerupEffect
{
   public float amount;
   public Player player;
   public override void Apply(GameObject target){
      player = target.GetComponent<Player>();
      player.piece = Player.PlayerType.QUEEN;
      player.transformTimer = amount;
   }
}

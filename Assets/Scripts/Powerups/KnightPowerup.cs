using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/QueenPowerup")]
public class KnightPowerup : PowerupEffect
{
   public float amount;
   public Player player;
   public override void Apply(GameObject target){
      player = target.GetComponent<Player>();
      player.piece = Player.PlayerType.KNIGHT;
      player.transformTimer = amount;
   }
}

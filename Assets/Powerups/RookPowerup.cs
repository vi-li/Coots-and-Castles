using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/RookPowerup")]
public class RookPowerup : PowerupEffect
{
   public float amount;
   public Player player;
   public override void Apply(GameObject target){
      player = target.GetComponent<Player>();
      player.piece = Player.PlayerType.ROOK;
      player.transformTimer = amount;
   }
}

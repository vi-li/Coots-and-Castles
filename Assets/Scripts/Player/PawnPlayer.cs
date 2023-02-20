using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPlayer : Player
{
    public Slash slash;

    protected override void OnFire()
    {
        Attack();
    }

    private void Attack()
    {
        print("PawnPlayer attacked");
    }
}

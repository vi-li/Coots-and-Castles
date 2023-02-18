using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPlayer : Player
{
    public Slash slash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnFire()
    {
        Attack();
    }

    private void Attack()
    {
        print("pawnplayer slashed");
        slash.SlashAttack();
    }
}

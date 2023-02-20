using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : AttackBase
{
    //public AttackDirection attackDirection;
    //private Vector2 rightAttackOffset;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0.5f;
        timer = lifetime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void Update()
    {
        TickTimer();
    }

    // public void SlashAttack()
    // {
    //     // To be used in the case of attacking different sides
    //     switch(attackDirection)
    //     {
    //         case AttackDirection.left:
    //             AttackLeft();
    //             break;
    //         case AttackDirection.right:
    //             AttackRight();
    //             break;
    //     }
    //     print("slash started");
    //     slashCollider.enabled = true;
    // }

    // To be used in the case of attacking different sides
    // private void AttackRight()
    // {
    //     slashCollider.enabled = true;
    //     transform.position = rightAttackOffset;
    // }

    // private void AttackLeft()
    // {
    //     slashCollider.enabled = true;
    //     transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    // }

    // public void StopAttack()
    // {
    //     print("slash stopped");
    //     slashCollider.enabled = false;
    // }
}

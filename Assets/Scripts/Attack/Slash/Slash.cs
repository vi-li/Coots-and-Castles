using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Attack
{
    public AttackDirection attackDirection;
    private Vector2 rightAttackOffset;
    private Collider2D slashCollider;

    // Start is called before the first frame update
    void Start()
    {
        slashCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;        
    }

    public void SlashAttack()
    {
        switch(attackDirection)
        {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
        }
    }

    private void AttackRight()
    {
        slashCollider.enabled = true;
        transform.position = rightAttackOffset;
    }

    private void AttackLeft()
    {
        slashCollider.enabled = true;
        transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        slashCollider.enabled = false;
    }
}

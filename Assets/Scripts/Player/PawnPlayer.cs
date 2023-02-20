using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPlayer : Player
{
    public SlashSpawnData spawnData;
    public float cooldown = 0.5f;

    protected override void OnFire()
    {
        Attack();
    }

    private void Attack()
    {
        if (abilityTimer <= 0)
        {
            GameObject spawnedSlash = AttackManager.GetAttackFromPoolWithType(spawnData.slashType);

            if (spawnedSlash == null)
            {
                spawnedSlash = Instantiate(spawnData.slashResource);
                AttackManager.attacks.Add(spawnedSlash);
            }

            spawnedSlash.transform.SetParent(transform);
            spawnedSlash.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            spawnedSlash.transform.localPosition = new Vector2(0, 1 / transform.localScale.y);
            spawnedSlash.transform.rotation = transform.rotation;
            spawnedSlash.transform.SetParent(null);

            var slash = spawnedSlash.GetComponent<Slash>();
            slash.SetLifetime(spawnData.slashLifetime);
            slash.SetDamage(spawnData.slashDamage);

            abilityTimer = abilityCooldown;
        }   
    }
}

using UnityEngine;
using System.Collections.Generic;
public class AttackManager : MonoBehaviour
{
    public static List<GameObject> attacks;

    private void Start()
    {
        attacks = new List<GameObject>();
    }

    public static GameObject GetBulletFromPool()
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            if (!attacks[i].activeSelf)
            {
                GameObject availableBullet = attacks[i];
                var b = availableBullet.GetComponent<AttackBase>();
                b.ResetTimer();
                availableBullet.SetActive(true);
                return availableBullet;
            }
        }

        return null;
    }

    public static GameObject GetBulletFromPoolWithType(string type)
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            if (!attacks[i].activeSelf && attacks[i].GetComponent<Bullet>().GetAttackType() == type)
            {
                GameObject availableBullet = attacks[i];
                var b = availableBullet.GetComponent<Bullet>();
                b.ResetTimer();
                availableBullet.SetActive(true);
                return availableBullet;
            }
        }

        return null;
    }
}
using UnityEngine;
using System.Collections.Generic;
public class BulletManager : MonoBehaviour
{
    public static List<GameObject> bullets;

    private void Start()
    {
        bullets = new List<GameObject>();
    }

    public static GameObject GetBulletFromPool()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeSelf)
            {
                GameObject availableBullet = bullets[i];
                var b = availableBullet.GetComponent<Bullet>();
                b.ResetTimer();
                availableBullet.SetActive(true);
                return availableBullet;
            }
        }

        return null;
    }

    public static GameObject GetBulletFromPoolWithType(string type)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeSelf && bullets[i].GetComponent<Bullet>().GetBulletType() == type)
            {
                GameObject availableBullet = bullets[i];
                var b = availableBullet.GetComponent<Bullet>();
                b.ResetTimer();
                availableBullet.SetActive(true);
                return availableBullet;
            }
        }

        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSequence : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float smoothSpeed = 0.125f;

    public GameObject head;
    public GameObject pawL;
    public GameObject pawR;

    private BulletSpawner headBulletSpawner;
    private BulletSpawner pawLBulletSpawner;
    private BulletSpawner pawRBulletSpawner;
    // Start is called before the first frame update
    void Start()
    {
        head = gameObject.transform.Find("Head").gameObject;
        pawL = gameObject.transform.Find("PawL").gameObject;
        pawR = gameObject.transform.Find("PawR").gameObject;

        headBulletSpawner = head.GetComponent<BulletSpawner>();
        pawLBulletSpawner = pawL.GetComponent<BulletSpawner>();
        pawRBulletSpawner = pawR.GetComponent<BulletSpawner>();

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return StartCoroutine(RotateToAngle(head, 0.5f, 20));
        yield return StartCoroutine(RotateSpawnBullets(head, 2.0f, -45.0f, 5));
    }

    // IEnumerator ScreamAttack()
    // {
    //     headBulletSpawner.SpawnBullets();
    // }

    IEnumerator RotateToAngle(GameObject obj, float duration, float angleToRotateTo)
    {
        Transform objTranform = obj.transform;

        Quaternion startRotation = objTranform.rotation;
        Quaternion endRotation = Quaternion.Euler(objTranform.eulerAngles.x, objTranform.eulerAngles.y, angleToRotateTo);
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            objTranform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);

            yield return null;
        }
    }

    IEnumerator RotateSpawnBullets(GameObject obj, float duration, float angleToRotate, int numWavesBullets)
    {
        Transform objTranform = obj.transform;

        Quaternion startRotation = objTranform.rotation;
        Quaternion endRotation = Quaternion.Euler(objTranform.eulerAngles.x, objTranform.eulerAngles.y, objTranform.eulerAngles.z + angleToRotate);
        float timer = 0.0f;
        float bulletInterval = duration / (float)numWavesBullets;
        float timeNextWave = timer + bulletInterval;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            objTranform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);

            if (timer >= timeNextWave)
            {
                headBulletSpawner.SpawnBullets();
                timeNextWave += bulletInterval;
            }

            yield return null;
        }
    }

    private float SmoothProgress(float progress)
    {
        //maps the progress between -π/2 to π/2
        progress = Mathf.Lerp(-Mathf.PI/2, Mathf.PI/2, progress);
        //returns a value between -1 and 1
        progress = Mathf.Sin(progress);
        //scale the sine value between 0 and 1.
        progress = (progress/2f) + .5f;
        return progress;
    }
}

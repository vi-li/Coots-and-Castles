using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSequence : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float smoothSpeed = 0.125f;

    public GameObject head;
    public GameObject facialExpression;
    public GameObject blinkExpression;
    public GameObject pawL;
    public GameObject pawR;

    public GameObject pawAnchorL;
    public GameObject pawAnchorR;

    private BulletSpawner headBS;
    private BulletSpawner pawLBS;
    private BulletSpawner pawRBS;

    private SpriteRenderer expressionSpriteRenderer;

    public Sprite faceScream;
    public Sprite faceNeutralOpenEyes;
    public Sprite faceNeutralClosedEyes;

    public GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        head = gameObject.transform.Find("Head").gameObject;
        pawL = gameObject.transform.Find("PawL").gameObject;
        pawR = gameObject.transform.Find("PawR").gameObject;

        facialExpression = gameObject.transform.Find("Head").gameObject.transform.Find("FacialExpression").gameObject;
        blinkExpression = gameObject.transform.Find("Head").gameObject.transform.Find("BlinkExpression").gameObject;

        pawAnchorL = gameObject.transform.Find("PawAnchorL").gameObject;
        pawAnchorR = gameObject.transform.Find("PawAnchorR").gameObject;

        expressionSpriteRenderer = facialExpression.GetComponent<SpriteRenderer>();

        headBS = head.transform.Find("BulletSpawnerObj").gameObject.GetComponent<BulletSpawner>();
        pawLBS = pawL.transform.Find("BulletSpawnerObj").gameObject.GetComponent<BulletSpawner>();
        pawRBS = pawR.transform.Find("BulletSpawnerObj").gameObject.GetComponent<BulletSpawner>();

        facialExpression.SetActive(false);
        blinkExpression.SetActive(true);
        
        StartCoroutine(Loop());
        
    }
    IEnumerator Loop(){
        while(true){
            print("about to attack");
            yield return new WaitForSeconds(3);
            yield return StartCoroutine(AttackRoutine());
        }
    }
    IEnumerator AttackRoutine()
    {
        float num = Random.Range(0f, 3f);
        if(num < 1){
            print("swipe");
            yield return StartCoroutine(SwipeAttack(pawL, pawLBS, 0.1f));
        }
        else if(num < 2){
            print("scream");
            yield return StartCoroutine(ScreamAttack(2.0f, -45.0f, 5));
        }
        else{
            print("bomb");
            yield return StartCoroutine(BombAttack());
        }
        // RotateAround(GameObject obj, float inTimeSecs, float angle, GameObject anchorObj)
        ///yield return StartCoroutine(RotateToAngle(head, 0.5f, 20));
        // yield return StartCoroutine(ScreamAttack(2.0f, -45.0f, 5));
        // yield return new WaitForSeconds(2);
        // yield return StartCoroutine(ScreamAttack(2.0f, 45.0f, 5));
        // yield return new WaitForSeconds(2);
        //yield return StartCoroutine(RotateAround(pawL, 1.0f, -15.0f, pawAnchorL));
        //yield return StartCoroutine(RotateAround(pawL, 0.2f, 15.0f, pawAnchorL));
        //yield return StartCoroutine(SwipeAttack(pawL, pawLBS, 0.1f));
        //yield return StartCoroutine(BombAttack());
    }

    IEnumerator ScreamAttack(float duration, float angleToRotate, int numWavesBullets)
    {
        SwapExpression();
        expressionSpriteRenderer.sprite = faceScream;
        yield return StartCoroutine(RotateSpawnBullets(head, headBS, duration, angleToRotate, numWavesBullets));
        SwapExpression();
    }

    IEnumerator SwipeAttack(GameObject paw, BulletSpawner pawBS, float duration)
    {
        SwapExpression();
        expressionSpriteRenderer.sprite = faceNeutralClosedEyes;
        yield return StartCoroutine(RotateSpawnBullets(paw, pawBS, duration, 0.0f, 1));
        SwapExpression();
    }

    IEnumerator BombAttack(){
        Vector3 bomblocation = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, -6f));
        Instantiate(objectToSpawn, bomblocation, transform.rotation);
        yield return 1;
    }
    void SwapExpression()
    {
        blinkExpression.SetActive(!blinkExpression.activeSelf);
        facialExpression.SetActive(!facialExpression.activeSelf);
    }

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

    IEnumerator RotateSpawnBullets(GameObject obj, BulletSpawner bs, float duration, float angleToRotate, int numWavesBullets)
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
                print(bs.transform.eulerAngles);
                bs.SpawnBullets();
                timeNextWave += bulletInterval;
            }

            yield return null;
        }
    }

    IEnumerator RotateAround(GameObject obj, float inTimeSecs, float angle, GameObject anchorObj)
    {
        Vector3 anchorPosition = anchorObj.transform.position;
        float timer = 0.0f;
        float angleDelta = angle / inTimeSecs; // How many degress to rotate in one second
        float ourTimeDelta = 0;

        while (timer < inTimeSecs)
        {
            timer += Time.deltaTime;
            ourTimeDelta = Time.deltaTime;

            // Make sure we dont spin past the angle we want.
            if (timer > inTimeSecs)
            {
                ourTimeDelta -= (timer - inTimeSecs);
            }

            obj.transform.RotateAround(anchorPosition, Vector3.forward, angleDelta * ourTimeDelta);

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

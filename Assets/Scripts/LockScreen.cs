using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockScreen : MonoBehaviour
{
    public CameraFollow cameraF;
    public Camera cam;
    public Tilemap collisionTilemap;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemyPieces = GameObject.Find("Enemy Pieces");
        enemyPieces.SetActive(false);

        collisionTilemap.gameObject.SetActive(true);
        if (collision.tag == "Player")
        {
            cam.orthographicSize += 1;
            cam.transform.position = cam.transform.position + new Vector3(0f, 5f);
            cameraF.isYLocked = true;
            gameObject.SetActive(false);
        }
       
    }
}

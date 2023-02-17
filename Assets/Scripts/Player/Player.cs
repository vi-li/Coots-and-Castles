using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;
    private PlayerMovement controls;

    [SerializeField]
    private float smoothSpeed = 10.0f;
    private Vector3 moveToPosition;
    private Vector3 velocity = Vector3.zero;

    public float startHp;
    float hp;
    public float bulletCooldown;
    float bulletTimer;
    
    private void Awake() {
        controls = new PlayerMovement();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        moveToPosition = transform.position;
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        hp = startHp;
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            moveToPosition = moveToPosition + (Vector3)direction;
        }

    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(moveToPosition + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
        {
            return false;
        }

        return true;
    }

    void Update()
    {
        if (bulletTimer > 0)
        {
            bulletTimer -= Time.deltaTime;
        }

        transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocity, smoothSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && bulletTimer <= 0)
        {
            hp -= collision.gameObject.GetComponent<Bullet>().damage;
            print(hp);
            bulletTimer = bulletCooldown;
        }
    }
}
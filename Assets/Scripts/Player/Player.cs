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
    private SpriteRenderer spriteRenderer;

    public float startHp;
    public float hp;
    public float invulnerabilityCooldown;
    public float invulnerabilityTimer;
    public float transformTimer;
    public PlayerType piece;
    public enum PlayerType{
        ROOK,
        BISHOP,
        QUEEN,
        KNIGHT
    }
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
        invulnerabilityTimer = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            moveToPosition = moveToPosition + (Vector3)direction;
        }
        
        RotateSprite(direction);
    }

    private void RotateSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        } else if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void SmoothMove()
    {
        transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocity, smoothSpeed);
    }

    void Update()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }

        SmoothMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            if (invulnerabilityTimer <= 0){
                float damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
                hp -= damage;
                print("Health: " + hp);

                if (hp <= 0)
                {
                    print("you died");
                }

                invulnerabilityTimer = invulnerabilityCooldown;
            }
        }
    }
}
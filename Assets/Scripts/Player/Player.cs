using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected Tilemap groundTilemap;
    [SerializeField]
    protected Tilemap collisionTilemap;
    protected PlayerMovement controls;

    [SerializeField]
    protected float smoothSpeed = 10.0f;
    protected Vector3 moveToPosition;
    protected Vector3 velocity = Vector3.zero;
    protected SpriteRenderer spriteRenderer;

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

    protected void OnEnable() {
        controls.Enable();
    }

    protected void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Main.Fire.performed += ctx => OnFire();
        moveToPosition = transform.position;
        hp = startHp;
        invulnerabilityTimer = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(moveToPosition + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
        {
            return false;
        }

        return true;
    }

    protected void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            moveToPosition = moveToPosition + (Vector3)direction;
        }
        
        RotateSprite(direction);
    }

    protected void RotateSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        } else if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    protected void SmoothMove()
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

    protected void OnTriggerEnter2D(Collider2D collision)
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

    protected virtual void OnFire()
    {
        print("you attacked");
    }
}
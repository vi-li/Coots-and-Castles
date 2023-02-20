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
    protected Direction facingDirection;

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
    public PlayerType playerType;

    protected enum Direction {
        left, right, up, down
    }
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
        print("set player hp " + hp + " " + startHp);
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

        Rotate(direction);
    }

    protected void Rotate(Vector2 direction)
    {
        if (direction.x > 0)
        {
            facingDirection = Direction.right;
        } else if (direction.x < 0)
        {
            facingDirection = Direction.left;
        } else if (direction.y > 0)
        {
            facingDirection = Direction.up;
        } else
        {
            facingDirection = Direction.down;
        }

        UpdatePlayerRotation();
        //UpdateSpriteRenderer();
    }

    // To be used in the case of "front-facing" sprites
    protected void UpdateSpriteRenderer()
    {
        switch (facingDirection)
        {
            case(Direction.right):
                spriteRenderer.flipX = true;
                break;
            case(Direction.left):
                spriteRenderer.flipX = false;
                break;
        }
    }

    // To be used in the case of "top-down" sprites
    protected void UpdatePlayerRotation()
    {
        switch (facingDirection)
        {
            case(Direction.right):
                transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case(Direction.up):
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case(Direction.left):
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case(Direction.down):
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;
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
            print(collision);
            float damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
            print(damage);
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
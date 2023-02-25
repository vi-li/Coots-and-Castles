using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    
    [SerializeField] Healthbar healthbar;
    [SerializeField]
    protected Tilemap groundTilemap;
    [SerializeField]
    protected Tilemap collisionTilemap;
    protected PlayerMovement controls;

    [SerializeField]
    protected float smoothSpeed = 10.0f;
    protected Vector3 moveToPosition;
    protected Vector3 velocity = Vector3.zero;
    protected Direction facingDirection;

    public float startHp;
    public float hp;

    public float invulnerabilityCooldown;
    public float invulnerabilityTimer;
    public float transformTimer;

    public PlayerType defaultPiece;
    public PlayerType piece;
    public GameController control;

    protected SpriteRenderer spriteRenderer;
    protected Dictionary<string, Sprite> spriteDict;
    public AssetReferenceSprite pawnSprite;
    public AssetReferenceSprite knightSprite;
    public AssetReferenceSprite rookSprite;
    public AssetReferenceSprite bishopSprite;
    public AssetReferenceSprite queenSprite;

    public enum PlayerType
    {
        PAWN,
        KNIGHT,
        ROOK,
        BISHOP,
        QUEEN,
    }

    protected enum Direction {
        left, right, up, down
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
        healthbar.setMaxHealth(startHp);
        print("set player hp " + hp);
        UpdateHealth();

        invulnerabilityTimer = 0;

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteDict = new Dictionary<string, Sprite>();

        LoadAddressableSprites();
    }
    
    void Update()
    {
        TickTimer();
        SmoothMove();
    }

    private void TickTimer()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }

        if (transformTimer > 0)
        {
            transformTimer -= Time.deltaTime;
            if (transformTimer <= 0)
            {
                piece = defaultPiece;
                UpdatePieceTypeScript();
            }
        }
    }

    public void UpdatePieceTypeScript()
    {
        Destroy(gameObject.GetComponent<PieceType>());

        var bulletSpawner = gameObject.GetComponent<BulletSpawner>();
        if (bulletSpawner != null)
        {
            Destroy(bulletSpawner);
        }

        switch (piece)
        {
            case PlayerType.PAWN:
                gameObject.AddComponent<PawnType>();
                spriteRenderer.sprite = spriteDict["Pawn_Light"];
                break;
            case PlayerType.KNIGHT:
                gameObject.AddComponent<KnightType>();
                spriteRenderer.sprite = spriteDict["Knight_Light"];
                break;
            case PlayerType.ROOK:
                gameObject.AddComponent<RookType>();
                spriteRenderer.sprite = spriteDict["Rook_Light"];
                break;
            case PlayerType.BISHOP:
                gameObject.AddComponent<BishopType>();
                spriteRenderer.sprite = spriteDict["Bishop_Light"];
                break;
            case PlayerType.QUEEN:
                gameObject.AddComponent<QueenType>();
                spriteRenderer.sprite = spriteDict["Queen_Light"];
                break;
        }
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

        //UpdatePlayerRotation();
        UpdateSpriteRenderer();
    }

    // To be used in the case of "front-facing" sprites
    protected void UpdateSpriteRenderer()
    {
        switch (facingDirection)
        {
            case(Direction.right):
                transform.eulerAngles = new Vector3(0, 0, 270);
                spriteRenderer.flipX = true;
                break;
            case(Direction.up):
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case(Direction.left):
                transform.eulerAngles = new Vector3(0, 0, 90);
                spriteRenderer.flipX = false;
                break;
            case(Direction.down):
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;
        }
        spriteRenderer.transform.rotation = Quaternion.identity;
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

    public void UpdateHealth()
    {
        SetHealthBar();

        if (hp <= 0)
        {
            print("you died");
            control.GameOver();
        }
    }

    public void SetHealthBar()
    {
        healthbar.setHealth(hp);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack" && invulnerabilityTimer <= 0)
        {
            float damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
            hp -= damage;
            print("Player Health: " + hp);
            UpdateHealth();
            invulnerabilityTimer = invulnerabilityCooldown;
        }
    }

    protected virtual void OnFire()
    {
        gameObject.GetComponent<PieceType>().Attack();
    }

    private void LoadAddressableSprites()
    {
        AsyncOperationHandle<Sprite> pawnSpriteHandle = pawnSprite.LoadAssetAsync();
        AsyncOperationHandle<Sprite> knightSpriteHandle = knightSprite.LoadAssetAsync();
        AsyncOperationHandle<Sprite> rookSpriteHandle = rookSprite.LoadAssetAsync();
        AsyncOperationHandle<Sprite> bishopSpriteHandle = bishopSprite.LoadAssetAsync();
        AsyncOperationHandle<Sprite> queenSpriteHandle = queenSprite.LoadAssetAsync();
        
        pawnSpriteHandle.Completed += SpriteLoaded;
        knightSpriteHandle.Completed += SpriteLoaded;
        rookSpriteHandle.Completed += SpriteLoaded;
        bishopSpriteHandle.Completed += SpriteLoaded;
        queenSpriteHandle.Completed += SpriteLoaded;

        Addressables.Release(pawnSpriteHandle);
        Addressables.Release(knightSpriteHandle);
        Addressables.Release(rookSpriteHandle);
        Addressables.Release(bishopSpriteHandle);
        Addressables.Release(queenSpriteHandle);
    }

    private void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                spriteDict.Add(obj.Result.name, obj.Result);
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("Sprite load failed.");
                break;
            default:
                // case AsyncOperationStatus.None:
                break;
        }
    }
}
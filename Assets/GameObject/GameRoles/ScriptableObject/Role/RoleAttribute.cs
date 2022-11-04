using UnityEngine;
[CreateAssetMenu(menuName="GameRole/PlayerAttribute")]
public class RoleAttribute : ScriptableObject
{
    [Header("PlayerState")]
    public float healthy = 100f;
    [Header("Movement")]
    public float speed = 5f; //role's move spped
    // public float crouchSpeedDivisor = 2f;//denominator of roles' move speed lost 
    public float JumpRatio = 17f;//jump 125% higher than normal
    public float gravity = 7;
    // public float crouchJumpRatio = 1.6f;//jump 160% higher than normal
    // public float jumpForce = 13f; //the force
    public int faceDirection = 1;
    // public int DoubleJumpY = 1;
    // public int DoubleJumpX = 1;

    [Header("Collider Detection")]
    public LayerMask groundLayer;
    // public Transform castPoint;

    [Header("State")]
    // public bool isCrouching;
    public bool isOnGround;
    public bool isJumping;
    public bool isMoving;
    // public bool isHeadBlocked;
    // public bool isHanging;
    public bool isForwardJumping;
    public bool isAttachking;
    public bool isJumpingPressed;
    public bool isMovingPressed;
    public bool isDoubleJumping;

    [Header("Environment Detection")]
    // public float playerHeight;
    public float xVelocity;
    public float yVelocity;
    public bool jumpPressed;
    // public bool crouchHeld;
    // public bool upHolding;
    public bool commonAttackCast;
    public bool specialAttackCast;
    public bool ultimateAttackCast;
    // public Vector2 colliderStandSize;
    // public Vector2 colliderStandOffset;
    // public Vector2 colliderCrouchSize;
    // public Vector2 colliderCrouchOffset;
    [Header("Game Setting")]
    public int frontSizeNormal = 16;
    public int frontSizeCrit = 25;
    public void OnEnable()
    {
        // isCrouching = false;
        isOnGround = false;
        isJumping = false;
        isMoving = false;
        // isHeadBlocked = false;
        // isHanging = false;
        isForwardJumping = false;
        isAttachking = false;
        isJumpingPressed = false;
        isMovingPressed = false;
    }
    public void Initialize(BoxCollider2D thisBoxCollider2D, Transform transform, Transform _castPoint)
    {
        // playerHeight = thisBoxCollider2D.size.y;
        //collider size defined
        // colliderStandSize = thisBoxCollider2D.size;
        // colliderStandOffset = thisBoxCollider2D.offset; 
        // colliderCrouchSize = new Vector2(thisBoxCollider2D.size.x, thisBoxCollider2D.size.y/2f);
        // colliderCrouchOffset = new Vector2(thisBoxCollider2D.offset.x, thisBoxCollider2D.offset.y/2f);
        //ensure direction
        float x = transform.localScale.x;
        if(x==-1) faceDirection=-1;else faceDirection=1;
        // castPoint = _castPoint;
    }
}

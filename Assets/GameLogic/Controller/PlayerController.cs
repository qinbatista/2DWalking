using UnityEngine;

using UnityEngine.InputSystem;
using System.Collections;
public enum GroundType
{
    None,
    Soft,
    Hard
}

public class PlayerController : MonoBehaviour
{
    readonly Vector3 flippedScale = new Vector3(-1, 1, 1);
    readonly Quaternion flippedRotation = new Quaternion(0, 0, 1, 0);

    [Header("Character")]
    [SerializeField] Animator animator = null;
    [SerializeField] Transform puppet = null;
    [SerializeField] CharacterAudio audioPlayer = null;

    [Header("Tail")]
    [SerializeField] Transform tailAnchor = null;
    [SerializeField] Rigidbody2D tailRigidbody = null;

    [Header("Equipment")]
    [SerializeField] Transform handAnchor = null;
    [SerializeField] UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary = null;

    [Header("Movement")]
    private Rigidbody2D thisRigidbody2D;
    


    private Rigidbody2D controllerRigidbody;
    private BoxCollider2D thisBoxCollider2D;
    private Collider2D controllerCollider;
    private LayerMask softGroundMask;
    private LayerMask hardGroundMask;

    private Vector2 movementInput;
    private bool jumpInput;

    private Vector2 prevVelocity;
    private GroundType groundType;
    private bool isFlipped;
    private bool isJumping;
    private bool isFalling;

    private int animatorGroundedBool;
    private int animatorRunningSpeed;
    private int animatorJumpTrigger;

    public bool CanMove { get; set; }



    //new down
    public RoleAttribute roleAttribute;
    public static PlayerController Instance { get; private set; } // static singleton
    private float groundDistance = 0.2f;
    private Vector2 left_foot_position;
    private Vector2 right_foot_position;
    private Vector3 rawInputMovement;
    private bool startMoveJump=false;
    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;
    RaycastHit2D leftCheckJump;
    RaycastHit2D rightCheckJump;
    public float bodyVelocityX = 0;
    float bodyVelocityY = 0;
    int GroundLayerID;
    void Start()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        thisBoxCollider2D = GetComponent<BoxCollider2D>();
        left_foot_position =  new Vector2(-thisBoxCollider2D.size.x* Mathf.Abs(transform.localScale.x)/2,0);
        right_foot_position = new Vector2(thisBoxCollider2D.size.x* Mathf.Abs(transform.localScale.x)/2,0);
        thisRigidbody2D.gravityScale = roleAttribute.gravity;
        controllerRigidbody = GetComponent<Rigidbody2D>();
        controllerCollider = GetComponent<Collider2D>();
        GroundLayerID = LayerMask.NameToLayer("Ground");
    }
    void FixedUpdate()
    {
        RolePhysicalDetection(); 
        ControllerMovement();  
    }

    public void RolePhysicalDetection()
    {
        leftCheck = Raycast(left_foot_position, Vector2.down, groundDistance, roleAttribute.groundLayer);
        rightCheck = Raycast(right_foot_position, Vector2.down, groundDistance, roleAttribute.groundLayer);
        if(leftCheck || rightCheck)
        {
            roleAttribute.isOnGround = true;
            roleAttribute.isJumping=false;
            // roleAttribute.isJumping=false;
            // startMoveJump =false;
            // roleAttribute.isDoubleJumping = false;
            // bodyVelocityX=0;
        }
        else 
        {
            // startMoveJump = false;
            roleAttribute.isOnGround = false;
            roleAttribute.isJumping=true;
            // roleAttribute.isJumping=true;
        }
        // if(groundDistance == 0)
        // {

        // }
        // if(startMoveJump)
        // {
        //     roleAttribute.isJumping=true;
        // }
    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);
        Color color = hit ? Color.red: Color.green;
        Debug.DrawRay(pos+ offset, rayDirection * length,color);
        return hit;
    }

    bool stopPushing = false;
    void ControllerMovement()
    {   
        if(roleAttribute.xVelocity==0)
            return;
        
        if(roleAttribute.xVelocity < 0 )
        {
            roleAttribute.faceDirection=-1;
            transform.localScale = new Vector3(roleAttribute.faceDirection* Mathf.Abs(transform.localScale.x),transform.localScale.y,1);
        }
        else if(roleAttribute.xVelocity > 0)
        {
            roleAttribute.faceDirection=1;
            transform.localScale = new Vector3(roleAttribute.faceDirection*Mathf.Abs(transform.localScale.x),transform.localScale.y,1);
        }
        //moving role if role is walking or forwarding jumping

        if(roleAttribute.isJumping==false) 
        {
            // Debug.Log("isMoving="+roleAttribute.xVelocity);
            thisRigidbody2D.velocity = new Vector2(roleAttribute.xVelocity * roleAttribute.speed, 0);
        }

        // Debug.Log("Xposition="+Xposition+" transform.position.x="+transform.position.x);
        if(Mathf.Approximately(Xposition,transform.position.x))
        {
            // Debug.Log("forwarding");
            thisRigidbody2D.velocity = new Vector2(bodyVelocityX, thisRigidbody2D.velocity.y);
            // if(Mathf.Abs(bodyVelocityX)==Mathf.Abs(thisRigidbody2D.velocity.x))
            // {
            //     stopPushing = true;
            // }
            // isFixed=true;
            // Debug.Log("is jump and Moving ="+roleAttribute.xVelocity);
            // thisRigidbody2D.velocity = new Vector2(roleAttribute.xVelocity * roleAttribute.speed, 0);
        }
        if(roleAttribute.isOnGround)
        {
            bodyVelocityX=0;
            // Debug.Log("bodyVelocityX="+bodyVelocityX);
        }
        // if(bodyVelocityX==0&&roleAttribute.xVelocity==0)
        // {
        //     Debug.Log("stop on update");
        //     thisRigidbody2D.velocity = Vector2.zero;
        // }
        // else if(roleAttribute.isForwardJumping)
        // {
            // Debug.Log("isMoving");
            // thisRigidbody2D.velocity = new Vector2(roleAttribute.faceDirection * roleAttribute.speed, thisRigidbody2D.velocity.y);   
        // }
        
    }
    public void OnMovement(InputAction.CallbackContext context)
    {

        roleAttribute.xVelocity = context.ReadValue<Vector2>().x;
        roleAttribute.yVelocity = context.ReadValue<Vector2>().y;
        if(context.performed)
        {
            Debug.Log("context="+context);
            roleAttribute.isMoving=true;
        }
        if(context.canceled)
        {
            Debug.Log("context="+context);
            roleAttribute.isMoving=false;
            // thisRigidbody2D.velocity = Vector2.zero;
        }
        // Debug.Log("bodyVelocityX="+bodyVelocityX+" roleAttribute.xVelocity="+roleAttribute.xVelocity);
        if(bodyVelocityX==0&&roleAttribute.xVelocity==0)
        {
            // Debug.Log("stop on movement");
            thisRigidbody2D.velocity = Vector2.zero;
        }
        // Debug.Log(inputMovement+" context="+context);
        // roleAttribute.xVelocity = inputMovement.x;
        // roleAttribute.yVelocity = inputMovement.y;
        // rawInputMovement = new Vector3(inputMovement.x, 0, 0);
        
    }
    float Xposition = 0;
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // if(roleAttribute.xVelocity == 1)
            // {
            //     Debug.Log("move jump");
            //     startMoveJump=true;
            //     // StartCoroutine()
            //     // thisRigidbody2D.AddForce( new Vector2(roleAttribute.speed,roleAttribute.JumpRatio) , ForceMode2D.Impulse);
            //     // thisRigidbody2D.velocity = roleAttribute.JumpRatio;
            //     // thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, roleAttribute.JumpRatio);
            // }
            // else
            // {
            //     // thisRigidbody2D.velocity = new Vector2(thisRigidbody2D.velocity.x, roleAttribute.JumpRatio);
            // }
            bodyVelocityX = roleAttribute.xVelocity*roleAttribute.speed;
            Debug.Log("given value:"+bodyVelocityX);
            bodyVelocityY = roleAttribute.JumpRatio;
            thisRigidbody2D.velocity = new Vector2(bodyVelocityX, bodyVelocityY);
            groundDistance = 0;
            roleAttribute.isJumping=true;
            Xposition = transform.position.x;
            // stopPushing=false;
            // StartCoroutine(JumpCooldown());
            // stopMoving = true;
            // roleAttribute.isJumping =true;
            // else
            // {
            //     // Debug.Log("context="+context+" jumpForce="+1111);
            //     thisRigidbody2D.AddForce( new Vector2(0,roleAttribute.JumpRatio) , ForceMode2D.Impulse);
            //     // thisRigidbody2D.MovePosition(transform.position + new Vector3(0,1 * Time.deltaTime,0));
            //     roleAttribute.isJumping =true;
            // }
        }

    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == GroundLayerID)
        {
            // Debug.Log("stop on OnCollisionEnter2D");
            bodyVelocityX=0;
            groundDistance = 0.2f;
            if(bodyVelocityX==0&&roleAttribute.xVelocity==0)
            {
                // Debug.Log("stop on OnCollisionEnter2D");
                thisRigidbody2D.velocity = Vector2.zero;
            }
        }
    }
    // void OnCollisionExit(Collision2D other)
    // {
    //     if(other.gameObject.layer == GroundLayerID)
    //     {
    //         Debug.Log(other.gameObject.name);
    //         roleAttribute.isJumping=true;
    //         // thisRigidbody2D.velocity = Vector2.zero;
    //     }
    // }
}

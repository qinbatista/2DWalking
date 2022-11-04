using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController playerController;
    Animator animator;
    Rigidbody2D myRigidbody2D;

    int isCrouchID;
    int isOnGroundID;
    int isJumpID;
    int isHeadBlockedID;
    int isHangingID;
    int speedID;
    int fallID;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        myRigidbody2D = GetComponentInParent<Rigidbody2D>();

        isOnGroundID = Animator.StringToHash("isOnGround");
        isHangingID = Animator.StringToHash("isHanging");
        isCrouchID = Animator.StringToHash("isCrouching");
        speedID = Animator.StringToHash("speed");
        fallID = Animator.StringToHash("verticalVelocity");

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(speedID,Mathf.Abs(playerController.roleAttribute.xVelocity));
        animator.SetBool(isOnGroundID, playerController.roleAttribute.isOnGround);
        // animator.SetBool(isHangingID, playerController.roleAttribute.isHanging);
        // animator.SetBool(isCrouchID, playerController.roleAttribute.isCrouching);
        animator.SetFloat(fallID, myRigidbody2D.velocity.y);
    }

    public void SetpAuido()
    {
        // AudioManager.PlayFootstepAudio();
    }
    public void CrouchStepAudio()
    {
        // AudioManager.CrouchStepAudio();
    }
}

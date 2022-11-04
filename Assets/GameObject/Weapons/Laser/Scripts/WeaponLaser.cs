using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLaser : RoleWeapon
{
    // Start is called before the first frame update
    public GameObject bullet;
    private WeaponLaser Instance;
    public LineRenderer laserLine;
    public GameObject explosionVFXPrefab;
    public LayerMask Boundary;
    public bool isLaserAttacking;
    private RaycastHit2D hitInfo;
    private float laserRate = 0.2f;
    private float BasicSkillCooldownTime = 0.5f;
    
    private void Awake() {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }   
    }
    private void Start() 
    {
        laserLine.enabled = false;
        // GameManager.Instance.playerActionController.PlayerBodyController.WeaponLaserBasicSkill.performed += inputContext => BasicSkill(inputContext);
        // GameManager.Instance.playerActionController.PlayerBodyController.WeaponLaserBasicSkill.canceled += inputContext => BasicSkillCancel(inputContext);
    }
    private void LaserAttacking()
    {
        // hitInfo = Physics2D.Raycast(PlayerController.Instance.roleAttribute.castPoint.position, new Vector2(PlayerController.Instance.roleAttribute.faceDirection, 0), 1000, Boundary);
        // if(hitInfo)
        // {
        //     laserLine.SetPosition(0,PlayerController.Instance.roleAttribute.castPoint.position);
        //     laserLine.SetPosition(1, hitInfo.point);
        // }
        // else
        // {
        //     laserLine.SetPosition(0, PlayerController.Instance.roleAttribute.castPoint.position);
        //     laserLine.SetPosition(1, PlayerController.Instance.roleAttribute.castPoint.position + new Vector3(PlayerController.Instance.roleAttribute.faceDirection, 0,0)*100); 
        // } 
        laserLine.enabled = true;
    }
    private void Blasting()
    {
        if(Time.time>BasicSkillNextFireTime)
        {
            // Debug.Log(hitInfo.transform.name);
            if(hitInfo)
            {       
                GameObject VFXPrefab = Instantiate(explosionVFXPrefab, hitInfo.transform.position, hitInfo.transform.rotation);
                Destroy(VFXPrefab,1.0f);
            }
            BasicSkillNextFireTime = Time.time +BasicSkillCooldownTime;
        }
    }
    // Update is called once per frame
    override public void BasicSkill(InputAction.CallbackContext context) 
    {
        InvokeRepeating("LaserAttacking",0f,0.01f);
        InvokeRepeating("Blasting",0f,laserRate);
    }
    override public void BasicSkillCancel(InputAction.CallbackContext context) 
    {
        CancelInvoke("LaserAttacking");
        CancelInvoke("Blasting");
        laserLine.enabled = false;
    }
    override public void SpecialSkill(InputAction.CallbackContext context)
    {

    }
    override public void UltimateSkill(InputAction.CallbackContext context)
    {

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //cause damage
    }
}

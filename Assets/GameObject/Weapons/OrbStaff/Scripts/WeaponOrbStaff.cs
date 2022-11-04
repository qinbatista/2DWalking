using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponOrbStaff : RoleWeapon
{
    public WeaponObject staffAttribute;
    private bool isFiring;
    private bool isEnter;
    public static WeaponOrbStaff Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); } 
    }
    private void Start() 
    {
        GameManager.Instance.playerActionController.PlayerBodyController.BasicSkill.performed += inputContext => BasicSkill(inputContext); 
        GameManager.Instance.playerActionController.PlayerBodyController.BasicSkill.canceled += inputContext => BasicSkill(inputContext); 
    }
    private void FireBullet()
    {
        if(Time.time>staffAttribute.BasicSkillNextFireTime)
        {
            // GameObject thisAttack =Instantiate(staffAttribute.prefab, PlayerController.Instance.roleAttribute.castPoint.position, PlayerController.Instance.roleAttribute.castPoint.rotation);
            staffAttribute.BasicSkillNextFireTime = Time.time +staffAttribute.BasicSkillCooldownTime;  
        }
    }
    // Update is called once per frame
    override public void BasicSkill(InputAction.CallbackContext context)
    {
        isFiring=!isFiring;
        if(isFiring&&isEnter==false)
        {
            isEnter = true;
            InvokeRepeating("FireBullet",0f,0.01f);
        }
        else
        {
            isEnter = false;
            CancelInvoke("FireBullet");
        }
    }
    override public void SpecialSkill(InputAction.CallbackContext context)
    {
        if(Time.time>staffAttribute.SpecialSkillNextFireTime)
        {
            staffAttribute.SpecialSkillNextFireTime = Time.time +staffAttribute.SpecialSkillNextFireTime;
        }
    }
    override public void UltimateSkill(InputAction.CallbackContext context)
    {
        if(Time.time>staffAttribute.UltimateSkillNextFireTime)
        {
            staffAttribute.UltimateSkillNextFireTime = Time.time +staffAttribute.UltimateSkillNextFireTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //cause damage
    }
}

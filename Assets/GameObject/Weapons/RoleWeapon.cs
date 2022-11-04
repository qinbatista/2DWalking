using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class RoleWeapon : RoleItem
{
    // Start is called before the first frame update
    private bool isRightFilp = true;
    public GameObject RotatingNeededSprite; //some materials need rotate, or the material can't display normally
    protected float BasicSkillNextFireTime = 0;
    protected float SpecialSkillNextFireTime = 0;
    protected float UltimateSkillNextFireTime = 0;
    public static int totallCausedDamage = 0;

    [HideInInspector]
    public List<int> damageList;
    [HideInInspector]
    public List<bool> isCritList;
    [HideInInspector]
    public float DisplayingDeplayTime;
    [HideInInspector]
    public float AttackDelayTime;
    [HideInInspector]
    public DamageDisplayingManager.AttackTypeEnum AttackType = DamageDisplayingManager.AttackTypeEnum.SingalAttack;

    void Start()
    {
        FlipWeaponDisplaying();
    }
    protected void Update()
    {
        Movement();
    }
    private void Movement()
    {
        if(RotatingNeededSprite!=null)
        {
            if(PlayerController.Instance.roleAttribute.xVelocity<0 && isRightFilp == true)
            {
                isRightFilp=false;
                RotatingNeededSprite.transform.rotation = Quaternion.Euler(RotatingNeededSprite.transform.rotation.eulerAngles.x, RotatingNeededSprite.transform.rotation.eulerAngles.y+180, RotatingNeededSprite.transform.rotation.eulerAngles.z);
            }
            else if(PlayerController.Instance.roleAttribute.xVelocity>0 && isRightFilp == false)
            {
                isRightFilp=true;
                RotatingNeededSprite.transform.rotation = Quaternion.Euler(RotatingNeededSprite.transform.rotation.eulerAngles.x, RotatingNeededSprite.transform.rotation.eulerAngles.y-180, RotatingNeededSprite.transform.rotation.eulerAngles.z);
            }
        }
    }
    private void FlipWeaponDisplaying()
    {
        if(RotatingNeededSprite!=null)
        {
            if(PlayerController.Instance.roleAttribute.faceDirection==1) 
            {
                isRightFilp = true; 
                RotatingNeededSprite.transform.rotation = Quaternion.Euler(RotatingNeededSprite.transform.rotation.eulerAngles.x, RotatingNeededSprite.transform.rotation.eulerAngles.y, RotatingNeededSprite.transform.rotation.eulerAngles.z);
            }
            else
            { 
                isRightFilp =false;
                RotatingNeededSprite.transform.rotation = Quaternion.Euler(RotatingNeededSprite.transform.rotation.eulerAngles.x, RotatingNeededSprite.transform.rotation.eulerAngles.y+180, RotatingNeededSprite.transform.rotation.eulerAngles.z);
            }
        }
    }
    public virtual void BasicSkill(InputAction.CallbackContext context)
    {

    }
    public virtual void BasicSkillCancel(InputAction.CallbackContext context)
    {

    }
    public virtual void SpecialSkill(InputAction.CallbackContext context)
    {

}
    public virtual void SpecialSkillCancel(InputAction.CallbackContext context)
    {

    }
    public virtual void UltimateSkill(InputAction.CallbackContext context)
    {

    }
    public virtual void UltimateSkillCancel(InputAction.CallbackContext context)
    {

    }
    public void DamageCalculator(float damage, int attackTimes, float damageRangeRate, float CritRate, float critDamageRate ,out List<int> damageList, out List<bool> isCritList)
    {
        List<int> thisDamageList = new List<int>();
        List<bool> thisIsCritList = new List<bool>();
        float CurrentDamage = 0;
        float FinalDamage = 0;
        bool isCrit = false;
        for(int i = 0; i <attackTimes; i++)
        {
            CurrentDamage =  (1+Random.Range(-damageRangeRate,damageRangeRate)) * damage;
            if(((int)(CritRate*100)) > Random.Range(0,100))
            {
                isCrit = true;
                FinalDamage = CurrentDamage * critDamageRate;
            }
            else
            {
                isCrit = false;
                FinalDamage = CurrentDamage;
            }
            totallCausedDamage = totallCausedDamage+(int)FinalDamage;
            thisDamageList.Add((int)(FinalDamage));
            thisIsCritList.Add(isCrit);
        }
        damageList = thisDamageList;
        isCritList = thisIsCritList;
    }
}

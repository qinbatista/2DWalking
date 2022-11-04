using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBullet : RoleWeapon
{
    int target;
    public GameObject explosionVFXPrefab;
    public Rigidbody2D thisRigidbody2D;
    public LayerMask interactLayer;
    // public static OrbBullet Instance { get; private set; } // static singleton
    void Start()
    {
        thisRigidbody2D.velocity = new Vector2(PlayerController.Instance.roleAttribute.faceDirection * WeaponOrbStaff.Instance.staffAttribute.projectileSpeed, thisRigidbody2D.velocity.y);
    }
    void Awake()
    {
        // if (Instance == null) { Instance = this;  }
        // else { Destroy(gameObject); } 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((interactLayer.value & (1 << other.gameObject.layer)) > 0)
        // if(other.gameObject.layer == Boundary)
        {
            List<int> thisDamageList = new List<int>();
            List<bool> thisIsCritList = new List<bool>();
            int attackTimes = 1;

            AttackType = WeaponOrbStaff.Instance.staffAttribute.BasicSkillAttackType;
            if(AttackType != DamageDisplayingManager.AttackTypeEnum.SingalAttack)
                attackTimes = WeaponOrbStaff.Instance.staffAttribute.BasicSkillAttackTimes;
            WeaponOrbStaff.Instance.DamageCalculator(WeaponOrbStaff.Instance.staffAttribute.BasicSkillDamage,attackTimes,WeaponOrbStaff.Instance.staffAttribute.DamageRangeRate,WeaponOrbStaff.Instance.staffAttribute.CritRate,WeaponOrbStaff.Instance.staffAttribute.CritDamageRate,out thisDamageList,out thisIsCritList);
            damageList = thisDamageList;
            isCritList = thisIsCritList;
            DisplayingDeplayTime = WeaponOrbStaff.Instance.staffAttribute.BasicSkillDisplayingDeplayTime;
            AttackDelayTime = WeaponOrbStaff.Instance.staffAttribute.BasicSkillAttackDelayTime;

            GameObject thisExplosion = Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
            thisExplosion.transform.parent = other.gameObject.transform;
            this.GetComponent<SpriteRenderer>().enabled=false;
            this.GetComponent<CircleCollider2D>().enabled=false;
            for (int i = 0; i< gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            Destroy(this.gameObject,1.0f);
            Destroy(thisExplosion,1.0f);
            // AudioManager.PlayOrbAudio();
            // GameManager.PlayerGrabbedOrb(this);
        }
    }

}

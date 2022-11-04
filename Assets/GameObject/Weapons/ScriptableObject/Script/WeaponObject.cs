using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/Items/Weapon")]
public  class WeaponObject : ItemObject
{
    [Header("UserEquipment")]
    public float health = 100;
    public float defense = 100;
    public float damageBase = 100;
	public virtual void Initialize() { }
    [Header("BasicSkill")]
    public float BasicSkillCooldownTime = 0.5f;
    public float BasicSkillNextFireTime = 0;
    public float BasicSkillDamage = 1;
    public DamageDisplayingManager.AttackTypeEnum BasicSkillAttackType;
    public float BasicSkillDisplayingDeplayTime = 0;
    public float BasicSkillAttackDelayTime = 0;
    public int BasicSkillAttackTimes = 1;
    [Header("SpecialSkill")]
    public float SpecialSkillCooldownTime = 1f;
    public float SpecialSkillNextFireTime = 0;
    public float SpecialSkillDamage = 1;
    public DamageDisplayingManager.AttackTypeEnum SpecialSkillAttackType;
    public float SpecialSkillDisplayingDeplayTime = 0;
    public float SpecialSkillAttackDelayTime = 0;
    public int SpecialSkillAttackTimes = 1;
    [Header("UltimateSkill")]
    public float UltimateSkillCooldownTime = 1f;
    public float UltimateSkillNextFireTime = 0;
    public float UltimateSkillDamage = 1;
    public DamageDisplayingManager.AttackTypeEnum UltimateSkillAttackType;
    public float UltimateSkillDisplayingDeplayTime = 0;
    public float UltimateSkillAttackDelayTime = 0;
    public int UltimateSkillAttackTimes = 1;
    [Header("WeaponAttri")]
    public float CritRate = 0.15f;
    public float CritDamageRate = 0.5f;
    public float DamageRangeRate = 0.5f;
    public float ExtraBossDamage = 0f;
    public float projectileSpeed = 20f;
    public WeaponElementType weaponElementType;
    public WeaponType weaponType;
    
    private void Awake()
    {
        itemType = ItemType.Weapon;
        stackable = false;
    }
    private void OnEnable() {
        BasicSkillNextFireTime = 0;
        SpecialSkillNextFireTime = 0;
        UltimateSkillNextFireTime = 0;
    }
}
public enum WeaponType
{
    Laser,
    Orb 
}
public enum WeaponElementType
{
    ice, //30% get, 50% additional damage for fire,25% crit chance. 50% less damage for wind,25 crit chance, 20% missing, 
    fire,//30% get, 50% additional damage for wind.25% crit chance. 50% less damage for ice,25 crit chance, 20% missing,  
    wind,//30% get, 50% additional damage for ice.25% crit chance. 50% less damage for fire ,25 crit chance, 20% missing, 
    light, //4.5% get,100% received and caused from darkness 
    darkness, //4.5% get, wind,100% received and caused from light, no effect for and from ice,fire,wind
    sacred,//1% get all,damage received reduce 50%, all damage caused increase 50%, 100% crit chance, Electronics from four-dimensional space

}
[System.Serializable]
public class WeaponData
{
    public int Id;
    public bool stackable;
    public ItemType itemType;
    public string itemName;
    public string description;
    public WeaponType weaponType;
    public float damage_base,damage_base_rate,basic_skill_cool_down,basic_skill_damage_rate,special_skill_cool_down,special_skill_damage_rate,ultimate_skill_cool_down,ultimate_skill_damage_rate;
    public float crit_rate,crit_damage_rate,final_damage_range_rate,extra_boss_damage,health,defense,defense_recover_rate,projectile_speed,attack_speed;
    public float normal_element, special_element;
    public WeaponElementType weapon_element_type;
    public WeaponData(WeaponObject weapon)
    {
        // Debug.Log("weapon.weaponType.ToString()="+weapon.weaponType.ToString());
        // Debug.Log("name="+GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["name"]);
        // Debug.Log("description="+GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["description"]);
        Id = weapon.Id;
        stackable = weapon.stackable;
        itemType = weapon.itemType;
        itemName = GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["name"].ToString();
        description = GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["description"].ToString();
        weaponType = weapon.weaponType;
        damage_base = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["damage_base"].ToString());
        damage_base_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["damage_base_rate"].ToString());
        basic_skill_cool_down = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["basic_skill_cool_down"].ToString());
        basic_skill_damage_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["basic_skill_damage_rate"].ToString());
        special_skill_cool_down = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["special_skill_cool_down"].ToString());
        special_skill_damage_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["special_skill_damage_rate"].ToString());
        ultimate_skill_cool_down = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["ultimate_skill_cool_down"].ToString());
        ultimate_skill_damage_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["ultimate_skill_damage_rate"].ToString());
        crit_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["crit_rate"].ToString());
        crit_damage_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["crit_damage_rate"].ToString());
        // Debug.Log(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["final_damage_range_rate"]);
        final_damage_range_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["final_damage_range_rate"].ToString());
        extra_boss_damage = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["extra_boss_damage"].ToString());
        // Debug.Log("1");
        projectile_speed = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["projectile_speed"].ToString());
        // Debug.Log("1");
        health = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["health"].ToString());
        defense = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["defense"].ToString());
        attack_speed = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["attack_speed"].ToString());
        
        normal_element = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["normal_element"].ToString());
        special_element = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["special_element"].ToString());
        // 60
        // 39
        // 1
        if(Random.Range(0,100)<normal_element)
        {
            weapon_element_type = (WeaponElementType)Random.Range(0, 3);
        }
        else
        {
            if(Random.Range(0,100-normal_element)<special_element)
            {
                weapon_element_type = (WeaponElementType)Random.Range(3, 5);
            }
            else
            {
                weapon_element_type = (WeaponElementType)Random.Range(5, 6);
            }
        }
        // int ice =0;
        // int fire =0;
        // int wind =0;
        // int dark=0,light=0;
        // int sacred = 0;
        // for(int i = 0 ;i<100;i++)
        // {

        //     if(Random.Range(0,100)<normal_element)
        //     {
        //         // weapon_element_type = (WeaponElementType)Random.Range(0, 3);
        //         if(Random.Range(0, 3)==0)
        //             ice++;
        //         if(Random.Range(0, 3)==1)
        //             fire++;
        //         if(Random.Range(0, 3)==2)
        //             wind++;
        //     }
        //     else
        //     {
        //         if(Random.Range(0,100-normal_element)<special_element)
        //         {
        //             // weapon_element_type = (WeaponElementType)Random.Range(3, 5);
        //             if(Random.Range(3, 5)==3)
        //                 dark++;
        //             if(Random.Range(3, 5)==4)
        //                 light++;
        //         }
        //         else
        //         {
        //             // weapon_element_type = (WeaponElementType)Random.Range(5, 6);
        //             if(Random.Range(5, 6)==5)
        //                 sacred++;
        //         }
        //     }
        // }
        // Debug.Log("ice="+ice+" fire="+fire +" wind="+wind +" dark="+dark+" light="+light+ " sacred="+sacred);
        defense_recover_rate = float.Parse(GameManager.Instance.weapon_config[weapon.weaponType.ToString()]["defense_recover_rate"].ToString());
        // Debug.Log("1");
        // stackable = weapon.stackable;
        // itemType = weapon.itemType;
        // description = weapon.description;
        // weaponType = weapon.weaponType;
        // weaponElementType = (WeaponElementType)Random.Range(0, System.Enum.GetValues(typeof(WeaponElementType)).Length);

        // itemAttribute = new ItemAttribute[item.itemAttribute.Length];
        // for (int i =0; i<itemAttribute.Length; i++)
        //     itemAttribute[i] = new ItemAttribute(item.itemType, item.weaponType);
    }
}
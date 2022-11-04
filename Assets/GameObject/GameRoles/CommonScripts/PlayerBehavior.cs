using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    string LayerName = "Attack";
    string DamageDislayingPointName = "DamageTextPoint";
    DamageDisplayingManager myDamageDisplayingManager;
    
    public static PlayerBehavior Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); } 
    }
    void Start()
    {
        myDamageDisplayingManager = (DamageDisplayingManager)gameObject.transform.Find(DamageDislayingPointName).GetComponent(typeof(DamageDisplayingManager));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(LayerName))
        {
            RoleWeapon OrbBullet = other.gameObject.GetComponent<RoleWeapon>();
            myDamageDisplayingManager.Attack(OrbBullet.AttackType, OrbBullet.damageList,OrbBullet.isCritList,OrbBullet.DisplayingDeplayTime, OrbBullet.AttackDelayTime);
        }
    }
}

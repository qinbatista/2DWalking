using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageDisplayingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject damageTextFront;
    public GameObject damageTextFrontStatic;
    public RoleAttribute roleAttribute;

    public int totalReceivedDamage;
    public enum AttackTypeEnum
    {
        SingalAttack,
        MultipleAttack,
        ConsistentAttack
    }
    public static DamageDisplayingManager Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); } 
    }
    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Q))
        {
            print("Produced:"+RoleWeapon.totallCausedDamage+",Received:"+totalReceivedDamage);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            RoleWeapon.totallCausedDamage = 0;
            totalReceivedDamage = 0;
            print("Cleaned Produced:"+RoleWeapon.totallCausedDamage+",Received:"+totalReceivedDamage);
        }

        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //     List<int> damageList= {Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999)};
        //     bool [] isCritList = {true,false,true,true,true,false,true,true};
        //     MultipleAttack(damageList,isCritList,0,0.1f);
        // }
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     int [] damageList= {Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999),Random.Range(5000,9999)};
        //     bool [] isCritList = {true,true,true,true,true,true,true,true};
        //     ConsistentAttack(damageList,isCritList,1f,0.1f);
        // }
        #endif
    }
    public void Attack(AttackTypeEnum type, List<int> damageList, List<bool> isCritList, float DisplayingDeplayTime,float AttackDelayTime)
    {
        
        switch (type)
        {
            case AttackTypeEnum.SingalAttack:SingalAttack(damageList,isCritList,DisplayingDeplayTime,AttackDelayTime);break;
            case AttackTypeEnum.MultipleAttack:MultipleAttack(damageList,isCritList,DisplayingDeplayTime,AttackDelayTime);break;
            case AttackTypeEnum.ConsistentAttack:ConsistentAttack(damageList,isCritList,DisplayingDeplayTime,AttackDelayTime); break;
            default:break;
        }
    }
    public void SingalAttack(List<int> damageList, List<bool> isCritList, float DisplayingDeplayTime, float AttackDelayTime)
    {
        StartCoroutine(SingalAttackEnume(damageList,isCritList,DisplayingDeplayTime,AttackDelayTime));
    }
    public void MultipleAttack(List<int> damageList, List<bool> isCritList, float DisplayingDeplayTime, float AttackDelayTime)
    {
        StartCoroutine(MultipleAttackEnume(damageList,isCritList,DisplayingDeplayTime,AttackDelayTime));
    }
    public void ConsistentAttack(List<int> damageList, List<bool> isCritList, float DisplayingDeplayTime, float AttackDelayTime)
    {
        StartCoroutine(ConsistentAttackEnume(damageList,isCritList,DisplayingDeplayTime, AttackDelayTime));
    }
    public IEnumerator SingalAttackEnume(List<int> damageList, List<bool> isCrit, float DisplayingDeplayTime, float AttackDelayTime)
    {
        yield return new WaitForSeconds(DisplayingDeplayTime);
        if(!isCrit[0])
        {
            GameObject _damageText = Instantiate(damageTextFront,transform);
            _damageText.transform.GetComponent<TextMeshPro>().fontSize=roleAttribute.frontSizeNormal;
            _damageText.transform.GetComponent<TextMeshPro>().SetText(damageList[0].ToString());
            _damageText.transform.GetComponent<TextMeshPro>().faceColor = new Color32(255,211,4,255);
            totalReceivedDamage = totalReceivedDamage+damageList[0];
        }
        else
        {
            GameObject _damageText = Instantiate(damageTextFront,transform);
            _damageText.transform.GetComponent<TextMeshPro>().fontSize=roleAttribute.frontSizeCrit;
            _damageText.transform.GetComponent<TextMeshPro>().SetText(damageList[0].ToString());
            _damageText.transform.GetComponent<TextMeshPro>().faceColor = new Color32(32,4,255,255);
            totalReceivedDamage = totalReceivedDamage+damageList[0];
        }
        yield return new WaitForSeconds(AttackDelayTime);
        // PlayerBehavior.Instance.damageList.Clear();
        // PlayerBehavior.Instance.isCritList.Clear();
    }
    public IEnumerator MultipleAttackEnume(List<int> damageList, List<bool> isCritList, float DisplayingDeplayTime, float AttackDelayTime)
    {
        yield return new WaitForSeconds(DisplayingDeplayTime);
        for(int i = 0; i< damageList.Count; i++)
        {
            GameObject _damageText = Instantiate(damageTextFrontStatic,transform);
            if(!isCritList[i])
            {
                _damageText.transform.GetComponent<TextMeshPro>().fontSize=roleAttribute.frontSizeNormal;
                _damageText.transform.GetComponent<TextMeshPro>().faceColor = new Color32(255,211,4,255);
                _damageText.transform.localPosition = new Vector3(0f,1.5f*i,0f);
                
            }
            else
            {
                _damageText.transform.GetComponent<TextMeshPro>().fontSize=roleAttribute.frontSizeCrit;
                _damageText.transform.GetComponent<TextMeshPro>().faceColor = new Color32(32,4,255,255);
                _damageText.transform.localPosition = new Vector3(0f,1.5f*i,0f);
            }
            totalReceivedDamage = totalReceivedDamage+damageList[i];
            _damageText.transform.GetComponent<TextMeshPro>().SetText(damageList[i].ToString());
            yield return new WaitForSeconds(AttackDelayTime);
        }
    }
    public IEnumerator ConsistentAttackEnume(List<int> damageList, List<bool> isCritList, float DisplayingDeplayTime, float AttackDelayTime)
    {
        yield return new WaitForSeconds(DisplayingDeplayTime);
        for(int i = 0; i< damageList.Count; i++)
        {
            if(!isCritList[i])
            {
                GameObject _damageText = Instantiate(damageTextFront,transform);
                _damageText.transform.GetComponent<TextMeshPro>().fontSize=roleAttribute.frontSizeNormal;
                _damageText.transform.GetComponent<TextMeshPro>().SetText(damageList[i].ToString());
                _damageText.transform.GetComponent<TextMeshPro>().faceColor = new Color32(255,211,4,255);
            }
            else
            {
                GameObject _damageText = Instantiate(damageTextFront,transform);
                _damageText.transform.GetComponent<TextMeshPro>().fontSize=roleAttribute.frontSizeCrit;
                _damageText.transform.GetComponent<TextMeshPro>().SetText(damageList[i].ToString());
                _damageText.transform.GetComponent<TextMeshPro>().faceColor = new Color32(32,4,255,255);
            }
            totalReceivedDamage = totalReceivedDamage+damageList[i];
            yield return new WaitForSeconds(AttackDelayTime);
        }
    }
}

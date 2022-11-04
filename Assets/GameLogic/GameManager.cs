using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    // SceneFader fader;
    // List<Orb> orbs;
    // Door lockedDoor;
    float gameTime;
    // public int orbNum;
    bool gameIsOVer;
    public GameObject mobileController;
    public PlayerActionController playerActionController;
    public JObject weapon_config = new JObject();
    public static GameManager Instance { get; private set; } // static singleton
    private void Awake()
    {
        if (Instance == null) {Instance = this;}
        else { Destroy(gameObject); }
        if (Application.platform == RuntimePlatform.Android||Application.platform == RuntimePlatform.IPhonePlayer)
            mobileController.SetActive(true);
        DontDestroyOnLoad(this);
        playerActionController = new PlayerActionController();
        // orbs = new List<Orb>();
        LoadWeaponData();
    }

    private void Update()
    {
        if(gameIsOVer)
            return;
        // orbNum = instance.orbs.Count;
        gameTime += Time.deltaTime;
        // UIManager.UpdateTimeUI(gameTime);
    }
    // public static void RegisterSceneFader(SceneFader obj)
    // {
    //     // instance.fader = obj;
    // }
    // public static void RegistDoor(Door door)
    // {
    //     instance.lockedDoor = door;
    // }
    // public static void RegisterOrb(Orb orb)
    // {
    //     if(instance == null)
    //         return;
    //     if(!instance.orbs.Contains(orb))
    //         instance.orbs.Add(orb);
    //     UIManager.UpdateOrbUI(instance.orbs.Count);
    // }

    // public static void PlayerWon()
    // {
    //     instance.gameIsOVer=true;
    //     UIManager.DisplayGameOver();
    //     AudioManager.PlayerWonAudio();
    // }
    public static bool GameOver()
    {
        return instance.gameIsOVer;
    }
    // public static void PlayerGrabbedOrb(Orb orb)
    // {
    //     if(instance.orbs.Contains(orb))
    //         instance.orbs.Remove(orb);
    //     if(instance.orbs.Count == 0)
    //         instance.lockedDoor.Open();
    //     UIManager.UpdateOrbUI(instance.orbs.Count);
    // }
    // Update is called once per frame
    // void RestartScene()
    // {
    //     instance.orbs.Clear();
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }
    private void OnEnable() 
    {
        playerActionController.Enable();
    }
    private void OnDisable() 
    {
        playerActionController.Disable();
    }
    public void LoadWeaponData()
    {
        // string weapon_id = "weaponID";
        // string weapon_data_unity_path  = string.Concat(Application.dataPath,"/Resources/weapon_data.bin");
        // if(File.Exists(weapon_data_unity_path))
        // {
        //     IFormatter formatter = new BinaryFormatter();
        //     Stream stream = new FileStream(weapon_data_unity_path,FileMode.Open,FileAccess.Read);
        //     // Debug.Log(stream.Length);
        //     FormateData formateData = new FormateData();
        //     formateData = (FormateData)formatter.Deserialize(stream);
        //     stream.Close();
        //     string[] protocols = formateData.weapon_data[0].Split(',');
        //     for(int i = 1; i<formateData.weapon_data.Count-1; i++)
        //     {
        //         string[] info = formateData.weapon_data[i].Split(',');
        //         var weapon_info = new JObject();
        //         for(int j = 0; j< info.Length; j++)
        //         {
        //             // Debug.Log("protocols[j]="+protocols[j]+",info[i]="+info[j]);
        //             weapon_info.Add(protocols[j], info[j]);
        //         }
        //         weapon_config.Add(weapon_info[weapon_id].ToString(),weapon_info);
        //         weapon_info = new JObject();
        //     }
        //     // Debug.Log(weapon_config.ToString());
        // }
        // else
        // {
        //     Debug.Log("loading assest from website");
        // }
    }
}

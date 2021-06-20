using UnityEngine;
//using System.Collections;
using UnityEngine.UI;
public class LevelControll : MonoBehaviour {
    public int Level;
    public GameData.MISSIONTYPE MissionType;
    public int TargetScore;
    public GameData.Collection[] Collection;
    public GameObject panelShop;
    public int TimePlay;
    
	// Use this for initialization
    void Awake()
    {
        name = "Level" + Level.ToString();        
        //GetComponentInChildren<Text>().text = Level.ToString();
        GetComponent<Button>().onClick.AddListener(OnClick);
        //GetComponentInChildren<Text>().resizeTextMaxSize = 60;        
    }

	void Start () {
        
    }

    
	
	// Update is called once per frame
	void Update () {
        //if (PlayerPrefsControll.LevelSuccess(Level))
        //{
        //    GetComponent<Image>().color = Color.white;
        //    GetComponent<Button>().enabled = true;
        //}
        //else
        //{
        //    GetComponent<Image>().color = Color.gray;
        //    GetComponent<Button>().enabled = false;
        //}
	
	}
    void OnClick()
    {
        SoundController.Intance.Play(SoundController.CLIP.CLICK);
        GameData.TimePlay = TimePlay;
        GameData.Level = Level;
        GameData.MissionType = MissionType;
        GameData.TargetScore = TargetScore;
        GameData.KeoCollection = Collection;
        GetComponent<LevelShow>().setImageKeoCollection();
        panelShop.SetActive(true);      

    }
}

using UnityEngine;
using UnityEngine.UI;

public class LevelShow : MonoBehaviour {
    public Text txtLevel;
    public Text txtTarget;
    public Image keo0;
    public Image keo1;
    public Image keo2;
    public Sprite dua, dau, hanh, tao, ot, ngo, playOn, playOff;
    public Image play;
    private LevelControll levelcontroll;
    // Use this for initialization
    void Awake()
    {
        levelcontroll = GetComponent<LevelControll>();
        if (GameData.TestGame)
        {
        //    print("test");
            return;
        }
        else
        {
        //    print("no test");
            if (PlayerPrefsControll.LevelSuccess(levelcontroll.Level - 1))
            {
                play.sprite = playOn;
                GetComponent<Button>().enabled = true;
            }
            else
            {
                play.sprite = playOff;
                GetComponent<Button>().enabled = false;
            }
        }
        txtTarget.gameObject.SetActive(false);
        keo0.gameObject.SetActive(false);
        keo1.gameObject.SetActive(false);
        keo2.gameObject.SetActive(false);
    }
	void Start () {
        
        txtLevel.text = levelcontroll.Level.ToString();
        txtLevel.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-9,15,0);
        if (levelcontroll.MissionType == GameData.MISSIONTYPE.COLLECTION)
        {
            if (levelcontroll.Collection.Length == 1)
            {
                keo0.gameObject.SetActive(true);
                keo0.sprite = GetSprite(levelcontroll.Collection[0]);
                keo0.GetComponentInChildren<Text>().text = levelcontroll.Collection[0].Count.ToString();
            }
            else if (levelcontroll.Collection.Length == 2)
            {
                keo1.gameObject.SetActive(true);
                keo1.GetComponent<Image>().sprite = GetSprite(levelcontroll.Collection[0]);
                keo1.GetComponentInChildren<Text>().text = levelcontroll.Collection[0].Count.ToString();
                keo2.gameObject.SetActive(true);
                keo2.GetComponent<Image>().sprite = GetSprite(levelcontroll.Collection[1]); ;
                keo2.GetComponentInChildren<Text>().text = levelcontroll.Collection[1].Count.ToString();
            }
        }
        else
        {
            txtTarget.gameObject.SetActive(true);
            txtTarget.text = "Target:\n" + levelcontroll.TargetScore.ToString();
        }
        if (PlayerPrefsControll.GetHigh(levelcontroll.Level) != 0)
        {
            foreach (Text txt in GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name == "High")
                {
                    txt.text = PlayerPrefsControll.GetHigh(levelcontroll.Level).ToString();
                }
            }
        }

    }

    public void setImageKeoCollection()
    {
        if(GameData.MissionType == GameData.MISSIONTYPE.COLLECTION)
        {
            GameData.Keo1 = GetSprite(levelcontroll.Collection[0]);
            if (GameData.KeoCollection.Length == 2)
            {
                GameData.Keo2 = GetSprite(levelcontroll.Collection[1]);
            }           
        }
        
    }
    // Update is called once per frame
    private Sprite GetSprite(GameData.Collection collection)
    {
        switch (collection.KeoCollection)
        {
            case GameData.KEOCOLLECTION.DAU:
                return dau;
            case GameData.KEOCOLLECTION.DUA:
                return dua;
            case GameData.KEOCOLLECTION.HANH:
                return hanh;
            case GameData.KEOCOLLECTION.NGO:
                return ngo;
            case GameData.KEOCOLLECTION.OT:
                return ot;
            case GameData.KEOCOLLECTION.TAO:
                return tao;
            default:
                return null;
        }
    }
}

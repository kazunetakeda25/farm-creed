using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {
    public GameObject loading;
    public GameObject worldMap;
    public Scrollbar scrollbar;
    public GameObject Menu;
    public GameObject Shop;
    public GameObject Play;
    public GameObject Quit;
    public Button yes, no;
    public bool TestModel = false;
    public GameObject Sound;
    public Sprite soundOn, soundOff;
    public Text txtMyGold;
    public Button freeGold;


    // Use this for initialization
    void Awake()
    {
        GameData.TestGame = TestModel;
        PlayerPrefsControll.TheFirst();
        if (GameData.FirstStart)
        {
            GameData.FirstStart = false;
            StartCoroutine(Load());
        }
        else if (GameData.NextLevel)
        {
            GameData.NextLevel = false;
            Menu.SetActive(false);
            loading.SetActive(false);
            worldMap.SetActive(true);
            StartCoroutine(setScrollbarValue());
        }
        else
        {
            GoMenu();
           
        }
    }

    IEnumerator setScrollbarValue()
    {
        yield return new WaitForSeconds(0.1f);
        scrollbar.value = 1 - (PlayerPrefsControll.GetBest()) / 50f;
        //print(scrollbar.value);
        Canvas.ForceUpdateCanvases();
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3);
        GoMenu();
    }

   

    void Start() {
        StartCoroutine(start());
		GoogleAdmob.RequestBanner();
		GoogleAdmob.RequestInterstitial();
		if (GoogleAdmob.bannerView!=null) GoogleAdmob.bannerView.Show();
    }

    public void FreeGold()
    {
        print("gold for you");
        // show your ads for free Gold and up Gold for Player;
        /* to up gold, use code:
        
        GameData.Gold += _;  //insert your number at _
        PlayerPrefsControll.SaveGold(GameData.Gold);

        */
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(0.5f);

        SoundController.Intance.StopAll();
        if (System.DateTime.Now.Hour >= 6 && System.DateTime.Now.Hour <= 18)
        {
            SoundController.Intance.Play(SoundController.CLIP.MAINMENU_MORNING, true);
        }
        else
        {
            SoundController.Intance.Play(SoundController.CLIP.MAINMENU_NIGHT, true);
        }

    }
    // Update is called once per frame
    void Update() {
        txtMyGold.text = GameData.Gold.ToString();
        Back();
        if (Menu.activeSelf)
        {
            if (!GameData.Mute)
            {
                Sound.GetComponent<Image>().sprite = soundOn;
            }
            else
            {
                Sound.GetComponent<Image>().sprite = soundOff;
            }
        }
    }

    void GoMenu()
    {
        loading.SetActive(false);
        worldMap.SetActive(false);
        Menu.SetActive(true);        
    }
    public void GoMap()
    {
        worldMap.SetActive(true);       
        Menu.SetActive(false);
        loading.SetActive(false);
    }

   public void soundClick()
    {
        print(GameData.Mute + "sound");
        if (!GameData.Mute)
        {
            GameData.Mute = true;
            SoundController.Intance.StopAll(true);
            Sound.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            GameData.Mute = false;
            SoundController.Intance.StopAll(false);
            Sound.GetComponent<Image>().sprite = soundOn;
        }
    }

    void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Shop.activeSelf)
            {
                Shop.SetActive(false);
            }
            if (Menu.activeSelf)
            {
                if (Quit.activeSelf)
                {
                    Yes();
                }
                else
                {
                    Quit.SetActive(true);
                    yes.onClick.AddListener(Yes);
                    no.onClick.AddListener(No);
                }
            }
            else
            {
                GoMenu();
            }
        }
    }

    void Yes()
    {
        Application.Quit();
    }

    void No()
    {
        Quit.SetActive(false);
    }

    void OnApplicationQuit()
    {
        GameData.FirstStart = true;
        foreach (GameObject x in GameObject.FindObjectsOfType<GameObject>())
        {
            Destroy(x.gameObject);
        }    
    }
}

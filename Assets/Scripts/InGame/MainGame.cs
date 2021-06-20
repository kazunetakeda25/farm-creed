using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
    //public Color colo;
	// Use this for initialization
    private UnityEngine.UI.Text txtStart;
    private float timer = 4;
    private bool notAdd = true;
    public UnityEngine.UI.Text Timer;
    public UnityEngine.UI.Text txtScore;
    public GameObject target, keo1, keo2;
    public GameObject panelGray;
    public GameObject panelGameOver;
    public GameObject panelPause;
    private bool started = false;
    public static bool isrestart = false;

    void Awake()
    {
        GameData.Score = 0;
        GameData.NumKeo1 = 0;
        GameData.NumKeo2 = 0;
        GameData.AllLoadData();
        GameData.TypePlay = 2;
        GameData.BaseScore = 10;
        if (GameData.X2)
        {
            GameData.BaseScore *= 2;
            GameData.X2 = false;
        }
        GameData.TimePlay += GameData.TimePlus;
        GameData.TimePlus = 0;        
        if (GameData.TypePlay == 1)
        {
            GameData.MaxRow = 12;
        }
        else
        {
            GameData.MaxRow = 9;
        }
    }

	void Start () {        
        GameControll.isGameOver = false;
        GameControll.isPause = false;
        keo1.SetActive(false);
        keo2.SetActive(false);
        //panelGameOver.SetActive(false);
        //panelPause.SetActive(false);
        txtStart = GameObject.Find("txtStart").GetComponent<UnityEngine.UI.Text>();
        target.SetActive(false);
        txtStart.text = "";
        if (isrestart)
        {
            StartCoroutine(start(0f));
        }
        else
        {
            StartCoroutine(start(1.45f));
        }
		if (GoogleAdmob.bannerView!=null) GoogleAdmob.bannerView.Hide();
		if (GoogleAdmob.interstitial!=null) GoogleAdmob.ShowInterstitial();
	}

    IEnumerator start(float time)
    {        
        yield return new WaitForSeconds(time);
        panelGray.SetActive(false);
        started = true;
        
        if (System.DateTime.Now.Hour >= 6 && System.DateTime.Now.Hour <= 18)
        {
            SoundController.Intance.Play(SoundController.CLIP.BACKGROUND_MORNING, true);
        }
        else
        {
            SoundController.Intance.Play(SoundController.CLIP.BACKGROUND_NIGHT, true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!started)
        {
            return;
        }
       
        if (!notAdd)
        {
            return;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            txtStart.text = ((int)(timer / 1)).ToString();
        }
       
       
        if (timer <= 1 && notAdd)
        {
            gameObject.AddComponent<GameControll>();
            GetComponent<GameControll>().Target = target;
            GetComponent<GameControll>().Keo1 = keo1;
            GetComponent<GameControll>().Keo2 = keo2;
            GetComponent<GameControll>().txtTimer = Timer;
            GetComponent<GameControll>().txtScore = txtScore;
            GetComponent<GameControll>().PanelGameOver = panelGameOver;
            GetComponent<GameControll>().PanelPause = panelPause;
            txtStart.text = "";
            notAdd = false;
            enabled = false;
        }
	    
	}   
    Vector3 normalsize (Vector3 vector)
    {
        if (System.Math.Abs(vector.x) > System.Math.Abs(vector.y))
        {
            return new Vector3(vector.x / System.Math.Abs(vector.x), vector.y / System.Math.Abs(vector.x));
        }
        else
        {
            return new Vector3(vector.x / System.Math.Abs(vector.y), vector.y / System.Math.Abs(vector.y));
        }
    }
}

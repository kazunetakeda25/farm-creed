using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelGameOver : MonoBehaviour {
    public GameObject BG_GameOver;
    public GameObject keo1, keo2;
    public GameObject ReplayWenWin, ReplayWenFailed, Next, Cancel;
    public GameObject Target;
    public GameObject ThongBao;
    public GameObject Score;
	// Use this for initialization
	void Start () {
        ReplayWenFailed.GetComponent<Button>().onClick.AddListener(ButReplay);
        ReplayWenWin.GetComponent<Button>().onClick.AddListener(ButReplay);
        Next.GetComponent<Button>().onClick.AddListener(ButNext);
        Cancel.GetComponent<Button>().onClick.AddListener(ButCancel);
        keo1.SetActive(false);
        keo2.SetActive(false);
        ReplayWenFailed.SetActive(false);
        ReplayWenWin.SetActive(false);
        Next.SetActive(false);
        Target.SetActive(false);
        //BG_GameOver.GetComponent<Image>().color = Color.gray;
        BG_GameOver.GetComponent<Image>().fillAmount = 0;
        BG_GameOver.SetActive(false);        
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameControll.isGameOver)
        {
            StartCoroutine(delayGameOver());
        }
	
	}

    IEnumerator delayGameOver()
    {
        yield return new WaitForSeconds(1);
        GameOver();
    }
    void GameOver()
    {
        BG_GameOver.SetActive(true);
        BG_GameOver.GetComponent<Image>().fillAmount += Time.deltaTime;
        GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
        Score.GetComponent<Text>().text = "Score\n" + GameData.Score.ToString();

        if(GameData.MissionType == GameData.MISSIONTYPE.SCORE)
        {
            Target.SetActive(true);
            Target.GetComponent<Text>().text = "Target\n" + GameData.TargetScore.ToString();           

            if (GameData.Score >= GameData.TargetScore)
            {
                Completed();
            }
            else
            {
                Failed();
            }
        }
        else
        {
            if(GameData.KeoCollection.Length == 1)
            {
                keo1.SetActive(true);
                keo1.GetComponent<Image>().sprite = GameData.Keo1;
                keo1.GetComponentInChildren<Text>().text = GameData.NumKeo1.ToString() + "/" + GameData.KeoCollection[0].Count.ToString(); 
                if(GameData.NumKeo1>=GameData.KeoCollection[0].Count)
                {
                    Completed();
                }
                else
                {
                    Failed();
                }
            }
            else
            {
                keo1.SetActive(true);
                keo2.SetActive(true);
                keo1.GetComponent<Image>().sprite = GameData.Keo1;
                keo2.GetComponent<Image>().sprite = GameData.Keo2;
                keo1.GetComponentInChildren<Text>().text = GameData.NumKeo1.ToString() + "/" + GameData.KeoCollection[0].Count.ToString();
                keo2.GetComponentInChildren<Text>().text = GameData.NumKeo2.ToString() + "/" + GameData.KeoCollection[1].Count.ToString();
                if (GameData.NumKeo1 >= GameData.KeoCollection[0].Count && GameData.NumKeo2 >= GameData.KeoCollection[1].Count)
                {
                    Completed();
                }
                else
                {
                    Failed();
                }
            }           
        }
    }

    void Completed()
    {
        SoundController.Intance.Play(SoundController.CLIP.WIN);
        PlayerPrefsControll.SaveHigh(GameData.Level, GameData.Score);
        PlayerPrefsControll.SetBest(GameData.Level);
        
        ThongBao.GetComponent<Text>().text = "Level " + GameData.Level + "  completed";
        ThongBao.GetComponent<Text>().color = Color.white;
        ReplayWenWin.SetActive(true);
        Next.SetActive(true);
        PlayerPrefsControll.SaveLevel(GameData.Level);
    }

    void Failed()
    {
        SoundController.Intance.Play(SoundController.CLIP.FAIL);
        ThongBao.GetComponent<Text>().text = "Level " + GameData.Level + "  failed";
        ThongBao.GetComponent<Text>().color = Color.white;
        ReplayWenFailed.SetActive(true);
    }

    void ButReplay()
    {
        SoundController.Intance.Play(SoundController.CLIP.EAT);
        MainGame.isrestart = true;
        Application.LoadLevel(Application.loadedLevel);
    }

    void ButNext()
    {
        SoundController.Intance.Play(SoundController.CLIP.EAT);
        GameData.NextLevel = true;
        Application.LoadLevelAsync("Map");
    }

    void ButCancel()
    {
        SoundController.Intance.Play(SoundController.CLIP.EAT);
        Application.LoadLevelAsync("Map");
    }
}

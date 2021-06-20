using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameControll : MonoBehaviour {
    private GameObject[,] Keos;
    private static int Row, Column;
    public static float Spacing;
    private List<GameObject> listKeos, checkList;
    private static string checkKeoType;
   // private static Vector3 keoScale;
    public static int speedCandyFall;
    public static float startPointX, startPointY;    
    //private static int countCheckFail;
    public Text txtTimer;
    public Text txtScore;
    private static int Score;
    private float time_Score_plus;
    public GameObject Keo1, Keo2;
    public GameObject Target;
    public static bool isGameOver;
    public GameObject PanelGameOver;
    public GameObject PanelPause;
    public static bool isPause;
    private bool click;
    private float time_Eat_Keo;
    private int timeplay;
    private int oldTime, newTime;
    void Awake()
    {
        isPause = false;
        isGameOver = false;
        timeplay = GameData.TimePlay;
        Spacing = 0.085f;
        Row = 9;
        Column = 7;       
        speedCandyFall = 9;
        startPointX = -1.073f;
        startPointY = -1.376f;
        //Timer = GameObject.Find("Timer").GetComponent<Image>();
        //txtScore = GameObject.Find("txtScore").GetComponent<Text>();
        time_Score_plus = 0.05f;
        Score = 0;  
        StartCoroutine(loadMission());
        click = true;
    }

    IEnumerator loadMission()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameData.MissionType == GameData.MISSIONTYPE.SCORE)
        {
            Target.SetActive(true);
            Keo1.SetActive(false);
            Keo2.SetActive(false);
            Target.GetComponent<Text>().text ="Target: "+ GameData.TargetScore.ToString();
        }
        else
        {
            Target.SetActive(false);
            if (GameData.KeoCollection.Length == 1)
            {
                Keo1.SetActive(true);                
                Keo2.SetActive(false);

                Keo1.GetComponent<Image>().sprite = GameData.Keo1;
                Keo1.GetComponentInChildren<Text>().text = GameData.NumKeo1.ToString() + "/" + GameData.KeoCollection[0].Count.ToString();
            }
            else
            {
                Keo1.SetActive(true);
                Keo2.SetActive(true);

                Keo1.GetComponent<Image>().sprite = GameData.Keo1;
                Keo2.GetComponent<Image>().sprite = GameData.Keo2;

                Keo1.GetComponentInChildren<Text>().text = GameData.NumKeo1.ToString() + "/" + GameData.KeoCollection[0].Count.ToString();
                Keo2.GetComponentInChildren<Text>().text = GameData.NumKeo2.ToString() + "/" + GameData.KeoCollection[1].Count.ToString();

            }
        }
    }
    
   
	// Use this for initialization
	void Start () {
      
       // countCheckFail = 0;
        listKeos = new List<GameObject>();        
        Keos = new GameObject[GameData.MaxRow, Column];
        for(int x = 0; x<Row;x++)
        {
            for(int y = 0; y<Column; y++)
            {
                Keos[x, y] = (GameObject)Instantiate(GameData.getBean(), Vector3.zero, Quaternion.identity);
                Keos[x, y].AddComponent<Keo>();
                Keos[x, y].GetComponent<Keo>().Row = x;
                Keos[x, y].GetComponent<Keo>().Column = y;
               // Keos[x, y].AddComponent<SpriteRenderer>();

               // Keos[x, y].GetComponent<SpriteRenderer>().sprite = GameData.getBean();// (Sprite)Resources.Load(GameData.getBean(), typeof(Sprite));
                Keos[x, y].GetComponent<Keo>().type = Keos[x, y].GetComponentInChildren<SpriteRenderer>().sprite.name;
                Keos[x, y].name = "dau " + x + "_" + y;
                Keos[x, y].transform.localScale = GameData.KeoScale;
                Keos[x, y].GetComponent<Keo>().NameCollection = GameData.getKeoCollection(GameData.Name);
                Keos[x, y].tag = "keo";
               // Keos[x, y].AddComponent<BoxCollider2D>();
                if (y % 2 == 0)
                {
                    Keos[x, y].transform.position = new Vector3(startPointX + y * Keos[x, y].GetComponent<Collider2D>().bounds.size.x + y * y * Spacing, startPointY + x * Keos[x, y].GetComponent<Collider2D>().bounds.size.y + x * x * Spacing * 2 + 5f, 0);
                }
                else
                {
                    Keos[x, y].transform.position = new Vector3(startPointX + y * Keos[x, y].GetComponent<Collider2D>().bounds.size.x + y * y * Spacing, startPointY + x * Keos[x, y].GetComponent<Collider2D>().bounds.size.y + x * x * Spacing * 2 + 5f  + Keos[x, y].GetComponent<Collider2D>().bounds.size.y*0.5f, 0);            
                }
                
            }
        }
        GameData.vecReset = Keos[3, 3].transform.position;
        StartCoroutine(setOldTime());
        //CheckListFail();
	}

  

	// Update is called once per frame
	void Update () {
        txtScore.text = "Score "+GameData.Score.ToString();       
        if (Score > 0)
        {
            if (time_Score_plus > 0)
            {
                time_Score_plus -= Time.deltaTime;
            }
            else
            {
                time_Score_plus = 0.05f;
                Score -= 10;
                GameData.Score += 10;
            }
        }
        if (isGameOver)
        {
            PanelGameOver.SetActive(true);
            return;
        }
        if (isPause)
        {
            PanelPause.SetActive(true);
            return;
        }
        StartCoroutine(UpDateTimePlay());
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && click)
        {
            if (listKeos != null)
            {                
               // listKeos = null;
                listKeos = new List<GameObject>();
            }
            if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.gameObject.tag == "keo")
                {
                    GameData.TargetCombo = hit.collider.gameObject;
                    //print(GameData.TargetCombo.GetComponent<Keo>().NameCollection);
                    checkKeo(hit.collider.gameObject, hit.collider.gameObject.GetComponent<Keo>().Row, hit.collider.gameObject.GetComponent<Keo>().Column, hit.collider.gameObject.GetComponent<Keo>().type);
                   
                    WhenCheckSuccess();
                }
            }
            
        }
	
	}

    IEnumerator setOldTime()
    {
        yield return new WaitForSeconds(1.2f);
        oldTime = System.DateTime.Now.Second;
    }

    /// <summary>
    /// update time play 
    /// </summary>    
    IEnumerator UpDateTimePlay()
    {
        txtTimer.text = timeplay.ToString();
        yield return new WaitForSeconds(1.5f);
        if (timeplay > 0)
        {
            newTime = System.DateTime.Now.Second;
            if (newTime != oldTime)
            {
                oldTime = newTime;
                timeplay--;                
            }
        }
        else
        {
            isGameOver = true;
        }
    }

    // restart - reload all list keo
    public void restart()
    {
        foreach (GameObject x in Keos)
        {
            Destroy(x.gameObject);
        }
        Start();
    }

    /// <summary>
    /// action when check successed
    /// </summary>
    private void WhenCheckSuccess()
    {
        if (listKeos.Count >= 3)
        {
            click = false;
            UpDateCollection();
            Score = listKeos.Count * GameData.BaseScore;
            if (listKeos.Count < 6)
            {
                time_Eat_Keo = 0.15f;
                foreach (GameObject keo in listKeos)
                {
                    keo.GetComponent<Keo>().big = true;
                    Keos[keo.GetComponent<Keo>().Row, keo.GetComponent<Keo>().Column] = null;
                    Destroy(keo.gameObject, time_Eat_Keo);                    
                }
            }
            else
            {
                time_Eat_Keo = 0.3f;
                Score = listKeos.Count * GameData.BaseScore + listKeos.Count % 6 * GameData.BaseScore * 2;
                foreach (GameObject keo in listKeos)
                {
                    keo.GetComponent<Keo>().isCombo = true;
                    keo.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
                    Keos[keo.GetComponent<Keo>().Row, keo.GetComponent<Keo>().Column] = null;
                    Destroy(keo.gameObject, time_Eat_Keo);                    
                }
                GameData.TargetCombo.GetComponent<Keo>().big = true;
                GameData.TargetCombo.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            }
            StartCoroutine(ReturnClick(time_Eat_Keo));
            StartCoroutine(UpdateKeo(time_Eat_Keo));
        }
        else
        {
            //countCheckFail++;
            GameData.TargetCombo.GetComponent<Keo>().scale = true;
            foreach (GameObject keo in listKeos)
            {
                keo.GetComponent<Keo>().addtoList = false;
            }
        }
    }
    //test delete keo
    /// <summary>
    /// update keo affter eated
    /// </summary>
    /// <param name="Keos"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    IEnumerator UpdateKeo(float time)
    {
        yield return new WaitForSeconds(time);
        if (GameData.TypePlay == 1)
        {
            for (int x = GameData.MaxRow - 1; x > 0; x--)
            {
                for (int y = Column - 1; y >= 0; y--)
                {                   
                    if (Keos[x, y] != null && Keos[x - 1, y] == null)
                    {
                        updateKeo_Column(Keos, x - 1, y);                       
                    }
                }
            }
            newRowKeo();
            
        }
        if (GameData.TypePlay == 2)
        {
            for (int x = GameData.MaxRow - 1; x >= 0; x--)
            {
                for (int y = Column - 1; y >= 0; y--)
                {
                    if (Keos[x, y] == null)
                    {
                        updateKeo_Column(Keos, x, y);
                    }
                }
            }
        }
        CheckAfterUpdateList();
      
    }
    private void updateKeo_Column(GameObject[,] Keos, int x, int y)
    {
        if (GameData.TypePlay == 1)
        {
            while (x < GameData.MaxRow - 1 && Keos[x + 1, y] != null)
            {

                Keos[x, y] = Keos[x + 1, y];
                Keos[x, y].GetComponent<Keo>().Row = x;
                Keos[x, y].GetComponent<Keo>().Column = y;
                Keos[x, y].name = "dau" + x + "_" + y;
                x++;
            }
            Keos[x, y] = null;     
        }
        if (GameData.TypePlay == 2)
        {
            while (x < GameData.MaxRow -1&& Keos[x+1 , y] != null)
            {

                Keos[x, y] = Keos[x + 1, y];
                Keos[x, y].GetComponent<Keo>().Row = x;
                Keos[x, y].GetComponent<Keo>().Column = y;
                Keos[x, y].name = "dau" + x + "_" + y;
                x++;
            }
            Keos[x, y] = newKeo(Keos[x,y],x,y);
        }
            
    }
    private void newRowKeo()
    {
        for (int x = GameData.MaxRow - 1; x >0; x--)
        {
            for (int y = Column - 1; y >= 0; y--)
            {
                if (Keos[x - 1, y] != null)
                {
                    Keos[x, y] = Keos[x - 1, y];
                    Keos[x, y].GetComponent<Keo>().Column = y;
                    Keos[x, y].GetComponent<Keo>().Row = x;
                    Keos[x, y].name = "dau" + x + "_" + y;
                }
            }
        }
        for (int y = 0; y < Column; y++)
        {
            Keos[0, y] = newKeo(Keos[0, y], 0, y);
        }
           
    }
    private GameObject newKeo(GameObject keo, int Row, int Column)
    {
        keo = (GameObject)Instantiate(GameData.getBean(), Vector3.zero, Quaternion.identity);
        keo.AddComponent<Keo>();
        keo.GetComponent<Keo>().Row = Row;
        keo.GetComponent<Keo>().Column = Column;
        //keo.AddComponent<SpriteRenderer>();
       // keo.GetComponent<SpriteRenderer>().sprite = GameData.getBean();// (Sprite)Resources.Load(GameData.getBean(), typeof(Sprite));
        keo.GetComponent<Keo>().type = keo.GetComponentInChildren<SpriteRenderer>().sprite.name;
        keo.name = "dau " + Row + "_" + Column;
        keo.GetComponent<Keo>().NameCollection = GameData.getKeoCollection(GameData.Name);
        keo.tag = "keo";
        keo.transform.localScale = GameData.KeoScale;
        //keo.AddComponent<BoxCollider2D>();
        keo.transform.position = new Vector3(startPointX + Column * keo.GetComponent<Collider2D>().bounds.size.x + Column * Spacing, startPointY + Row * keo.GetComponent<Collider2D>().bounds.size.y + Row*Row * Spacing , 0);
        return keo;
    }
    ///<summary>
    ///check and add to listkeo
    ///</summary>
    void checkKeo(GameObject keo, int x, int y, string type)
    { 
        if( keo!=null &&keo.GetComponent<Keo>().type == type && !keo.GetComponent<Keo>().addtoList)
        {
            listKeos.Add(keo);
            keo.GetComponent<Keo>().addtoList = true;
            if (x + 1 < GameData.MaxRow)
            {
                checkKeo(Keos[x + 1, y], x + 1, y, type);
            }
            if (x - 1 >= 0)
            {
                checkKeo(Keos[x - 1, y], x - 1, y, type);
            }
            if (y + 1 < Column)
            {
                checkKeo(Keos[x, y + 1], x, y + 1, type);
            }
            if (y - 1 >= 0)
            {
                checkKeo(Keos[x, y - 1], x, y - 1, type);
            }
            if (x - 1 >= 0 && y + 1 < Column)
            {
                checkKeo(Keos[x - 1, y + 1], x - 1, y + 1, type);
            }
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                checkKeo(Keos[x - 1, y - 1], x - 1, y - 1, type);
            }
            if (x + 1 < GameData.MaxRow && y + 1 < Column)
            {
                checkKeo(Keos[x + 1, y + 1], x + 1, y + 1, type);
            }
            if (x + 1 < GameData.MaxRow && y - 1 >= 0)
            {
                checkKeo(Keos[x + 1, y - 1], x + 1, y - 1, type);
            }           
        }
        else
        {
            return;
        }
    }

    //=========================================
    void CheckNull(GameObject keo, int x, int y, string type)
    {
        if (keo != null && keo.GetComponent<Keo>().type == type && !keo.GetComponent<Keo>().isCheck)
        {
            checkList.Add(keo);
            keo.GetComponent<Keo>().isCheck = true;
            if (x + 1 < GameData.MaxRow)
            {
                CheckNull(Keos[x + 1, y], x + 1, y, type);
            }
            if (x - 1 >= 0)
            {
                CheckNull(Keos[x - 1, y], x - 1, y, type);
            }
            if (y + 1 < Column)
            {
                CheckNull(Keos[x, y + 1], x, y + 1, type);
            }
            if (y - 1 >= 0)
            {
                CheckNull(Keos[x, y - 1], x, y - 1, type);
            }
            if (x - 1 >= 0 && y + 1 < Column)
            {
                CheckNull(Keos[x - 1, y + 1], x - 1, y + 1, type);
            }
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                CheckNull(Keos[x - 1, y - 1], x - 1, y - 1, type);
            }
            if (x + 1 < GameData.MaxRow && y + 1 < Column)
            {
                CheckNull(Keos[x + 1, y + 1], x + 1, y + 1, type);
            }
            if (x + 1 < GameData.MaxRow && y - 1 >= 0)
            {
                CheckNull(Keos[x + 1, y - 1], x + 1, y - 1, type);
            }           
        }
        else
        {
            return;
        }
    }

    void CheckAfterUpdateList() 
    {
        //yield return new WaitForSeconds(0.1f);
        for (int x = Row - 1; x >= 0 ; x--)
        {
            for (int y = Column - 1; y >= 0; y--)
            {
                checkList = new List<GameObject>();
                if (Keos[x, y] != null)
                {
                    CheckNull(Keos[x, y], x, y, Keos[x, y].GetComponent<Keo>().type);
                }               
                if (checkList.Count >= 3)
                {                    
                    removeCheckNull();
                    break;
                }
                else
                {
                    if (x == 0 && y == 0)
                    {
                        removeCheckNull();
                        StartCoroutine(resetList());
                    }
                }
            }
        }
    }

    IEnumerator resetList()
    {    
        foreach (GameObject x in Keos)
        {
            if (x != null)
            {
                x.GetComponent<Keo>().reset = true;
                Destroy(x.gameObject, 0.28f);
            }            
        }
        SoundController.Intance.Play(SoundController.CLIP.CLEAR);
        yield return new WaitForSeconds(0.3f);

        listKeos = new List<GameObject>();

        Keos = new GameObject[GameData.MaxRow, Column];
        for (int x = 0; x < Row; x++)
        {
            for (int y = 0; y < Column; y++)
            {
                Keos[x, y] = (GameObject)Instantiate(GameData.getBean(), Vector3.zero, Quaternion.identity);
                Keos[x, y].AddComponent<Keo>();
                Keos[x, y].GetComponent<Keo>().Row = x;
                Keos[x, y].GetComponent<Keo>().Column = y;
                // Keos[x, y].AddComponent<SpriteRenderer>();

                // Keos[x, y].GetComponent<SpriteRenderer>().sprite = GameData.getBean();// (Sprite)Resources.Load(GameData.getBean(), typeof(Sprite));
                Keos[x, y].GetComponent<Keo>().type = Keos[x, y].GetComponentInChildren<SpriteRenderer>().sprite.name;
                Keos[x, y].name = "dau " + x + "_" + y;
                Keos[x, y].GetComponent<Keo>().NameCollection = GameData.getKeoCollection(GameData.Name);
                Keos[x, y].transform.localScale = GameData.KeoScale;
                Keos[x, y].tag = "keo";
                // Keos[x, y].AddComponent<BoxCollider2D>();
               
                 Keos[x, y].transform.position = new Vector3(startPointX + y * Keos[x, y].GetComponent<Collider2D>().bounds.size.x , startPointY + x * Keos[x, y].GetComponent<Collider2D>().bounds.size.y , 0);               

            }
        }
    }

    private void removeCheckNull()
    {
        for (int x = Row - 1; x >= 0; x--)
        {
            for (int y = Column - 1; y >= 0; y--)
            {
                if (Keos[x, y] != null && Keos[x, y].GetComponent<Keo>() != null)
                {
                    Keos[x, y].GetComponent<Keo>().isCheck = false;
                }
                
            }
        }
    }
    


    IEnumerator ReturnClick( float time)
    {
        yield return new WaitForSeconds(time);
        click = true;
    }

    void UpDateCollection()
    {
        if (GameData.MissionType == GameData.MISSIONTYPE.COLLECTION)
        {
            if (GameData.KeoCollection.Length == 1)
            {
                if (GameData.TargetCombo.GetComponent<Keo>().NameCollection == GameData.KeoCollection[0].KeoCollection)
                {
                    GameData.NumKeo1 += listKeos.Count;
                }
            }
            else
            {
                if (GameData.TargetCombo.GetComponent<Keo>().NameCollection == GameData.KeoCollection[0].KeoCollection)
                {
                    GameData.NumKeo1 += listKeos.Count;
                }
                if (GameData.TargetCombo.GetComponent<Keo>().NameCollection == GameData.KeoCollection[1].KeoCollection)
                {
                    GameData.NumKeo2 += listKeos.Count;
                }
            }

        }
        if (GameData.MissionType == GameData.MISSIONTYPE.COLLECTION)
        {
            if (GameData.KeoCollection.Length == 1)
            {
                Keo1.GetComponentInChildren<Text>().text = GameData.NumKeo1.ToString() + "/" + GameData.KeoCollection[0].Count.ToString();

            }
            else
            {
                Keo1.GetComponentInChildren<Text>().text = GameData.NumKeo1.ToString() + "/" + GameData.KeoCollection[0].Count.ToString();
                Keo2.GetComponentInChildren<Text>().text = GameData.NumKeo2.ToString() + "/" + GameData.KeoCollection[1].Count.ToString();
            }
        }        
    }

    //IEnumerator delayCheckSuccess()
    //{
    //    yield return new WaitForEndOfFrame();

    //}  
   
   
    /// <summary>
    /// return color bean by random
    /// </summary>
    //private string getBean()
    //{
    //    switch (Random.Range(0, 4))
    //    { 
    //        case 0:
    //            return  "Sprites/bean_red";
    //        case 1:
    //            return "Sprites/bean_green";
    //        case 2:
    //            return "Sprites/bean_pink";
    //        case 3:
    //            return "Sprites/bean_yellow";
    //        case 4:
    //            return "Sprites/bean_orange";
    //        case 5:
    //            return "Sprites/bean_blue";
    //        case 6:
    //            return "Sprites/bean_white";
    //        case 7:
    //            return "Sprites/bean_purple"; 
    //        default:            
    //            return "Sprites/bean_blue";
    //    }
    //}
}

using UnityEngine;


public class GameData  {

    public static bool TestGame = false;

    public static bool FirstStart = true;
    
    public static int Gold { get; set; }

    public static bool NextLevel { get; set; }

    public static bool Mute = false;

    public static int TimePlay { get; set; }

    public static int TimePlus { get; set; }

    public static bool X2 { get; set; }

    public static int Level { set; get; }

    public static MISSIONTYPE MissionType { get; set; }

    public static int TargetScore { get; set; }

    public static Collection[] KeoCollection{ get; set; }

    public static int NumKeo1 { set; get; }

    public static int NumKeo2 { get; set; }   

    public static Sprite Keo1 { get; set; }

    public static Sprite Keo2 { get; set; }

    public static int TypePlay { get; set; }
    public static int MaxRow { get; set; }

    public static GameObject IMGKEO { get; set; }

    private static GameObject[] listIMGKEO;

    public static GameObject TargetCombo { get; set; }

    public static int Score { get; set; }

    public static int BaseScore;

    public static string Name{get;set;}

    public static Vector3 vecReset;
    public static Vector3 KeoScale = new Vector3( 0.38f, 0.38f, 0.38f );

    public static Texture2D ScreenShot { get; set; }

    //=========================== enum=============

    public enum MISSIONTYPE 
    {
        COLLECTION,
        SCORE,
    }

    public enum KEOCOLLECTION 
    {
        DAU,
        DUA,
        OT,
        TAO,
        HANH,
        NGO,
    }


    //public static int MaxColum { get; set; }

    //------------------------------------------------------------ Public Functions-------------------------------------------
    
    /// <summary>
    /// load all data game when starting
    /// </summary>
    public static void AllLoadData()
    {
        getlistIMGKEO();        
    }
    /// <summary>
    /// return list images of all keo 
    /// </summary>
    private static void  getlistIMGKEO ()
    {
        listIMGKEO = Resources.LoadAll<GameObject>("Prefabs/KEO");
    }
    /// <summary>
    /// return image for new keo
    /// </summary>
    /// <returns></returns>
    public static GameObject getBean()
    {
        IMGKEO = listIMGKEO[Random.Range(0, listIMGKEO.Length)];
        Name = IMGKEO.name;
        return IMGKEO;       
    }

    public static KEOCOLLECTION getKeoCollection(string name)
    {
        switch (name)
        {
            case "Hanh":
                return KEOCOLLECTION.HANH;
            case "Ngo":
                return KEOCOLLECTION.NGO;
            case "Tao":
                return KEOCOLLECTION.TAO;
            case "Ot":
                return KEOCOLLECTION.OT;
            case "Dau":
                return KEOCOLLECTION.DAU;
            case "Dua":
                return KEOCOLLECTION.DUA;
            default:
                return KEOCOLLECTION.DUA;
        }
    }
    /// <summary>
    ///  return effect when destroy keo
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject loadEF(string type)
    {
        switch (type)
        {
            case "Tao":
                return Resources.Load<GameObject>("EF/explosionRed");
                
            case "Ot":
                return Resources.Load<GameObject>("EF/explosionGreen");
                
            case "Dau":
                return Resources.Load<GameObject>("EF/explosionPink");
                
            case "Ngo":
                return Resources.Load<GameObject>("EF/explosionYellow");

            case "Hanh":
                return Resources.Load<GameObject>("EF/explosionYellow");

            case "Dua":
                return Resources.Load<GameObject>("EF/explosionGreen");

            default:
                return Resources.Load<GameObject>("EF/explosionRed");
        }
    }
    
    [System.Serializable]
    public class Collection 
    {
        public KEOCOLLECTION KeoCollection;
        public int Count;
        public Collection(KEOCOLLECTION keocollection)
        {
            KeoCollection = keocollection;
            Count = 0;
        }
    }
	
}

using UnityEngine;

public class PlayerPrefsControll  {
    public delegate ISerializationCallbackReceiver Callback();
    private static string LEVEL = "LEVEL";

    private static string HIGH = "HIGH";

    private static string GOLD = "Gold";

    private static string BEST = "BEST";
    // Use this for initialization
    public static void TheFirst()
    {
        if (!PlayerPrefs.HasKey("THE_FIRST"))
        {
            PlayerPrefs.SetInt("THE_FIRST", 1);
            GameData.Gold = 5;
            SaveGold(GameData.Gold);
        }
        else
        {
            GameData.Gold = GetGold();
        }
    }

    public static void SaveGold(int gold)
    {
        PlayerPrefs.SetInt(GOLD, gold);
        PlayerPrefs.Save();
    }

    public static int GetGold()
    {
        return PlayerPrefs.GetInt(GOLD);
    }


    public static void SaveLevel( int level)
    {    
        
        PlayerPrefs.SetInt(LEVEL+ level.ToString(), level);
        PlayerPrefs.Save();
    }

    public static bool LevelSuccess(int level)
    {
        if(PlayerPrefs.HasKey(LEVEL+level.ToString()) || level == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SaveHigh(int level, int score)
    {
        if (!PlayerPrefs.HasKey(HIGH + level.ToString()) || PlayerPrefs.GetInt(HIGH + level.ToString()) < score)
        {           
            PlayerPrefs.SetInt(HIGH + level.ToString(), score);         
        }        
        PlayerPrefs.Save();
    }

    public static int GetHigh(int level)
    {
        return PlayerPrefs.GetInt(HIGH + level.ToString());
    }

    public static void SetBest(int best)
    {
        if (!PlayerPrefs.HasKey(BEST) || best > PlayerPrefs.GetInt(BEST))
        {
            PlayerPrefs.SetInt(BEST, best);
            PlayerPrefs.Save();
        }
    }

    public static int GetBest()
    {
        if (PlayerPrefs.HasKey(BEST))
        {
            return PlayerPrefs.GetInt(BEST);
        }
        else
        {
            return 0;
        }
    }
}

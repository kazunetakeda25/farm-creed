using UnityEngine;
//using System.Collections;
//using UnityEngine.SocialPlatforms;


public class MyBoard : MonoBehaviour {
    private string leaderboard = "xxxxxxxxxxxxxx";//inserst name of leaderboard
    //private static MyBoard intance;

    // Use this for initialization
    void Start()
    {

    }

    //public static MyBoard GetIntance()
    //{
    //    if (intance == null)
    //    {
    //        intance = new MyBoard();
    //    }
    //    return intance;
    //}

    // Update is called once per frame
    void Update()
    {

    }
    public void UpLeaderBoad()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("You've successfully logged in");
            }
            else {
                Debug.Log("Login failed for some reason");
            }
        });

        if (Social.localUser.authenticated)
        {
            Social.ReportScore(PlayerPrefsControll.GetBest(), leaderboard, (bool success) =>
            {
                if (success)
                {
             
                }
                else {
                    //Debug.Log("Login failed for some reason");
                }
            });
        }
    }

}

using UnityEngine;
using UnityEngine.UI;

public class MissionViewDown : MonoBehaviour {
    public Scrollbar scrollbar;
    public Image  down;
	// Use this for initialization
	void Start () {       
	}
	
	// Update is called once per frame
	void Update () {
        if (scrollbar.value == 0)
        {
            down.color = Color.gray;
        }
        else
        {
            down.color = Color.white;
        }

        //if (scrollbar.value == 1)
        //{
        //    up.color = Color.gray;
        //}
        //else
        //{
        //    up.color = Color.white;
        //}
    }
}

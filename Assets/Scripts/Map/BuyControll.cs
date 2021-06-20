using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyControll : MonoBehaviour {
    public bool isX2;
    public int time;
    public Image buy;
    public Sprite buyOn, buyOff;
    public int price;
    public Text txtPrice;
    public Text info;
    private Button myBut;
	// Use this for initialization
    void Awake()
    {
        info.text = "";
        txtPrice.text = price.ToString();
        myBut = GetComponent<Button>();
        if ((isX2 && GameData.X2) || (!isX2 && GameData.TimePlus>0))
        {
            myBut.enabled = false;
            buy.sprite = buyOff;
           // print(false);
        }
        else
        {
            //print(true);
            myBut.enabled = true;
            buy.sprite = buyOn;
            myBut.onClick.AddListener(OnClick);
        }
    }	

    void Update()
    {
        if (!isX2)
        {
            if (GameData.TimePlus > 0)
            {
                myBut.enabled = false;
                buy.sprite = buyOff;
            }
        }
    }

    void OnClick()
    {
        SoundController.Intance.Play(SoundController.CLIP.CLICK);
        if (GameData.Gold >= price)
        {
            myBut.enabled = false;
            GameData.Gold -= price;
            PlayerPrefsControll.SaveGold(GameData.Gold);
            buy.sprite = buyOff;
            if (isX2)
            {
                GameData.X2 = true;
            }
            else
            {
                GameData.TimePlus = time;
            }
        }
        else
        {
            info.text = "You don't enough coin";
            StartCoroutine(clearInfo());
        }
    }

    IEnumerator clearInfo()
    {
        yield return new WaitForSeconds(2);
        info.text = "";
    }
   
}

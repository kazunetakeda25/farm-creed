using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour {
    private Image image;
    public Button resume, menu;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();       
        image.fillAmount = 0;
        resume.onClick.AddListener(Resume);
        menu.onClick.AddListener(Menu);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameControll.isPause)
        {
            image.fillAmount = 1;
        }
        else
        {
            image.fillAmount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GameControll.isPause)
        {
            Resume();
        }
	}
    void Resume()
    {
        SoundController.Intance.Play(SoundController.CLIP.EAT);
        GameControll.isPause = false;
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
       // image.fillAmount = 0;
    }
    void Menu()
    {
        SoundController.Intance.Play(SoundController.CLIP.EAT);
        Application.LoadLevel("Map");
    }
}

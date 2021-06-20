using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour {
    public Button playButton;
    public GameObject panelGray;
    // Use this for initialization
    void Start () {
        playButton.onClick.AddListener(Play);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Play()
    {
        SoundController.Intance.Play(SoundController.CLIP.CLICK);
        panelGray.SetActive(true);
        StartCoroutine(play());
    }

    IEnumerator play()
    {
        yield return new WaitForSeconds(1.4f);
        SoundController.Intance.StopAll();
        Application.LoadLevelAsync("MainGame");
    }
}

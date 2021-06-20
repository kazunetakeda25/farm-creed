using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScreenShot : MonoBehaviour
{
    public GameObject panelScreenshot;
    public Image img;
    public Button facebookShare, cancel;
    private Rect rect;
    int width = Screen.width;
    int height = Screen.height;

    // Use this for initialization
    void Start()
    {       
        rect = new Rect(0, 0, width, height);
        GetComponent<Button>().onClick.AddListener(OnClick);
        facebookShare.onClick.AddListener(ShareScreenShotToFacebook);
        cancel.onClick.AddListener(Cancel);         
    }

    IEnumerator showButton()
    {
        yield return new WaitForSeconds(1f);
        facebookShare.gameObject.SetActive(true);
        cancel.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        SoundController.Intance.Play(SoundController.CLIP.CLICK);
        StartCoroutine(TakeScreenshot());
    }

    void ShareScreenShotToFacebook()
    {
        SoundController.Intance.Play(SoundController.CLIP.CLICK);

        callcancel();
    }

    void Cancel()
    {
        SoundController.Intance.Play(SoundController.CLIP.CLICK);
        callcancel();
    }

    void callcancel()
    {
        facebookShare.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
        panelScreenshot.SetActive(false);
    }
    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();       
        GameData.ScreenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        // Read screen contents into the texture
        GameData.ScreenShot.ReadPixels(rect, 0, 0);
        GameData.ScreenShot.Apply();
        panelScreenshot.SetActive(true);
        StartCoroutine(showButton());
        img.sprite = Sprite.Create(GameData.ScreenShot, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
    }
}

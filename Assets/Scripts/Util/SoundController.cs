using UnityEngine;

public class SoundController : MonoBehaviour
{//: MonoBehaviour
    public static SoundController Intance;

    // all clip 
    public AudioClip MainMenuMorning;
    public AudioClip MainMenuNight;
    public AudioClip BackGroundMorning;
    public AudioClip BackGroundNight;
    public AudioClip Click;
    public AudioClip Eat;
    public AudioClip Combo;
    public AudioClip Clear;
    public AudioClip Seven;
    public AudioClip Win;
    public AudioClip Fail;    

    // var AudioSource
    private AudioSource AudioSrMenuMoning;
    private AudioSource AudioSrMenuNight;
    private AudioSource AudioSrBGMoning;
    private AudioSource AudioSrBGNight;
    private AudioSource AudioSrClick;
    private AudioSource AudioSrEat;
    private AudioSource AudioSrCombo;
    private AudioSource AudioSrClear;
    private AudioSource AudioSrSeven;
    private AudioSource AudioSrWin;
    private AudioSource AudioSrFail;   
    private AudioSource[] ListAudio;
	// Use this for initialization
    void Awake()
    {
        if (Intance == null)
        {
            Intance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            DestroyImmediate(gameObject);
        }
    }
    void Start()
    {
        Init();
        ListAudio = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
    //void Update () {
	
    //}
    public enum CLIP
    {
        MAINMENU_MORNING,
        MAINMENU_NIGHT,
        BACKGROUND_MORNING,
        BACKGROUND_NIGHT,
        CLICK,
        EAT,
        COMBO,
        CLEAR,
        SEVENT,
        WIN,
        FAIL,
    };
   
    void Init()
    {
        AudioSrMenuMoning = getAudioSource( MainMenuMorning);
        AudioSrMenuNight = getAudioSource(MainMenuNight);
        AudioSrBGMoning = getAudioSource( BackGroundMorning);
        AudioSrBGNight = getAudioSource( BackGroundNight);
        AudioSrEat = getAudioSource( Eat);
        AudioSrClick = getAudioSource( Click);
        AudioSrCombo = getAudioSource( Combo);
        AudioSrClear = getAudioSource( Clear);
        AudioSrSeven = getAudioSource( Seven);
        AudioSrWin = getAudioSource( Win);
        AudioSrFail = getAudioSource( Fail);
    }
    /// <summary>
    /// add component AudioSource 
    /// </summary>
    /// <param name="aus"></param>
    /// <param name="clip"></param>
    AudioSource getAudioSource( AudioClip clip)
    {
        AudioSource aus = gameObject.AddComponent<AudioSource>();
        aus.clip = clip;
        aus.playOnAwake = false;
        return aus;
    }

    /// <summary>
    /// play audio with clip name
    /// </summary>
    /// <param name="clip"></param>
    public void Play(CLIP clip)
    {
        GetClip(clip).Play();
    }

    /// <summary>
    /// play and loop audio with clip name
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="loop"></param>
    public void Play(CLIP clip, bool loop)
    {
        AudioSource au = GetClip(clip);
        au.Play();
        au.loop = loop;
    }

    public void Test()
    {
        AudioSrBGMoning.Play();
    }
    public void StopClip(CLIP clip)
    {
        GetClip(clip).Stop();
    }
    /// <summary>
    /// set mute for all audio
    /// </summary>
    /// <param name="mute"></param>
    public void StopAll(bool mute)
    {        
        foreach (AudioSource au in ListAudio)
        {
            au.mute = mute;
        }
    }
    /// <summary>
    /// stop all audio
    /// </summary>
    public void StopAll()
    {
        foreach (AudioSource au in ListAudio)
        {
            au.Stop();
        }
    }
    /// <summary>
    /// return audiosource same clip name
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    private AudioSource GetClip(CLIP clip)
    {
        switch (clip)
        {
            case CLIP.MAINMENU_MORNING:
                return AudioSrMenuMoning;

            case CLIP.MAINMENU_NIGHT:
                return AudioSrMenuNight;

            case CLIP.BACKGROUND_MORNING:
                return AudioSrBGMoning;

            case CLIP.BACKGROUND_NIGHT:
                return AudioSrBGNight;

            case CLIP.CLICK:
                return AudioSrClick;

            case CLIP.EAT:
                return AudioSrEat;

            case CLIP.COMBO:
                return AudioSrCombo;

            case CLIP.CLEAR:
                return AudioSrClear;

            case CLIP.SEVENT:
                return AudioSrSeven;

            case CLIP.WIN:
                return AudioSrWin;

            case CLIP.FAIL:
                return AudioSrFail;

            default:
                return AudioSrClick;
        }
    }
}

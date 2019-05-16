using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour {

    private static AudioManager instance;
    public static AudioManager _instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
                if (!instance)
                {
                    var obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }
    public AudioSource bgmPlayer;
    public AudioSource effectPlayer;
    Dictionary<string, string> bgmDic;
    public float initBgmVolume = 0.1f;
    public float initEffectVolume = 0.5f;
    public float saveVolume;
    public  bool reduceVol = false;
    public  bool increaseVol = false;

    string sceneName;

    private void Awake()
    {
        instance = this;
        InitPlayer();
        SetBgmDic();
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        string bgmName;
        if (bgmDic.TryGetValue(sceneName, out bgmName))
        {
            PlayeBGM(bgmName);
        }
    }

    void InitPlayer()
    {
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        effectPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        //只执行一次
        if (PlayerPrefs.HasKey("isMute"))
        {
            //读档
            effectPlayer.mute = bgmPlayer.mute = PlayerPrefs.GetInt("isMute") == 0 ? false : true;
            bgmPlayer.volume = PlayerPrefs.GetFloat("bgmVolume");
            bgmPlayer.volume = PlayerPrefs.GetFloat("effectVolume");
        }
        else
        {   //初始值  
            bgmPlayer.mute = false;
            effectPlayer.mute = false;
            bgmPlayer.volume = initBgmVolume;
            effectPlayer.volume = initEffectVolume;
        }

    }
    void SetBgmDic()
    {
        bgmDic = new Dictionary<string, string>();
        bgmDic.Add("hair", "coldNight");
    }

    public void SetPlayerVolume(float num)
    {
        AudioManager._instance.bgmPlayer.volume = AudioManager._instance.effectPlayer.volume = num;//根据档位设置音量
    }
    //什么时候执行？需要换背景音乐的时候
    public  void PlayeBGM(string name)
    {
        increaseVol = true;
        string lastName;
        //上个场景在播放的clip以实际为准
        if (bgmPlayer.clip == null)
        {
            lastName = "";
        }
        else
        {
            lastName = bgmPlayer.clip.name;
        }
        //把last的空和不为空，进行了统一
        if (lastName!=name)
        {
            //更换
            bgmPlayer.clip = Resources.Load<AudioClip>(MainContainer.bgmFolder+ name);
            lastName = name;
            bgmPlayer.Play();
        }     
    }
    public  void PlayEffect(string name)
    {
        if(effectPlayer != null)
        {
            effectPlayer.clip = Resources.Load<AudioClip>(MainContainer.effectFolder + name);
            effectPlayer.PlayOneShot(effectPlayer.clip);
        }              
    }
}

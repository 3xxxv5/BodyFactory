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
    public float initBgmVolume = 0.2f;
    public float initEffectVolume = 0.4f;
    public float saveVolume;
    public  bool reduceVol = false;
    public  bool increaseVol = false;

    string sceneName;

    private void Awake()
    {
        instance = this;
        InitPlayer();
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
    }

    void InitPlayer()
    {
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        effectPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = initBgmVolume;
        effectPlayer.volume = initEffectVolume;

    }

    public void SetPlayerVolume(float num)
    {
        bgmPlayer.volume = effectPlayer.volume = num;//根据档位设置音量
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] seClips;
    public AudioSource[] bgmClips;

    public static AudioManager instance;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySE(string playSE)
    {
        int playIndex = AudioDictionary.SePairs[playSE];
        if(playIndex < seClips.Length) { seClips[playIndex].Play(); }
    }

    public void PlayBGM(string playBGM)
    {
        int playIndex = AudioDictionary.BgmPairs[playBGM];
        if (!bgmClips[playIndex].isPlaying)
        {
            StopMusic();
            if(playIndex < bgmClips.Length)
            {
                bgmClips[playIndex].Play();
            }
        }
    }

    public IEnumerator PlayBGM(string playBGM, float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        int playIndex = AudioDictionary.BgmPairs[playBGM];
        if (!bgmClips[playIndex].isPlaying)
        {
            StopMusic();
            if (playIndex < bgmClips.Length)
            {
                bgmClips[playIndex].Play();
            }
        }
    }

    public void StopMusic()
    {
        for(int i = 0;i < bgmClips.Length; i++)
        {
            bgmClips[i].Stop();
        }
    }
}

public static class AudioDictionary
{
    public static Dictionary<string, int> SePairs = new Dictionary<string, int> {
        { "Tap", 0 },
        { "Flick", 1 }
    };
    public static Dictionary<string, int> BgmPairs= new Dictionary<string, int>{
        { "Barduckman", 0 }
    };
}

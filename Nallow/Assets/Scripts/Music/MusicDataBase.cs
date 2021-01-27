using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDataBase", menuName = "CreateMusicDataBase")]
public class MusicDataBase : ScriptableObject
{
    [SerializeField]
    private List<Music> musicList = new List<Music>();

    public List<Music> GetMusicList()
    {
        return musicList;
    }
}

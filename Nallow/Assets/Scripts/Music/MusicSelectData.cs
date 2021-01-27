using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MusicSelectData", menuName = "ScriptableObjects/MusicSelectData")]
public class MusicSelectData : ScriptableObject
{
    [SerializeField]
    private string sceneName;

    private ScoreManager scoreManager;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName ="Music", menuName ="CreateMusic")]
public class Music : ScriptableObject
{
#pragma warning disable 0649
    [SerializeField]
    private int id;
    [SerializeField]
    private string title;
    [SerializeField]
    private string artist;
    [SerializeField]
    private Sprite jacket;
    [SerializeField]
    private int easyDifficulty = 0;
    [SerializeField]
    private int hardDifficulty = 0;
    [SerializeField]
    private int expertDifficulty = 0;

    public int GetId()
    {
        return this.id;
    }
    public string GetTitle()
    {
        return this.title;
    }
    public string GetArtist()
    {
        return this.artist;
    }
    public Sprite GetSprite()
    {
        return this.jacket;
    }
    public int GetDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                return this.easyDifficulty;
            case "Hard":
                return this.hardDifficulty;
            case "Expert":
                return this.expertDifficulty;
            default:
                return 0;
        }
    }
}

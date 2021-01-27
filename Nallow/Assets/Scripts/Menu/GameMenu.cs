﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Player;

public class GameMenu : MonoBehaviour
{
#pragma warning disable 0649
#pragma warning disable 0414

    public static GameMenu instance;

    public static MenuContent menuContent;

    [SerializeField]
    GameObject modePanels;
    [SerializeField]
    GameObject musicPanels;
    [SerializeField]
    GameObject difficultyPanels;
    [SerializeField]
    Image musicJacket;
    [SerializeField]
    Text musicText;

    [SerializeField]
    private MusicDataBase musicDataBase;

    private GameObject topPanel, centerPanel, bottomPanel;

    private Text topText, centerText, bottomText;
    private Text titleText, difficultyText;

    private int topIndex, centerIndex, bottomIndex;

    private MenuState menuState;

    private Music selectedMusic;
    private string selectedDifficulty;

    private int musicCenterIndexBuffer, musicTopIndexBuffer;

    private float startSec = 0;

    [SerializeField] private AudioManager audioManager;

    void Awake()
    {
        instance = this;
        menuContent = new MenuContent();

        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }

        musicCenterIndexBuffer = 0;
        musicTopIndexBuffer = musicDataBase.GetMusicList().Count - 1;

        menuState = MenuState.Mode;
        topIndex = MenuContent.ModeIds.Count - 1;
        centerIndex = 0;
        bottomIndex = 1;
        findPanel(modePanels);
        findText();
    }

    void Update()
    {
        startSec = Time.time;
        showText();
    }

    public void showText()
    {
        if(menuState == MenuState.Mode)
        {
            topText.text = MenuContent.ModeIds[topIndex];
            centerText.text = MenuContent.ModeIds[centerIndex];
            bottomText.text = MenuContent.ModeIds[bottomIndex];
        }
        else if(menuState == MenuState.Music)
        {
            var centerMusicData = musicDataBase.GetMusicList()[centerIndex];
            titleText.text = centerMusicData.GetTitle();
            difficultyText.text = "Easy   " + centerMusicData.GetDifficulty("Easy") + "\n\n"
                + "Hard   " + centerMusicData.GetDifficulty("Hard") + "\n\n"
                + "Expert " + centerMusicData.GetDifficulty("Expert");
            musicJacket.sprite = centerMusicData.GetSprite();
            bottomText.text = musicDataBase.GetMusicList()[bottomIndex].GetTitle();
            topText.text = musicDataBase.GetMusicList()[topIndex].GetTitle();
        }
        else if(menuState == MenuState.Difficulty)
        {
            var topDifficulty = MenuContent.DifficultyIds[topIndex];
            var centerDifficulty = MenuContent.DifficultyIds[centerIndex];
            var bottomDifficulty = MenuContent.DifficultyIds[bottomIndex];
            topText.text = MenuContent.DifficultyIds[topIndex] + " " + selectedMusic.GetDifficulty(topDifficulty);
            centerText.text = MenuContent.DifficultyIds[centerIndex] + " " + selectedMusic.GetDifficulty(centerDifficulty);
            bottomText.text = MenuContent.DifficultyIds[bottomIndex] + " " + selectedMusic.GetDifficulty(bottomDifficulty);
            musicText.text = selectedMusic.GetTitle();
        }
    }

    public void Select()
    {
        //Difficultyの時はシーン遷移と曲保持
        switch (menuState)
        {
            case MenuState.Mode:
                //マルチプレイと設定画面は追々
                topIndex = musicTopIndexBuffer;
                centerIndex = musicCenterIndexBuffer;
                bottomIndex = (1 + centerIndex) % musicDataBase.GetMusicList().Count;
                menuState = MenuState.Music;
                modePanels.SetActive(false);
                musicPanels.SetActive(true);
                findPanel(musicPanels);
                findText();
                break;
            case MenuState.Music:
                musicCenterIndexBuffer = centerIndex;
                musicTopIndexBuffer = topIndex;
                topIndex = MenuContent.DifficultyIds.Count - 1;
                selectedMusic = musicDataBase.GetMusicList()[centerIndex];
                menuState = MenuState.Difficulty;
                musicPanels.SetActive(false);
                difficultyPanels.SetActive(true);

                centerIndex = 0;
                bottomIndex = 1;
                findPanel(difficultyPanels);
                findText();
                break;
            case MenuState.Difficulty:
                selectedDifficulty = MenuContent.DifficultyIds[centerIndex];
                LoadScore();
                SceneManager.LoadScene("GameScene");
                break;
        }
    }

    public void Back()
    {
        //Difficultyの時はシーン遷移と曲保持
        switch (menuState)
        {
            case MenuState.Mode:
                break;
            case MenuState.Music:
                topIndex = MenuContent.ModeIds.Count - 1;
                centerIndex = 0;
                bottomIndex = 1;
                menuState = MenuState.Mode;
                musicPanels.SetActive(false);
                modePanels.SetActive(true);
                findPanel(modePanels);
                findText();
                break;
            case MenuState.Difficulty:
                topIndex = musicTopIndexBuffer;
                centerIndex = musicCenterIndexBuffer;
                bottomIndex = (1 + centerIndex) % musicDataBase.GetMusicList().Count;
                menuState = MenuState.Music;
                difficultyPanels.SetActive(false);
                musicPanels.SetActive(true);
                findPanel(musicPanels);
                findText();
                break;
        }
    }

   void LoadScore()
    {
        int i = 0;
        foreach(var x in menuContent.scoreLoaders)
        {
            if(x.MetaData["TITLE"] == selectedMusic.GetTitle() &&
            x.MetaData["PLAYLEVEL"] == selectedDifficulty){
                PlayerController.scoreManager = new ScoreManager(menuContent.scorePaths[i]);
            }
            i++;
        }
    }

    public void UpIndex()
    {
        int ids = 0;
        if (menuState == MenuState.Mode)
        {
            ids = MenuContent.ModeIds.Count;
        }
        else if (menuState == MenuState.Music)
        {
            ids = musicDataBase.GetMusicList().Count;
        }
        else if (menuState == MenuState.Difficulty)
        {
            ids = MenuContent.DifficultyIds.Count;

        }
        topIndex = (ids - 1 + topIndex) % ids;
        centerIndex = (ids - 1 + centerIndex) % ids;
        bottomIndex = (ids - 1 + bottomIndex) % ids;
    }

    public void DownIndex()
    {
        int ids = 0;
        if (menuState == MenuState.Mode)
        {
            ids = MenuContent.ModeIds.Count;
        }
        else if (menuState == MenuState.Music)
        {
            ids = musicDataBase.GetMusicList().Count;
        }
        else if (menuState == MenuState.Difficulty)
        {
            ids = MenuContent.DifficultyIds.Count;
        }
        topIndex = (1 + topIndex) % ids;
        centerIndex = (1 + centerIndex) % ids;
        bottomIndex = (1 + bottomIndex) % ids;
    }

    void findPanel(GameObject panels)
    {
        this.topPanel = panels.transform.Find("Top Panel").gameObject;
        this.centerPanel = panels.transform.Find("Center Panel").gameObject;
        this.bottomPanel = panels.transform.Find("Bottom Panel").gameObject;
    }

    void findText()
    {
        topText = topPanel.GetComponentInChildren<Text>();
        if (menuState == MenuState.Mode || menuState == MenuState.Difficulty)
        {
            centerText = centerPanel.GetComponentInChildren<Text>();
        }
        else if (menuState == MenuState.Music)
        {
            titleText = centerPanel.transform.Find("Title Text").gameObject.GetComponent<Text>();
            difficultyText = centerPanel.transform.Find("Difficulty Text").gameObject.GetComponent<Text>();
        }
        bottomText = bottomPanel.GetComponentInChildren<Text>();
    }

    public enum MenuState
    {
        Mode,
        Music,
        Difficulty
    }
}

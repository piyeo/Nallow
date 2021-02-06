using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player;

public class GameMenu : MonoBehaviour
{
#pragma warning disable 0649
#pragma warning disable 0414
    [SerializeField]
    private MusicDataBase musicDataBase;
    [SerializeField]
    private GameObject modePanel, musicPanel, difficultyPanel;

    public static GameMenu instance;
    public static MenuContent menuContent;
    public static Music selectedMusic;
    public static MenuState menuState;
    public static int musicCenterIndexBuffer, musicTopIndexBuffer;
    public static string selectedDifficulty;

    private ModeMenuState modeMenuState;
    private MusicMenuState musicMenuState;
    private DifficultyMenuState difficultyMenuState;

    void Start()
    {
        instance = this;

        menuContent = new MenuContent();
        modeMenuState = modePanel.GetComponent<ModeMenuState>();
        musicMenuState = musicPanel.GetComponent<MusicMenuState>();
        difficultyMenuState = difficultyPanel.GetComponent<DifficultyMenuState>();

        modeMenuState.DeactivatePanel();
        musicMenuState.DeactivatePanel();
        difficultyMenuState.DeactivatePanel();

        AudioManager.instance.PlayBGM("Menu");

        musicCenterIndexBuffer = 0;
        musicTopIndexBuffer = musicDataBase.GetMusicList().Count - 1;

        modeMenuState.ActivatePanel();
    }

    void Update()
    {
        switch (menuState)
        {
            case MenuState.Mode:
                modeMenuState.ShowText();
                break;
            case MenuState.Music:
                musicMenuState.ShowText();
                break;
            case MenuState.Difficulty:
                difficultyMenuState.ShowText();
                break;
        }
    }

    public void Select()
    {
        AudioManager.instance.PlaySE("Select");
        switch (menuState)
        {
            case MenuState.Mode:
                modeMenuState.DeactivatePanel();
                musicMenuState.ActivatePanel();
                break;
            case MenuState.Music:
                musicMenuState.DeactivatePanel();
                difficultyMenuState.ActivatePanel();
                break;
            case MenuState.Difficulty:
                difficultyMenuState.DeactivatePanel();
                LoadScore();
                AudioManager.instance.StopMusic();
                SceneManager.LoadScene("GameScene");
                break;
        }
    }

    public void Back()
    {
        AudioManager.instance.PlaySE("Change");
        switch (menuState)
        {
            case MenuState.Mode:
                //タイトル画面
                break;
            case MenuState.Music:
                musicMenuState.DeactivatePanel();
                modeMenuState.ActivatePanel();
                break;
            case MenuState.Difficulty:
                difficultyMenuState.DeactivatePanel();
                musicMenuState.ActivatePanel();
                break;
        }
    }

    public void Up()
    {
        AudioManager.instance.PlaySE("Change");
        switch (menuState)
        {
            case MenuState.Mode:
                modeMenuState.UpIndex();
                break;
            case MenuState.Music:
                musicMenuState.UpIndex();
                break;
            case MenuState.Difficulty:
                difficultyMenuState.UpIndex();
                break;
        }
    }

    public void Down() {
        AudioManager.instance.PlaySE("Change");
        switch (menuState)
        {
            case MenuState.Mode:
                modeMenuState.DownIndex();
                break;
            case MenuState.Music:
                musicMenuState.DownIndex();
                break;
            case MenuState.Difficulty:
                difficultyMenuState.DownIndex();
                break;
        }
    }

    void LoadScore()
    {
        int i = 0;
        foreach (var x in menuContent.scoreLoaders)
        {
            if (x.MetaData["TITLE"] == selectedMusic.GetTitle() &&
            x.MetaData["PLAYLEVEL"] == DifficultyMenuState.selectedDifficulty)
            {
                PlayerController.scoreManager = new ScoreManager(menuContent.scorePaths[i]);
                PlayerController.playingBGM = x.MetaData["TITLE"];
            }
            i++;
        }
    }

    public enum MenuState
    {
        Mode,
        Music,
        Difficulty
    }

}

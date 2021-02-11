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
    private GameObject modePanel, musicPanel, difficultyPanel, settingPanels;
    [SerializeField]
    private GameObject variableButtons, constantButtons;

    public static GameMenu instance;
    public static MenuContent menuContent;
    public static Music selectedMusic;
    public static MenuState menuState;
    public static int musicCenterIndexBuffer, musicTopIndexBuffer;
    public static int selectedMode;
    public static string selectedDifficulty;

    private ModeMenuState modeMenuState;
    private MusicMenuState musicMenuState;
    private DifficultyMenuState difficultyMenuState;
    private SettingMenuState settingMenuState;

    void Start()
    {
        instance = this;

        menuContent = new MenuContent();
        if(GameManager.handValue == HandEnum.Left)
        {
            LeftHandSwitch();
        }
        else if(GameManager.handValue == HandEnum.Right)
        {
            RightHandSwitch();
        }
        modeMenuState = modePanel.GetComponent<ModeMenuState>();
        musicMenuState = musicPanel.GetComponent<MusicMenuState>();
        difficultyMenuState = difficultyPanel.GetComponent<DifficultyMenuState>();
        settingMenuState = settingPanels.GetComponent<SettingMenuState>();

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
            case MenuState.Setting:
                settingMenuState.ShowText();
                break;
        }
    }

    void LoadScore()
    {
        int i = 0;
        foreach (var x in menuContent.scoreLoaders)
        {
            if (x.MetaData["TITLE"] == selectedMusic.GetTitle() &&
            x.MetaData["PLAYLEVEL"] == selectedDifficulty)
            {
                PlayerController.scoreManager = new ScoreManager(menuContent.scorePaths[i]);
                PlayerController.playingBGM = x.MetaData["TITLE"];
            }
            i++;
        }
    }

    public void LeftHandSwitch()
    {
        var _localScale = variableButtons.GetComponent<RectTransform>().localScale;
        _localScale.x = -1;
        variableButtons.GetComponent<RectTransform>().localScale = _localScale;
        _localScale = constantButtons.GetComponent<RectTransform>().localScale;
        _localScale.x = -1;
        constantButtons.GetComponent<RectTransform>().localScale = _localScale;
    }

    public void RightHandSwitch()
    {
        var _localScale = variableButtons.GetComponent<RectTransform>().localScale;
        _localScale.x = 1;
        variableButtons.GetComponent<RectTransform>().localScale = _localScale;
        _localScale = constantButtons.GetComponent<RectTransform>().localScale;
        _localScale.x = 1;
        constantButtons.GetComponent<RectTransform>().localScale = _localScale;
    }

    public void Select()
    {
        AudioManager.instance.PlaySE("Select");
        switch (menuState)
        {
            case MenuState.Mode:
                modeMenuState.DeactivatePanel();
                if (MenuContent.ModeIds[selectedMode] == "Single Play")
                {
                    musicMenuState.ActivatePanel();
                }
                else if(MenuContent.ModeIds[selectedMode] == "Setting")
                {
                    variableButtons.SetActive(false);
                    settingMenuState.ActivatePanel();
                }
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
                modeMenuState.DeactivatePanel();
                AudioManager.instance.StopMusic();
                SceneManager.LoadScene("TitleScene");
                break;
            case MenuState.Music:
                musicMenuState.DeactivatePanel();
                modeMenuState.ActivatePanel();
                break;
            case MenuState.Difficulty:
                difficultyMenuState.DeactivatePanel();
                musicMenuState.ActivatePanel();
                break;
            case MenuState.Setting:
                variableButtons.SetActive(true);
                settingMenuState.DeactivatePanel();
                modeMenuState.ActivatePanel();
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

    public enum MenuState
    {
        Mode,
        Music,
        Difficulty,
        Setting
    }

}

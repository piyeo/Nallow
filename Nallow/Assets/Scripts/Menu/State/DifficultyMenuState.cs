using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuState : MenuStateBase
{
#pragma warning disable 0649
    [SerializeField]
    private Text centerText, musicText;

    public static string selectedDifficulty;

    private Dictionary<int, string> difficultyIds = MenuContent.DifficultyIds;

    public override void ShowText()
    {
        var topDifficulty = difficultyIds[topIndex];
        var centerDifficulty = difficultyIds[centerIndex];
        var bottomDifficulty = difficultyIds[bottomIndex];
        topText.text = MenuContent.DifficultyIds[topIndex]
            + " " + GameMenu.selectedMusic.GetDifficulty(topDifficulty);
        centerText.text = MenuContent.DifficultyIds[centerIndex]
            + " " + GameMenu.selectedMusic.GetDifficulty(centerDifficulty);
        bottomText.text = MenuContent.DifficultyIds[bottomIndex]
            + " " + GameMenu.selectedMusic.GetDifficulty(bottomDifficulty);
        musicText.text = GameMenu.selectedMusic.GetTitle();
    }

    public override void ActivatePanel()
    {
        GameMenu.menuState = GameMenu.MenuState.Difficulty;
        this.ResetPanelIndex();
        panels.SetActive(true);
    }

    public override void DeactivatePanel()
    {
        selectedDifficulty = difficultyIds[centerIndex];
        panels.SetActive(false);
    }

    public override void ResetPanelIndex()
    {
        base.ResetPanelIndex();
        topIndex = difficultyIds.Count - 1;
    }

    public override void Start()
    {
        countElements = difficultyIds.Count;
    }
}

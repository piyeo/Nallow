using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuState : MenuStateBase
{
#pragma warning disable 0649
    [Space(10)]
    [SerializeField] private Text centerText;
    [SerializeField] private Text musicText1;
    [SerializeField] private Text musicText2;

    private Dictionary<int, string> difficultyIds = MenuContent.DifficultyIds;

    private readonly float textWidth = 150;

    private Vector3 musicText1Position, musicText2Position;

    public override void Start()
    {
        countElements = difficultyIds.Count;
        musicText1Position = musicText1.rectTransform.localPosition;
        musicText2Position = musicText2.rectTransform.localPosition;
        musicText1Position.x = 0;
        musicText2Position.x = textWidth;
        musicText2.rectTransform.localPosition = musicText2Position;
        musicText1.rectTransform.localPosition = musicText1Position;
    }

    public void Update()
    {
        ScrollText();
    }

    private void ScrollText()
    {
        musicText1Position.x--;
        musicText2Position.x--;
        if (musicText1Position.x < -textWidth)
        {
            musicText1Position.x = textWidth;
        }
        if (musicText2Position.x < -textWidth)
        {
            musicText2Position.x = textWidth;
        }
        musicText1.rectTransform.localPosition = musicText1Position;
        musicText2.rectTransform.localPosition = musicText2Position;
    }

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
        musicText1.text = GameMenu.selectedMusic.GetTitle();
        musicText2.text = GameMenu.selectedMusic.GetTitle();
    }

    public override void ActivatePanel()
    {
        GameMenu.menuState = GameMenu.MenuState.Difficulty;
        this.ResetPanelIndex();
        panels.SetActive(true);
    }

    public override void DeactivatePanel()
    {
        GameMenu.selectedDifficulty = difficultyIds[centerIndex];
        panels.SetActive(false);
    }

    public override void ResetPanelIndex()
    {
        base.ResetPanelIndex();
        topIndex = difficultyIds.Count - 1;
    }
}

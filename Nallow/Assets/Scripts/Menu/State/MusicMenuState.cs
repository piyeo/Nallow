using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMenuState : MenuStateBase
{
#pragma warning disable 0649
    [Space(10)]
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text difficultyText;
    [SerializeField]
    private Image musicJacket;

    public override void ShowText()
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

    public override void ActivatePanel()
    {
        GameMenu.menuState = GameMenu.MenuState.Music;
        this.ResetPanelIndex();
        panels.SetActive(true);
    }

    public override void DeactivatePanel()
    {
        GameMenu.musicCenterIndexBuffer = centerIndex;
        GameMenu.musicTopIndexBuffer = topIndex;
        GameMenu.selectedMusic = musicDataBase.GetMusicList()[centerIndex];
        panels.SetActive(false);
    }

    override public void ResetPanelIndex()
    {
        topIndex = GameMenu.musicTopIndexBuffer;
        centerIndex = GameMenu.musicCenterIndexBuffer;
        bottomIndex = (1 + centerIndex) % musicDataBase.GetMusicList().Count;
    }

    override public void Start()
    {
        countElements = musicDataBase.GetMusicList().Count;
    }
}

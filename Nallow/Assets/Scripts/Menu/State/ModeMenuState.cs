using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenuState : MenuStateBase
{
#pragma warning disable 0649
    [SerializeField]
    private Text centerText;
    private Dictionary<int, string> modeIds = MenuContent.ModeIds;

    public override void ShowText()
    {
        topText.text = modeIds[topIndex];
        centerText.text = modeIds[centerIndex];
        bottomText.text = modeIds[bottomIndex];
    }

    public override void ActivatePanel()
    {
        GameMenu.menuState = GameMenu.MenuState.Mode;
        this.ResetPanelIndex();
        panels.SetActive(true);
    }

    public override void DeactivatePanel()
    {
        //タイトルに戻るため
        panels.SetActive(false);
    }

    public override void ResetPanelIndex()
    {
        base.ResetPanelIndex();
        topIndex = modeIds.Count - 1;
    }

    public override void Start()
    {
        countElements = modeIds.Count;
    }
}

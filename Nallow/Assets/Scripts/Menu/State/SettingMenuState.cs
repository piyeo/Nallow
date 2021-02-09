using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class SettingMenuState : MenuStateBase
{
#pragma warning disable 0649
    [Space(10)]
    [SerializeField]
    private Button LeftHandButton;
    [SerializeField]
    private Button RightHandButton;
    [SerializeField]
    private Text tapTimingValueText,noteSpeedValueText;

    public override void ShowText() {
        tapTimingValueText.text = GameManager.tapTimingValue.ToString();
        noteSpeedValueText.text = PlayerController.NoteSpeed.ToString();

    }
    public override void ActivatePanel() {
        GameMenu.menuState = GameMenu.MenuState.Setting;
        panels.SetActive(true);
    }
    public override void DeactivatePanel() {
        panels.SetActive(false);
    }
}

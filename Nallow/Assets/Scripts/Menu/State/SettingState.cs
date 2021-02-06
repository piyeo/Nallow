using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class SettingState : MenuStateBase
{
#pragma warning disable 0649
    [Space(10)]
    [SerializeField]
    private Button LeftHandButton;
    [SerializeField]
    private Button RightHandButton;
    [SerializeField]
    private Text tapTimingValueText;

    public override void ShowText() {
        tapTimingValueText.text = PlayerController.tapTimingValue.ToString();
    }
    public override void ActivatePanel() {
    }
    public override void DeactivatePanel() {
    }
}

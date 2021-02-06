using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button LeftHandButton;
    [SerializeField]
    private Button RightHandButton;

    public void PressUpButton()
    {
        GameMenu.instance.Up();
    }

    public void PressDownButton()
    {
        GameMenu.instance.Down();
    }

    public void PressBackButton()
    {
        GameMenu.instance.Back();
    }

    public void PressSelectButton()
    {
        GameMenu.instance.Select();
    }

    public void PressLeftHandButton()
    {
        LeftHandButton.GetComponent<Button>().image.color = Color.gray;
        RightHandButton.GetComponent<Button>().image.color = Color.white;
        GameManager.handValue = HandEnum.Left;
        GameMenu.instance.LeftHandSwitch();
    }

    public void PressRightHandButton()
    {
        RightHandButton.GetComponent<Button>().image.color = Color.gray;
        LeftHandButton.GetComponent<Button>().image.color = Color.white;
        GameManager.handValue = HandEnum.Left;
        GameMenu.instance.RightHandSwitch();
    }
}

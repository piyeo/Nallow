using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public void PressUpButton()
    {
        GameMenu.instance.UpIndex();
    }

    public void PressDownButton()
    {
        GameMenu.instance.DownIndex();
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

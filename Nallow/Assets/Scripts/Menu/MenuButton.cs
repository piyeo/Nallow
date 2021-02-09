using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class MenuButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button LeftHandButton;
    [SerializeField]
    private Button RightHandButton;
    [SerializeField]
    private Slider bgmVolumeSlider;
    [SerializeField]
    private Slider seVolumeSlider;

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

    public void PressNoteSpeedUpButton()
    {
        if(PlayerController.NoteSpeed < 10)
        {
            PlayerController.NoteSpeed++;
        }
    }

    public void PressNoteSpeedDownButton()
    {
        if (1 < PlayerController.NoteSpeed)
        {
            PlayerController.NoteSpeed--;
        }
    }

    public void PressTapTimingUpButton()
    {
        if (GameManager.tapTimingValue < 10)
        {
            GameManager.tapTimingValue++;
        }
    }

    public void PressTapTimingDownButton()
    {
        if (-10 < GameManager.tapTimingValue)
        {
            GameManager.tapTimingValue--;
        }
    }

    public void SlideSetBgmVolume()
    {
        AudioManager.instance.audioMixer.SetFloat("BGM",
            AudioManager.instance.ConvertVolumeToDb(bgmVolumeSlider.value));
    }

    public void SlideSetSeVolume()
    {
        AudioManager.instance.audioMixer.SetFloat("SE",
            AudioManager.instance.ConvertVolumeToDb(seVolumeSlider.value));
    }
}

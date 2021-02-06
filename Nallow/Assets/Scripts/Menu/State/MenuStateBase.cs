using UnityEngine;
using UnityEngine.UI;

public abstract class MenuStateBase : MonoBehaviour
{
#pragma warning disable 0649
    [Header("CommonField")]
    [SerializeField]
    protected GameObject panels;
    [SerializeField]
    protected Text topText, bottomText;
    [SerializeField]
    protected MusicDataBase musicDataBase;
    protected int topIndex, centerIndex, bottomIndex;
    protected int countElements;
    public abstract void ShowText();
    public abstract void ActivatePanel();
    public abstract void DeactivatePanel();

    //protected GameMenu.MenuState menuState;

    /*
    public GameMenu.MenuState GetMenuState() {
        return this.menuState;
    }

    public void SetMenuState(GameMenu.MenuState _menuState)
    {
        this.menuState = _menuState;
    }
    */

    public virtual void ResetPanelIndex()
    {
        this.centerIndex = 0;
        this.bottomIndex = 1;
    }

    public void UpIndex()
    {
        topIndex = (countElements - 1 + topIndex) % countElements;
        centerIndex = (countElements - 1 + centerIndex) % countElements;
        bottomIndex = (countElements - 1 + bottomIndex) % countElements;
    }

    public void DownIndex()
    {
        topIndex = (1 + topIndex) % countElements;
        centerIndex = (1 + centerIndex) % countElements;
        bottomIndex = (1 + bottomIndex) % countElements;
    }

    public virtual void Start()
    {
    }
}
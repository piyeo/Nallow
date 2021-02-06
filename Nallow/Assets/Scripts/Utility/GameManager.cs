using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HandEnum handValue;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        handValue = HandEnum.Right;
    }
}

public enum HandEnum
{
    Left,
    Right
}

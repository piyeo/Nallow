using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static HandEnum handValue;
    public static int tapTimingValue;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        handValue = HandEnum.Right;
        tapTimingValue = 0;
    }
}

public enum HandEnum
{
    Left,
    Right
}

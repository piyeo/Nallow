using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject audioManager;
    public GameObject gameManager;

    void Awake()
    {
        if(AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}

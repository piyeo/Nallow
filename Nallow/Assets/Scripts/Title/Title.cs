using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Title : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Text startText;

    private Sequence startTextSequence;

    private void Start()
    {
        AudioManager.instance.PlayBGM("Title");
        startText.DOFade(0, 1)
            .SetEase(Ease.Flash, 1)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        GetScreenTapped();
    }

    private void GetScreenTapped()
    {
        if (Input.GetMouseButtonDown(0)) {
            AudioManager.instance.StopMusic();
            DOTween.KillAll();
            SceneManager.LoadScene("MenuScene");
        }
    }
}
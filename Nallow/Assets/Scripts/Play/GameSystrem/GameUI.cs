using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Player;

public class GameUI : MonoBehaviour
{
#pragma warning disable 0414
#pragma warning disable 0649
    [SerializeField] private Text pointsText, comboText, judgeText;
    [SerializeField] private Text JudgeResultText, ReturnText, ResultText;
    [SerializeField] private GameObject objComboValue, objJudgeValue, resultPanel;

    private Text comboValueText;
    private RectTransform objComboValueRectTransform;
    private RectTransform objJudgeValueRectTransform;

    private readonly string perfectColor = "#FF61C4";
    private readonly string greatColor = "#FF9662";
    private readonly string goodColor = "#A3FF62";
    private readonly string badColor = "#8962FF";
    private readonly string missColor = "#9D9D9D";

    private Color32 judgeColor;

    public static GameUI instance;

    private Color GetJudgeColor(string colorCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(colorCode, out color))
        {
            return color;
        }
        else
        {
            return new Color(255, 255, 255);
        }
    }

    private Sequence comboSequence;
    private Sequence judgeSequence;

    private void Start()
    {
        instance = this;
        comboValueText = objComboValue.GetComponent<Text>();
        resultPanel.SetActive(false);
        pointsText.text = "score\n0";
        comboText.text = "";
        comboValueText.text = "";
        judgeText.text = "";

        //アニメーション用
        objComboValueRectTransform = objComboValue.GetComponent<RectTransform>();
        objJudgeValueRectTransform = objJudgeValue.GetComponent<RectTransform>();
        DOTween.Init();
        comboSequence = DOTween.Sequence()
            .Append(objComboValueRectTransform.DOScale(new Vector3(2f, 2f, 2f), 0.1f))
            .Append(objComboValueRectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.1f))
            .Pause()
            .SetAutoKill(false)
            .SetLink(objComboValue);
        judgeSequence = DOTween.Sequence()
            .Append(objJudgeValueRectTransform.DOScale(new Vector3(1.5f, 1.5f, 2f), 0.1f))
            .Append(objJudgeValueRectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.1f))
            .Pause()
            .SetAutoKill(false)
            .SetLink(objJudgeValue);
    }

    private void Update()
    {
        showPlayingUI();
    }

    private void showPlayingUI()
    {
        switch (GameEvaluation.currentJudge)
        {
            case "Perfect":
                judgeColor = GetJudgeColor(perfectColor);
                break;
            case "Great":
                judgeColor = GetJudgeColor(greatColor);
                break;
            case "Good":
                judgeColor = GetJudgeColor(goodColor);
                break;
            case "Bad":
                judgeColor = GetJudgeColor(badColor);
                break;
            case "Miss":
                judgeColor = GetJudgeColor(missColor);
                break;
        }
        if (GameEvaluation.currentJudge != "None")
        {
            judgeText.text = GameEvaluation.currentJudge.ToString();
            judgeText.color = judgeColor;
        }

        if (GameEvaluation.currentCombo == 0)
        {
            comboText.text = "";
            comboValueText.text = "";
        }
        else
        {
            comboText.text = "combo";
            comboValueText.text = GameEvaluation.currentCombo.ToString();
        }

        pointsText.text = "score\n" + Mathf.Floor(GameEvaluation.currentPoints).ToString();
    }

    public void ComboAnimation()
    {
        comboSequence.Restart();
    }

    public void JudgeAnimation()
    {
        judgeSequence.Restart();
    }

    public IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(3.0f);
        judgeText.text = "";
        GameEvaluation.currentJudge = "None";
        AudioManager.instance.PlaySE("GameEnd");
        resultPanel.SetActive(true);
        string perfect = string.Format("{0:000}", GameEvaluation.JudgementCounts[JudgementType.Perfect]);
        string great = string.Format("{0:000}", GameEvaluation.JudgementCounts[JudgementType.Great]);
        string good = string.Format("{0:000}", GameEvaluation.JudgementCounts[JudgementType.Good]);
        string bad = string.Format("{0:000}", GameEvaluation.JudgementCounts[JudgementType.Bad]);
        string miss = string.Format("{0:000}", GameEvaluation.JudgementCounts[JudgementType.Miss]);
        JudgeResultText.text = string.Format(JudgeResultText.text,perfect, great, good, bad, miss);
        ResultText.text = GameEvaluation.GetResult();
        ReturnText.text = "画面タップで戻る";
    }
}

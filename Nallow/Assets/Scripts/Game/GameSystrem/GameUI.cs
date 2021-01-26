using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
#pragma warning disable 0414
#pragma warning disable 0649
    [SerializeField] private Text pointsText;
    [SerializeField] private Text comboText;
    [SerializeField] private Text judgeText;
    [SerializeField] private GameObject objComboValue;
    [SerializeField] private GameObject objJudgeValue;

    private Text comboValueText;
    private RectTransform objComboValueRectTransform;
    private RectTransform objJudgeValueRectTransform;

    private readonly string perfectColor = "#FF61C4";
    private readonly string greatColor = "#FF9662";
    private readonly string goodColor = "#A3FF62";
    private readonly string badColor = "#8962FF";
    private readonly string missColor = "#9D9D9D";

    private Color32 judgeColor;

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

    private static Sequence comboSequence;
    private static Sequence judgeSequence;

    private void Start()
    {
        comboValueText = objComboValue.GetComponent<Text>();
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

    public static void ComboAnimation()
    {
        comboSequence.Restart();
    }

    public static void JudgeAnimation()
    {
        judgeSequence.Restart();
    }
}

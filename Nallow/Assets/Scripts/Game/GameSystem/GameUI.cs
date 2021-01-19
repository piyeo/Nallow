using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
#pragma warning disable 0414
#pragma warning disable 0649
    [SerializeField] private Text pointsText;
    [SerializeField] private Text comboText;
    [SerializeField] private Text judgeText;

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

    private void Start()
    {
        pointsText.text = "score\n0";
        comboText.text = "";
        judgeText.text = "";
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
        }
        else
        {
            comboText.text = "combo\n" + GameEvaluation.currentCombo.ToString();
        }

        pointsText.text = "score\n" + Mathf.Floor(GameEvaluation.currentPoints).ToString();
    }
}

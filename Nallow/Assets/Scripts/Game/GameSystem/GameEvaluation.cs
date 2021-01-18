using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player;
using Note;

public class GameEvaluation : MonoBehaviour
{
    private static readonly float basePoints = 100000;
    private static float difficultyRate;
    private static float allNotesCount;
    private static readonly Dictionary<JudgementType, float> JudgementRates =
        new Dictionary<JudgementType, float> {
            { JudgementType.Perfect, 1.0f },
            { JudgementType.Great, 0.9f },
            { JudgementType.Good, 0.8f },
            { JudgementType.Bad, 0f },
            { JudgementType.Miss, 0f }
        };
    private static readonly Dictionary<int, float> ComboRates =
        new Dictionary<int, float>
        {
            { 0, 1.00f},
            { 10, 1.01f },
            { 20, 1.03f },
            { 30, 1.05f },
            { 40, 1.07f },
            { 999, 1.10f },
        };

    public static float currentPoints;
    public static int currentCombo;
    public static int maxCombo;
    public static Dictionary<JudgementType, int> JudgementCounts =
        new Dictionary<JudgementType, int>();

    private void Start()
    {
        //変更予定
        difficultyRate = 1;

        allNotesCount = PlayerController.scoreManager.noteProperties
            .Count(x => x.noteType != NoteType.Long) +
            PlayerController.scoreManager.noteProperties
            .Count(x => x.noteType == NoteType.Long) * 2;
        currentPoints = 0f;
        currentCombo = 0;
        maxCombo = 0;
        JudgementCounts[JudgementType.Perfect] = 0;
        JudgementCounts[JudgementType.Great] = 0;
        JudgementCounts[JudgementType.Good] = 0;
        JudgementCounts[JudgementType.Bad] = 0;
        JudgementCounts[JudgementType.Miss] = 0;
    }

    private void Update()
    {
        maxCombo = Mathf.Max(currentCombo, maxCombo);
    }

    public static void Evaluation(JudgementType judgementType)
    {
        if(judgementType == JudgementType.None) { return; }
        if(judgementType == JudgementType.Miss)
        {
            currentCombo = 0;
            JudgementCounts[judgementType]++;
            return;
        }

        currentCombo++;

        float comboRate = ComboRates.Where(x => x.Key <= currentCombo)
            .Last().Value;
        var addPoints = basePoints * difficultyRate /
            (allNotesCount * JudgementRates[judgementType] * comboRate);
        currentPoints += addPoints;

        Debug.Log(currentCombo);
        Debug.Log(currentPoints);

        JudgementCounts[judgementType]++;

    }

}

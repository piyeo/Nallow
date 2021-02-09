using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Note;
using Player;

public class JudgementManager : MonoBehaviour
{
    public static Dictionary<JudgementType, float> JudgementZone =
        new Dictionary<JudgementType, float>
        {
            { JudgementType.Perfect, 0.05f },
            { JudgementType.Great, 0.10f },
            { JudgementType.Good, 0.20f },
            { JudgementType.Bad, 0.30f },
        };

    [SerializeField]
    static float flickDifference = 0.2f;

    private Vector2 flickStartPos;
    private Vector2 flickEndPos;

    public void GetTapDownLeft()
    {
        GetTapDown(0);
    }

    public void GetTapDownRight()
    {
        GetTapDown(1);
    }

    public void GetTapUpLeft()
    {
        GetTapUp(0);
    }

    public void GetTapUpRight()
    {
        GetTapUp(1);
    }

    private void GetTapDown(int lane)
    {
        var nearest = GetNextNote(lane);
        if (!nearest) { return; }

        flickStartPos = Input.mousePosition;

        var noteSec = nearest.noteProperty.secBegin;
        var differenceSec = Mathf.Abs(noteSec - PlayerController.CurrentSec
            + (GameManager.tapTimingValue * 0.1f));
        nearest.OnTapDown(GetJudgementType(differenceSec));
    }

    private void GetTapUp(int lane)
    {
        var processed = GetProcessedNote(lane);
        if (!processed) { return; };

        flickEndPos = Input.mousePosition;

        if (processed.GetType() == typeof(FlickNoteController))
        {
            bool isFlicked = flickDifference <= Mathf.Abs(flickStartPos.x - flickEndPos.x) ||
                flickDifference <= Mathf.Abs(flickStartPos.y - flickEndPos.y);
            processed.OnFlick(isFlicked);
            return;
        }

        var noteSec = processed.noteProperty.secEnd;
        var differenceSec = Mathf.Abs(noteSec - PlayerController.CurrentSec
            + (GameManager.tapTimingValue * 0.1f));
        processed.OnTapUp(GetJudgementType(differenceSec));
    }

    private JudgementType GetJudgementType(float differenceSec)
    {
        if(differenceSec <= JudgementZone[JudgementType.Perfect])
        {
            return JudgementType.Perfect;
        }
        else if(differenceSec <= JudgementZone[JudgementType.Great])
        {
            return JudgementType.Great;
        }
        else if(differenceSec <= JudgementZone[JudgementType.Good])
        {
            return JudgementType.Good;
        }
        else if (differenceSec <= JudgementZone[JudgementType.Bad])
        {
            return JudgementType.Bad;
        }
        else
        {
            return JudgementType.None;
        }
    }

    private NoteControllerBase GetNextNote(int lane)
    {
        var noteControllers =
            PlayerController.AliveNoteControllers
            .Where(x => x.noteProperty.lane == lane);
        if (noteControllers.Any())
        {
            return noteControllers
                .OrderBy(x => Mathf.Abs(
                    x.noteProperty.beatBegin - PlayerController.CurrentBeat))
                .First();
        }
        else
        {
            return null;
        }
    }

    private NoteControllerBase GetProcessedNote(int lane)
    {
        var noteControllers =
            PlayerController.AliveNoteControllers
            .Where(x => x.noteProperty.lane == lane && x.isProcessed);
        if (noteControllers.Any())
        {
            return noteControllers
                .OrderBy(x => Mathf.Abs(
                    x.noteProperty.beatBegin - PlayerController.CurrentBeat))
                .First();
        }
        else
        {
            return null;
        }
    }

}

public enum JudgementType
{
    None,
    Miss,
    Perfect,
    Great,
    Good,
    Bad
}

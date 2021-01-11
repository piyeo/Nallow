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
        var nearest = GetNearestNoteControllerBaseInLane(lane);
        if (!nearest) { return; }

        var noteSec = nearest.noteProperty.secBegin;
        var differenceSec = Mathf.Abs(noteSec - PlayerController.CurrentSec);
        nearest.OnTapDown(GetJudgementType(differenceSec));
    }

    private void GetTapUp(int lane)
    {
        var processed = GetProcessedNoteControllerBaseInLane(lane);
        if (!processed) { return; };

        var noteSec = processed.noteProperty.secEnd;
        var differenceSec = Mathf.Abs(noteSec - PlayerController.CurrentSec);
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
            return JudgementType.Miss;
        }
    }

    private NoteControllerBase GetNearestNoteControllerBaseInLane(int lane)
    {
        var noteControllers =
            PlayerController.ExistingNoteControllers
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

    private NoteControllerBase GetProcessedNoteControllerBaseInLane(int lane)
    {
        var noteControllers =
            PlayerController.ExistingNoteControllers
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
    Miss,
    Perfect,
    Great,
    Good,
    Bad
}

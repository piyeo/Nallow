using UnityEngine;
using Player;

namespace Note
{
    public class FlickNoteController : NoteControllerBase
    {
        void Update()
        {
            Vector2 position = new Vector2();
            //1.3f : JudgeZoneのx座標
            if (noteProperty.lane == 0) { position.x = -1.3f; }
            if (noteProperty.lane == 1) { position.x = 1.3f; }
            position.y = (noteProperty.beatBegin - PlayerController.CurrentBeat) * PlayerController.ScrollSpeed;
            transform.localPosition = position;
        }
    }
}

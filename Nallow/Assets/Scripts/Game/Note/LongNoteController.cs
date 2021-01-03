using UnityEngine;
using Player;

namespace Note
{
    public class LongNoteController : NoteControllerBase
    {
        #pragma warning disable 0649
        [SerializeField] GameObject objBegin;
        [SerializeField] GameObject objEnd;
        [SerializeField] GameObject objTrail;

        void Update()
        {
            Vector2 positionBegin = new Vector2();
            if (noteProperty.lane == 0) { positionBegin.x = -1.3f; }
            if (noteProperty.lane == 1) { positionBegin.x = 1.3f; }
            positionBegin.y = (noteProperty.beatBegin - PlayerController.CurrentBeat) * PlayerController.ScrollSpeed;
            objBegin.transform.localPosition = positionBegin;

            Vector2 positionEnd = new Vector2();
            if (noteProperty.lane == 0) { positionEnd.x = -1.3f; }
            if (noteProperty.lane == 1) { positionEnd.x = 1.3f; }
            positionEnd.y = (noteProperty.beatEnd - PlayerController.CurrentBeat) * PlayerController.ScrollSpeed;
            objEnd.transform.localPosition = positionEnd;

            Vector2 positionTrail = (positionBegin + positionEnd) / 2f;
            objTrail.transform.localPosition = positionTrail;

            Vector2 scale = objTrail.transform.localScale;
            scale.y = positionEnd.y - positionBegin.y;
            objTrail.transform.localScale = scale;
        }
    }
}

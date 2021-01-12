using UnityEngine;
using Player;
using GameCommon;

namespace Note
{
    public class LongNoteController : NoteControllerBase
    {
        #pragma warning disable 0649
        [SerializeField] GameObject objBegin;
        [SerializeField] GameObject objEnd;
        [SerializeField] GameObject objTrail;

        [SerializeField] Color32 processedColorEdges;
        [SerializeField] Color32 processedColorTrail;

        void Update()
        {
            setTransform();
            CheckMiss();
        }

        void setTransform()
        {
            Vector2 positionBegin = new Vector2();
            if (noteProperty.lane == 0) { positionBegin.x = -1.3f; }
            if (noteProperty.lane == 1) { positionBegin.x = 1.3f; }

            if (isProcessed)
            {
                if (noteProperty.lane == 0) { positionBegin.y = GameConst.judgeZoneLeftPosition.y; }
                if (noteProperty.lane == 1) { positionBegin.y = GameConst.judgeZoneRightPosition.y; }
            }
            else
            {
                positionBegin.y = (noteProperty.beatBegin - PlayerController.CurrentBeat) * PlayerController.ScrollSpeed;
            }
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

        void CheckMiss()
        {
            if(!isProcessed &&
                noteProperty.secBegin - PlayerController.CurrentSec <
                -JudgementManager.JudgementZone[JudgementType.Bad])
            {
                PlayerController.ExistingNoteControllers.Remove(GetComponent<NoteControllerBase>());
                Destroy(gameObject);
            }

            if (isProcessed &&
                noteProperty.secEnd - PlayerController.CurrentSec <
                -JudgementManager.JudgementZone[JudgementType.Bad])
            {
                isProcessed = false;
                PlayerController.ExistingNoteControllers.Remove(GetComponent<NoteControllerBase>());
                Destroy(gameObject);
            }
        }

        public override void OnTapDown(JudgementType judgementType)
        {
            Debug.Log(judgementType);

            if(judgementType != JudgementType.Miss)
            {
                isProcessed = true;

                objBegin.GetComponent<SpriteRenderer>().color = processedColorEdges;
                objEnd.GetComponent<SpriteRenderer>().color = processedColorEdges;
                objTrail.GetComponent<SpriteRenderer>().color = processedColorTrail;
            }
        }

        public override void OnTapUp(JudgementType judgementType)
        {
            Debug.Log(judgementType);
            isProcessed = false;
            PlayerController.ExistingNoteControllers.Remove(GetComponent<NoteControllerBase>());
            Destroy(gameObject);
        }
    }
}

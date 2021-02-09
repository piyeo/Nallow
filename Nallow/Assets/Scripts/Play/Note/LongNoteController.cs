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

        [SerializeField] Color32 processedEdgesColor;
        [SerializeField] Color32 processedTrailColor;

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
                positionBegin.y = (noteProperty.beatBegin - PlayerController.CurrentBeat) * PlayerController.NoteSpeed;
            }
            objBegin.transform.localPosition = positionBegin;

            Vector2 positionEnd = new Vector2();
            if (noteProperty.lane == 0) { positionEnd.x = -1.3f; }
            if (noteProperty.lane == 1) { positionEnd.x = 1.3f; }
            positionEnd.y = (noteProperty.beatEnd - PlayerController.CurrentBeat) * PlayerController.NoteSpeed;
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
                //始点と終点の分
                GameEvaluation.Evaluation(JudgementType.Miss);
                GameEvaluation.Evaluation(JudgementType.Miss);
                PlayerController.AliveNoteControllers.Remove(GetComponent<NoteControllerBase>());
                Destroy(gameObject);
            }

            if (isProcessed &&
                noteProperty.secEnd - PlayerController.CurrentSec <
                -JudgementManager.JudgementZone[JudgementType.Bad])
            {
                GameEvaluation.Evaluation(JudgementType.Miss);
                isProcessed = false;
                PlayerController.AliveNoteControllers.Remove(GetComponent<NoteControllerBase>());
                Destroy(gameObject);
            }
        }

        public override void OnTapDown(JudgementType judgementType)
        {
            if(judgementType != JudgementType.None)
            {
                GameEvaluation.Evaluation(judgementType);

                isProcessed = true;

                objBegin.GetComponent<SpriteRenderer>().color = processedEdgesColor;
                objEnd.GetComponent<SpriteRenderer>().color = processedEdgesColor;
                objTrail.GetComponent<SpriteRenderer>().color = processedTrailColor;
            }

            AudioManager.instance.PlaySE("Tap");
        }

        public override void OnTapUp(JudgementType judgementType)
        {
            if(judgementType != JudgementType.None)
            {
                GameEvaluation.Evaluation(judgementType);
            }
            else
            {
                GameEvaluation.Evaluation(JudgementType.Miss);
            }
            isProcessed = false;
            PlayerController.AliveNoteControllers.Remove(GetComponent<NoteControllerBase>());
            Destroy(gameObject);

            AudioManager.instance.PlaySE("Tap");
        }
    }
}

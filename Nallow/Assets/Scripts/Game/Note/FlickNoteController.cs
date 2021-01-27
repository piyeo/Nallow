using UnityEngine;
using Player;
using GameCommon;

namespace Note
{
    public class FlickNoteController : NoteControllerBase
    {
        private JudgementType judgementType;

        void Update()
        {
            SetTransform();
            CheckMiss();
        }

        void SetTransform()
        {
            Vector2 position = new Vector2();
            //1.3f : JudgeZoneのx座標
            if (noteProperty.lane == 0) { position.x = -1.3f; }
            if (noteProperty.lane == 1) { position.x = 1.3f; }

            if (isProcessed)
            {
                if (noteProperty.lane == 0) { position.y = GameConst.judgeZoneLeftPosition.y; }
                if (noteProperty.lane == 1) { position.y = GameConst.judgeZoneRightPosition.y; }
            }
            else
            {
                position.y = (noteProperty.beatBegin - PlayerController.CurrentBeat) * PlayerController.ScrollSpeed;
            }
            transform.localPosition = position;
        }

        void CheckMiss()
        {
            if (noteProperty.secBegin - PlayerController.CurrentSec <
                -JudgementManager.JudgementZone[JudgementType.Good])
            {
                Debug.Log(JudgementType.Miss);
                PlayerController.AliveNoteControllers.Remove(GetComponent<NoteControllerBase>());
                Destroy(gameObject);
            }
        }

        public override void OnTapDown(JudgementType _judgementType)
        {
            judgementType = _judgementType;
            if (judgementType != JudgementType.None)
            {
                isProcessed = true;
                AudioManager.instance.PlaySE("Flick");
            }
        }

        public override void OnFlick(bool isFlicked)
        {
            if (!isFlicked) { return; }
            if (!gameObject) { return; }

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

        }
    }
}

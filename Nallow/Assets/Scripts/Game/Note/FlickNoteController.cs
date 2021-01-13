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
                PlayerController.AliveNoteControllers.Remove(GetComponent<NoteControllerBase>());
                Destroy(gameObject);
            }
        }

        public override void OnTapDown(JudgementType _judgementType)
        {
            judgementType = _judgementType;
            if (judgementType != JudgementType.Miss)
            {
                isProcessed = true;
            }
        }

        public override void OnFlick(bool isFlicked)
        {
            if (!isFlicked) { return; }
            //確定していない
            Debug.Log(judgementType + "FlickOK");

            isProcessed = false;
            PlayerController.AliveNoteControllers.Remove(GetComponent<NoteControllerBase>());
            Destroy(gameObject);

            AudioManager.instance.PlaySE("Flick");
        }
    }
}

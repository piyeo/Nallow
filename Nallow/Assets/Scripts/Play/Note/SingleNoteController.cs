using UnityEngine;
using Player;

namespace Note
{
    public class SingleNoteController : NoteControllerBase
    {
        void Update()
        {
            SetTransform();
            CheckMiss();
        }

         private void SetTransform()
        {
            Vector2 position = new Vector2();
            //1.3f : JudgeZoneのx座標
            if (noteProperty.lane == 0) { position.x = -1.3f; }
            if (noteProperty.lane == 1) { position.x = 1.3f; }
            position.y = (noteProperty.beatBegin - PlayerController.CurrentBeat) * PlayerController.ScrollSpeed;
            transform.localPosition = position;
        }

        private void CheckMiss()
        {
            if (noteProperty.secBegin - PlayerController.CurrentSec <
                -JudgementManager.JudgementZone[JudgementType.Bad])
            {
                GameEvaluation.Evaluation(JudgementType.Miss);
                PlayerController.AliveNoteControllers.Remove(
                    GetComponent<NoteControllerBase>()
                );
                Destroy(gameObject);
            }
        }

        public override void OnTapDown(JudgementType judgementType)
        {
            if(judgementType != JudgementType.None)
            {
                GameEvaluation.Evaluation(judgementType);
                PlayerController.AliveNoteControllers.Remove(
                    GetComponent<NoteControllerBase>()
                );
                Destroy(gameObject);
            }

            AudioManager.instance.PlaySE("Tap");
        }
    }
}

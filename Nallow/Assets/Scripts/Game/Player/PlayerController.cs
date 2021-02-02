using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Note;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private GameObject ObjSingleNote;
        [SerializeField] private GameObject ObjLongNote;
        [SerializeField] private GameObject ObjFlickNote;
        [SerializeField] private GameObject gameZone;

        public static float ScrollSpeed = 3.0f;
        public static float CurrentSec = 0f;
        public static float CurrentBeat = 0f;

        //判定されていないノーツ
        public static List<NoteControllerBase> AliveNoteControllers;

        public static ScoreManager scoreManager { get; set; }
        private static readonly float startOffset = 5.0f;

        private static bool gameEnd;

        private void Awake()
        {
            CurrentSec = 0f;
            CurrentBeat = 0f;

            AliveNoteControllers = new List<NoteControllerBase>();

            foreach (var _noteProperty in scoreManager.noteProperties)
            {
                GameObject objNote = null;
                switch (_noteProperty.noteType)
                {
                    case NoteType.Single:
                        objNote = Instantiate(ObjSingleNote, gameZone.transform);
                        break;
                    case NoteType.Long:
                        objNote = Instantiate(ObjLongNote, gameZone.transform);
                        break;
                    case NoteType.Flick:
                        objNote = Instantiate(ObjFlickNote, gameZone.transform);
                        break;
                }
                AliveNoteControllers.Add(objNote.GetComponent<NoteControllerBase>());
                objNote.GetComponent<NoteControllerBase>().noteProperty = _noteProperty;
            }
            StartCoroutine(AudioManager.instance.PlayBGM("Barduckman",
    startOffset + scoreManager.audioOffset));
        }

        private void Update()
        {
            CurrentSec = Time.timeSinceLevelLoad -startOffset;
            CurrentBeat = ScoreManager.ToBeat(CurrentSec, scoreManager.tempo);
            //if(ノーツが全部流れたら)
            //ちょっと待って
            //パネルを出して
            //結果を表示
            //タップしたら終了
            //メニューに戻る
            if (AliveNoteControllers.Count == 0)
            {
                StartCoroutine(GameUI.instance.ShowResult());
            }
        }

    }
}

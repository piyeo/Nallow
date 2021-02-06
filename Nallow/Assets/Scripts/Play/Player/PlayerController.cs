using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public static string playingBGM;

        //判定されていないノーツ
        public static List<NoteControllerBase> AliveNoteControllers;

        public static ScoreManager scoreManager { get; set; }
        private static readonly float startOffset = 5.0f;

        private static bool gameEnd;

        private void Awake()
        {
            CurrentSec = 0f;
            CurrentBeat = 0f;

            if(GameManager.handValue == HandEnum.Left)
            {
                var position = gameZone.transform.localPosition;
                position.x *= -1;
                gameZone.transform.localPosition = position;
                var rotation = gameZone.transform.localEulerAngles;
                rotation.z *= -1;
                gameZone.transform.localEulerAngles = rotation;
            }

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
            StartCoroutine(AudioManager.instance.PlayBGM(playingBGM,
    startOffset + scoreManager.audioOffset));
        }

        private void Update()
        {
            if (!gameEnd)
            {
                CurrentSec = Time.timeSinceLevelLoad - startOffset;
                CurrentBeat = ScoreManager.ToBeat(CurrentSec, scoreManager.tempo);
                if (AliveNoteControllers.Count == 0)
                {
                    gameEnd = true;
                    StartCoroutine(GameUI.instance.ShowResult());
                }
                return;
            }

            if (Input.GetMouseButton(0))
            {
                gameEnd = false;
                AudioManager.instance.StopMusic();
                SceneManager.LoadScene("MenuScene");
            }
        }

    }
}

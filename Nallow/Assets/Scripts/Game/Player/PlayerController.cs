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
        [SerializeField] AudioSource audioSource;

        public static float ScrollSpeed = 3.0f;
        public static float CurrentSec = 0f;
        public static float CurrentBeat = 0f;

        //判定されていないノーツ
        public static List<NoteControllerBase> AliveNoteControllers;

        public static ScoreManager scoreManager { get; set; }
        private static readonly float startOffset = 5.0f;

        [SerializeField] private AudioManager audioManager;

        private void Awake()
        {
            CurrentSec = 0f;
            CurrentBeat = 0f;

            AliveNoteControllers = new List<NoteControllerBase>();

            if(AudioManager.instance == null)
            {
                Instantiate(audioManager);
            }

            var scoreDirectory = Application.streamingAssetsPath + "/Scores";
            scoreManager = new ScoreManager(scoreDirectory + "/Barduckman_NORMAL.sus");

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
            CurrentSec = Time.time - startOffset;
            CurrentBeat = ScoreManager.ToBeat(CurrentSec, scoreManager.tempo);
        }

    }
}

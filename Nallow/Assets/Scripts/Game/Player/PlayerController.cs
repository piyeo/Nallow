using System.Collections.Generic;
using UnityEngine;
using Note;
using static Note.NoteProperty;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private GameObject prefabSingleNote;
        [SerializeField] private GameObject prefabLongNote;
        [SerializeField] private GameObject prefabFlickNote;
        [SerializeField] private GameObject gameZone;

        public static float ScrollSpeed = 3.0f;
        public static float CurrentSec = 0f;
        public static float CurrentBeat = 0f;

        //判定されていないノーツ
        public static List<NoteControllerBase> ExistingNoteControllers;

        public static Beatmap beatmap { get; set; }
        private static readonly float startOffset = 5.0f;

        private void Awake()
        {
            CurrentSec = 0f;
            CurrentBeat = 0f;

            ExistingNoteControllers = new List<NoteControllerBase>();

            var beatmapDirectory = Application.streamingAssetsPath + "/Beatmaps";
            beatmap = new Beatmap(beatmapDirectory + "/Barduckman_NORMAL.sus");

            foreach (var _noteProperty in beatmap.noteProperties)
            {
                GameObject objNote = null;
                switch (_noteProperty.noteType)
                {
                    case NoteType.Single:
                        objNote = Instantiate(prefabSingleNote, gameZone.transform);
                        break;
                    case NoteType.Long:
                        objNote = Instantiate(prefabLongNote, gameZone.transform);
                        break;
                    case NoteType.Flick:
                        objNote = Instantiate(prefabFlickNote, gameZone.transform);
                        break;
                }
                ExistingNoteControllers.Add(objNote.GetComponent<NoteControllerBase>());
                objNote.GetComponent<NoteControllerBase>().noteProperty = _noteProperty;
            }
        }

        private void Update()
        {
            CurrentSec = Time.time - startOffset;
            CurrentBeat = Beatmap.ToBeat(CurrentSec, beatmap.tempo);
        }

    }
}

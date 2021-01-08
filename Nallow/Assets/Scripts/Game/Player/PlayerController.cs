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

        public static readonly float ScrollSpeed = 1.0f;
        public static float CurrentSec = 0f;
        public static float CurrentBeat = 0f;

        public static Beatmap beatmap { get; set; }
        private static readonly float startOffset = 1.0f;

        private void Awake()
        {
            CurrentSec = 0f;
            CurrentBeat = 0f;

            beatmap = new Beatmap();

            beatmap.noteProperties = new List<NoteProperty>
            {
                new NoteProperty(1, 1, 0, NoteType.Single),
                new NoteProperty(2, 2, 1, NoteType.Single),
                new NoteProperty(4, 4, 0, NoteType.Single),
                new NoteProperty(6, 9, 0, NoteType.Long),
                new NoteProperty(11, 11, 1, NoteType.Flick)
            };

            beatmap.tempo = 60f;

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

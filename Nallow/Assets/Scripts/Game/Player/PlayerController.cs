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
                new NoteProperty(4, 4, 1, NoteType.Single)
            };

            beatmap.tempo = 60f;

            foreach (var noteProperty in beatmap.noteProperties)
            {
                var objNote = Instantiate(prefabSingleNote,gameZone.transform);
                objNote.GetComponent<NoteControllerBase>().noteProperty = noteProperty;
            }
        }

        private void Update()
        {
            CurrentSec = Time.time - startOffset;
            CurrentBeat = Beatmap.ToBeat(CurrentSec, beatmap.tempo);
        }

    }
}

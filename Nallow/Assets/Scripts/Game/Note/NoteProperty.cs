namespace Note
{
    public class NoteProperty
    {
        public float beatBegin { get; }
        public float beatEnd { get; }
        public float secBegin { get; set; }
        public float secEnd { get; set; }
        public int lane { get; }
        public NoteType noteType { get; }

        public NoteProperty(float beatBegin, float beatEnd, int lane, NoteType noteType)
        {
            this.beatBegin = beatBegin;
            this.beatEnd = beatEnd;
            this.lane = lane;
            this.noteType = noteType;
        }

        public enum NoteType
        {
            Single,
            Long,
            Flick
        }
    }
}

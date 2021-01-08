using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Note
{
    public class Beatmap
    {
        public List<NoteProperty> noteProperties = new List<NoteProperty>();
        public float tempo { get; }

        //susファイルを読み込む
        public Beatmap(string filepath)
        {
            var beatmapLoader = new BeatmapLoader(filepath);
            noteProperties = beatmapLoader.noteProperties;
            tempo = beatmapLoader.tempo;
        }

        public static float ToSec(float beat, float _tempo)
        {
            // 60f : 一分間の秒数
            return beat / (_tempo / 60f);
        }

        public static float ToBeat(float sec, float _tempo)
        {
            // 60f : 一分間の秒数
            return sec * (_tempo / 60f);
        }
    }
}

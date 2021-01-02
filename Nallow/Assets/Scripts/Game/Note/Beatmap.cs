using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Note
{
    public class Beatmap
    {
        public List<NoteProperty> noteProperties = new List<NoteProperty>();
        public float tempo { get; set; }
        //private List<TempoChange> TempoChanges = new List<TempoChange>();

        public static float ToSec(float beat, float tempo)
        {
            // 60f : 一分間の秒数
            return beat / (tempo / 60f);
        }

        public static float ToBeat(float sec, float tempo)
        {
            // 60f : 一分間の秒数
            return sec * (tempo / 60f);
        }

        //public static float ToTotalSec(float beat, List<TempoChange> tempoChanges)
        //{
        //    float totalSec = 0f;
        //    // i: テンポ変化番号
        //    int i = 0;
        //    // n: 変換するbeatの直前までのテンポ変化の回数
        //    var n = tempoChanges.Count(x => (x.beat <= beat));
        //    // 変換するbeatの直前にあるテンポ変化までのsecを求める
        //    while (i < n - 1)
        //    {
        //        totalSec += ToSec(tempoChanges[i+1].beat - tempoChanges[i].beat, tempoChanges[i].tempo);
        //        i++;
        //    }
        //    // 残りのbeat分を足す
        //    totalSec += ToSec(beat - tempoChanges[i].beat, tempoChanges[i].tempo);
        //    return totalSec;
        //}

        //// テンポ変化情報を基に、secをbeatへ変換する
        //public static float ToTotalBeat(float sec, List<TempoChange> tempoChanges)
        //{
        //    float totalSec = 0;
        //    // i: テンポ変化番号
        //    int i = 0;
        //    // n: 全てのテンポ変化の回数
        //    var n = tempoChanges.Count;
        //    // 最後から1つ前のテンポ変化までループ
        //    while (i < n - 1)
        //    {
        //        // tmpSec: i回目のテンポ変化地点での秒数
        //        var tmpSec = totalSec;
        //        // 次(i+1回目)のテンポ変化タイミング(秒)を計算する
        //        totalSec += ToSec(tempoChanges[i+1].beat - tempoChanges[i].beat, tempoChanges[i].tempo);
        //        if (totalSec >= sec)
        //        {
        //            // 次のテンポ変化タイミングが変換するsecを超えた場合、
        //            //「超える直前のテンポ変化があるbeat + 残りのbeat」を返す
        //            return tempoChanges[i].beat + ToBeat(sec - tmpSec, tempoChanges[i].tempo);
        //        }
        //        i++;
        //    }
        //    // 変換するsecが最後のテンポ変化よりも後にある時、
        //    //「最後のテンポ変化があるbeat + 残りのbeat」を返す
        //    return tempoChanges[n - 1].beat + ToBeat(sec - totalSec, tempoChanges[n - 1].tempo
        //    );
        //}
    }
}

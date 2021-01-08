using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Note;

public class BeatmapLoader
{
    private static string MainDataPattern = @"#([0-9]{3})([0-9A-Z]{2})(.*): (.*)";

    private static List<string> HeaderPatterns = new List<string>
    {
        @"#(TITLE) (.*)",
        @"#(ARTIST) (.*)",
        @"#(PLAYLEVEL) (.*)",
        @"#(BPM01):(.*)"
    };

    //sus上のレーン番号とゲームのレーン番号の対応
    private static Dictionary<char, int> LanePairs = new Dictionary<char, int>
    {
        {'0', 0 },
        {'8', 1 }
    };

    public Dictionary<string, string> headerData = new Dictionary<string, string>();
    public List<NoteProperty> noteProperties = new List<NoteProperty>();
    public int tempo { get; }

    // 各小節の長さ(beat単位、4で4分の4拍子)
    private float[] measureLengths = Enumerable.Repeat(4f, 1000).ToArray();

    //ロングノーツの開始ノーツ
    private float longNoteBegin = 0;


    public BeatmapLoader(string filePath)
    {
        var lines = File.ReadAllLines(filePath, Encoding.UTF8);

        foreach(var line in lines)
        {
            LoadHeaderLine(line);
        }

        tempo = Convert.ToInt32(headerData["BPM01"]);

        foreach (var line in lines)
        {
            LoadMainDataLine(line);
        }
    }


    private void LoadHeaderLine(string line)
    {
        foreach(var headerPattern in HeaderPatterns)
        {
            Match match = Regex.Match(line, headerPattern);
            if (match.Success)
            {
                var headerName = match.Groups[1].Value;
                var data = match.Groups[2].Value;
                headerData[headerName] = data;
                return;
            }
        }
    }

    private void LoadMainDataLine(string line)
    {
        if (line.StartsWith("#0000")) { return; }
        var match = Regex.Match(line, MainDataPattern);
        if(match.Success)
        {
            //小節番号
            int measureNum = Convert.ToInt32(match.Groups[1].Value);
            //レーン・ノーツの種類番号
            string type = match.Groups[2].Value;
            //データ本体
            string body = match.Groups[4].Value;
            //データ種別
            DataType dataType = GetDataType(type);

            if (dataType == DataType.Unsupported) { return; }

            //小節の開始beat
            float measureStartBeat = measureLengths.Take(measureNum).Sum();
            //分割数(例：00 18 00 18 は 4)
            int objCount = body.Length / 2;

            //データ本体を2桁ごとに区切って処理
            for(int i = 0; i < body.Length / 2; i++)
            {
                //オブジェクト番号
                string objNum = body.Substring(i * 2, 2);

                if(objNum == "00"){ continue; }

                float beat = measureStartBeat +
                    (i * measureLengths[measureNum] / objCount);

                int lane = LanePairs[type[1]];
                switch (dataType)
                {
                    case DataType.SingleNote:
                        if(objNum[0] == '1')
                        {
                            noteProperties.Add(
                                new NoteProperty(beat, beat, lane, NoteProperty.NoteType.Single)
                            );
                        }
                        else if(objNum[0] == '3')
                        {
                            noteProperties.Add(
                                new NoteProperty(beat, beat, lane, NoteProperty.NoteType.Flick)
                            );
                        }
                        break;

                    case DataType.LongNote:
                        if(objNum[0] == '1')
                        {
                            longNoteBegin = beat;
                        }
                        else if(objNum[0] == '2')
                        {
                            noteProperties.Add(
                                new NoteProperty(
                                    longNoteBegin, beat, lane, NoteProperty.NoteType.Long)
                            );
                        }
                        break;
                }
            }
        }
    }

    private DataType GetDataType(string type)
    {
        if (type[0] == '1')
        {
            return DataType.SingleNote;
        }
        else if (type[0] == '2')
        {
            return DataType.LongNote;
        }
        else
        {
            return DataType.Unsupported;
        }
    }

}

public enum DataType
{
    Unsupported,
    SingleNote,
    LongNote
}

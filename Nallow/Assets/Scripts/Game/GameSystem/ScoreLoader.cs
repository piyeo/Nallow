using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Note;
using UnityEngine;

public class ScoreLoader
{
    private static string ScoreDataRegax = @"#([0-9]{3})([0-9A-Z]{2})(.*): (.*)";

    private static List<string> MetaDataRegax = new List<string>
    {
        @"#(TITLE) (.*)",
        @"#(ARTIST) (.*)",
        @"#(PLAYLEVEL) (.*)",
        @"#(WAVE) (.*)",
        @"#(WAVEOFFSET) (.*)",
        @"#(BPM01):(.*)"
    };

    public Dictionary<string, string> MetaData = new Dictionary<string, string>();
    public List<NoteProperty> noteProperties = new List<NoteProperty>();
    public int tempo { get; }
    public float audioOffset { get; set; }

    private float[] measureLengths = Enumerable.Repeat(4f, 1000).ToArray();

    private float longNoteBegin = 0;


    public ScoreLoader(string filePath)
    {
        var lines = File.ReadAllLines(filePath, Encoding.UTF8);

        foreach(var line in lines)
        {
            LoadHeaderLine(line);
        }

        tempo = Convert.ToInt32(MetaData["BPM01"]);
        audioOffset = Convert.ToSingle(MetaData["WAVEOFFSET"]);

        foreach (var line in lines)
        {
            LoadMainDataLine(line);
        }

        foreach(var noteProperty in noteProperties)
        {
            noteProperty.secBegin = ScoreManager.ToSec(noteProperty.beatBegin, tempo);
            noteProperty.secEnd = ScoreManager.ToSec(noteProperty.beatEnd, tempo);
        }
    }


    private void LoadHeaderLine(string line)
    {
        foreach(var headerPattern in MetaDataRegax)
        {
            Match match = Regex.Match(line, headerPattern);
            if (match.Success)
            {
                var headerName = match.Groups[1].Value;
                var data = match.Groups[2].Value;
                MetaData[headerName] = data.Replace("\"", "");
                return;
            }
        }
    }

    private void LoadMainDataLine(string line)
    {
        if (line.StartsWith("#0000")) { return; }
        var match = Regex.Match(line, ScoreDataRegax);
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

                float beat = measureStartBeat + i;

                int lane = 0;
                if (type[1] == '8') { lane = 1; }

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

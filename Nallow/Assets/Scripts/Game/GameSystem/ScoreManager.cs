using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Note;

public class ScoreManager
{
    public List<NoteProperty> noteProperties = new List<NoteProperty>();
    public float tempo { get; }
    public string audioFilePath = "";
    public float audioOffset { get; }

    //susファイルを読み込む
    public ScoreManager(string filePath)
    {
        var scoreDirectory = new FileInfo(filePath).DirectoryName;

        var scoreLoader = new ScoreLoader(filePath);
        noteProperties = scoreLoader.noteProperties;
        tempo = scoreLoader.tempo;

        if (scoreLoader.headerData.ContainsKey("WAVE"))
        {
            audioFilePath = scoreDirectory + "/BGM/" + scoreLoader.headerData["WAVE"];
        }
        audioOffset = scoreLoader.audioOffset;
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

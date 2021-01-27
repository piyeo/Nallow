using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuContent
{
    public static Dictionary<int, string> ModeIds = new Dictionary<int, string>
    {
        { 0, "Single Play" },
        { 1, "Multi Play" },
        { 2, "Setting" }
    };
    public static Dictionary<int, string> DifficultyIds = new Dictionary<int, string>
    {
        { 0, "Easy" },
        { 1, "Hard" },
        { 2, "Expert" }
    };

    public string[] scorePaths;
    public List<ScoreLoader> scoreLoaders;

    public MenuContent()
    {
        var scoreDirectory = Application.streamingAssetsPath + "/Scores";
        scorePaths = Directory.GetFiles(scoreDirectory, "*.sus", SearchOption.AllDirectories);
        scoreLoaders = scorePaths.Select(path => new ScoreLoader(path)).ToList();
    }

}

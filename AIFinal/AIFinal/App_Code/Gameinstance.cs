using AIFinal.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Main Data structure for Goal Agent holds everything
/// </summary>
public class Gameinstance
{
    public string gameID;
    public string RecordingFilePath;
    public GameEvents metaData;
    public Emotion[] emote;
    public metaDataGen meta;

    public Gameinstance(string GameId, string RecordingFilePath, GameEvents jsonGameData, Emotion e, metaDataGen meta)
    {
        
    }
}
using AIFinal.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Main Data structure for Goal Agent
/// </summary>
public class Gameinstance
{
    public string gameID;
    public string RecordingFilePath;
    public GameEvents metaData;

    public Gameinstance(string GameId, string RecordingFilePath, GameEvents jsonGameData)
    {
        
    }
}
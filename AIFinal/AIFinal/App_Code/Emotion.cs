using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Emotion
/// </summary>
public class Emotion
{
    public string emotionDetected { get; set; }
    public int intensity { get; set; }
    public int delta { get; set; }
    public int unixStart { get; set; }
    public int unixEnd { get; set; }
    public string AudioLocation;
    public string[] ArrayWords;

    public Emotion()
    {
        
    }
    
}
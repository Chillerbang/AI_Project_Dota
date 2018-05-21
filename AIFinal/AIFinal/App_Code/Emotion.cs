using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// EmotionClass for simple descions used by reflexAgent
/// </summary>
public class Emotion
{
    public string Mp3ParentName;
    public string emotionDetected { get; set; }
    public double intensity { get; set; }
    public double delta { get; set; }
    public string[] ArrayWords;

    public Emotion()
    {
        
    }
    
}
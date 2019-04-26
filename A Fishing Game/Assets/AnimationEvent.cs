using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject fishingLine;
    
    public void AniEvent()
    {
        FishingLine line = fishingLine.GetComponent<FishingLine>();
        line.throwStarted = true;
    }
}

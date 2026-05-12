using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DefaultFishSpecies", menuName = "ScriptableObjects/Fish", order = 1)]
public class BaseFishSpecies : ScriptableObject
{
    public string fishName;
    public int maxTemp;
    public int minTemp;

    public int maxDecibels;
    
    public bool hideFromFace;

    public float fleeSpeed;
    [Range(0.75f, 1f)]
    public float movementSpeed = 0.75f;

    public float hideTime;
    public float sunbatheTime;

    public bool doesSunbathe;

    public int startingHour;
    public int endingHour;

    public int startingMonth;
    public int endingMonth;

    [System.Serializable]
    public struct SwimHeights
    {
        public bool low;
        public bool medium;
        public bool high;
    }

    public SwimHeights swimHeights;
}

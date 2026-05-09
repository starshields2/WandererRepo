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
    public int minDecibels;
    public bool hideFromFace;

    public float fleeSpeed;
    public float movementSpeed;

    public float hideTime;
    public float sunbatheTime;

    public bool doesSunbathe;

}

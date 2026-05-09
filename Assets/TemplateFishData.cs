using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateFishData : MonoBehaviour
{

    public BaseFishSpecies baseFishSpecies;
    public enum FishState
    {
        Idle,
        RegularSwim,
        Run,
        Hide,
        Follow,
        Sunbathe
    }

    public FishState currentState = FishState.Hide;
    [SerializeField] private Transform[] _targets;
    public Transform hidingSpot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

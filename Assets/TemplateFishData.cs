using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AILerp))]

public class TemplateFishData : MonoBehaviour
{
    
    public BaseFishSpecies baseFishSpecies;
    
    [Header("Managers")]
    
    public WeatherManager _weatherManager;
    public TimeAndDate _timeManager;
  
    [Header("Dependencies")]
    
    public Transform hidingSpot;
    public Transform sunSpot;
    public AIDestinationSetter _destinationSetter;
    private bool _isMovingAlongCurve = false;
    public Transform _objectToMove;
    private Coroutine _moveCoroutine;
    private Coroutine _hideCoroutine;

    [SerializeField] private float _localHideTime;
    [SerializeField] private float _localSunbatheTime;
    [SerializeField] private float _fleeSpeed;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private GameObject[] _Visuals;

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

    public bool cameraShy;

    [SerializeField] private List<Transform> _targets = new List<Transform>();

    


    // Start is called before the first frame update
    void Awake()
    {
        GetSwimSpots();
        SetPreferences();
    }

    private void SetPreferences()
    {
        _objectToMove = this.gameObject.transform;
        _destinationSetter = this.gameObject.GetComponent<AIDestinationSetter>();
        _localHideTime = baseFishSpecies.hideTime;
        _localSunbatheTime = baseFishSpecies.sunbatheTime;
        _fleeSpeed = baseFishSpecies.fleeSpeed;
        _movementSpeed = baseFishSpecies.movementSpeed;
        cameraShy = baseFishSpecies.hideFromFace;

    }

    private void LateUpdate()
    {
        switch (currentState)
        {
            case FishState.Idle:
                StopMovingAlongCurve();
                break;

            case FishState.RegularSwim:
                if (!_isMovingAlongCurve)
                {
                    Debug.Log("Starting RegularSwim movement.");
                    //StartMovingAlongCurve();
                    DefaultDestinationSetting();
                }
                break;

            case FishState.Run:
                // Can add special logic here if needed
                break;

            case FishState.Hide:
                if (_isMovingAlongCurve && _hideCoroutine == null)
                {
                    //StopMovingAlongCurve();
                    _isMovingAlongCurve = false;
                    BeginHiding();
                }
                break;

            case FishState.Sunbathe:
                if (!_isMovingAlongCurve && _hideCoroutine == null)
                {
                    StopMovingAlongCurve();
                    _hideCoroutine = StartCoroutine(SunbatheSequence());
                }
                break;

            case FishState.Follow:
                if (!_isMovingAlongCurve && _hideCoroutine == null)
                {
                    StopMovingAlongCurve();
                   // _hideCoroutine = StartCoroutine(FollowSequence());
                }
                break;
        }
    }

    public void CheckTempAndTime()
    {
        if(_weatherManager.localTemp < baseFishSpecies.minTemp || _weatherManager.localTemp > baseFishSpecies.maxTemp)
        {
            BeginHiding();
        }

        if (_timeManager.timeTick > baseFishSpecies.startingHour && _timeManager.timeTick < baseFishSpecies.endingHour)
        {
            currentState = FishState.Idle;
            foreach(GameObject item in _Visuals)
            {
                item.SetActive(false);
            }
        }

        if(_timeManager.currentMonth >= baseFishSpecies.startingMonth && _timeManager.currentMonth <= baseFishSpecies.endingMonth)
        {
            currentState = FishState.Idle;
            foreach (GameObject item in _Visuals)
            {
                item.SetActive(false);
            }
        }
    }

    public void GetSwimSpots()
    {
        GameObject swimSpotsFinder = GameObject.Find("SwimSpots");
        SwimSpotsCollection swimSpotsAdder = swimSpotsFinder.GetComponent<SwimSpotsCollection>();

        if (baseFishSpecies.swimHeights.low == true)
        {
            foreach (Transform spot in swimSpotsAdder.lowSpots)
            {
                _targets.Add(spot);
            }
        }
        if (baseFishSpecies.swimHeights.medium == true)
        {
            foreach (Transform spot in swimSpotsAdder.mediumSpots)
            {
                _targets.Add(spot);
            }
        }
        if (baseFishSpecies.swimHeights.high == true)
        {
            foreach (Transform spot in swimSpotsAdder.highSpots)
            {
                _targets.Add(spot);
            }
        }
    }


    private void DefaultDestinationSetting()
    {
        StartCoroutine(DefaultSwimCoroutine());
    }

    private IEnumerator DefaultSwimCoroutine()
    {
        _isMovingAlongCurve = true;

        // Start with a random target
        int currentIndex = Random.Range(0, _targets.Count);
        _destinationSetter.target = _targets[currentIndex];

        while (_isMovingAlongCurve)
        {
            // When close enough to the current target...
            if (Vector3.Distance(_objectToMove.position, _destinationSetter.target.position) < 0.2f)
            {
                int newIndex;

                // Pick a new random index that is NOT the same as the current one
                do
                {
                    newIndex = Random.Range(0, _targets.Count);
                }
                while (newIndex == currentIndex);

                currentIndex = newIndex;
                _destinationSetter.target = _targets[currentIndex];
            }

            yield return null; // keep coroutine alive frame-by-frame
        }
    }

    private void StopMovingAlongCurve()
    {
        if (_isMovingAlongCurve)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
            _isMovingAlongCurve = false;
        }
    }

    private void BeginHiding()
    {
        StartCoroutine(HidingCoroutine());
    }

    private IEnumerator HidingCoroutine()
    {
        Debug.Log("Hide THIS FISH!");
        _isMovingAlongCurve = false;
        _destinationSetter.target = hidingSpot;
        yield return new WaitForSeconds(_localHideTime);
        currentState = FishState.RegularSwim;
    }

    private void BeginSunbathing()
    {
        StartCoroutine(SunbatheSequence());
    }

    private IEnumerator SunbatheSequence()
    {
        Debug.Log("Hide THIS FISH!");
        _isMovingAlongCurve = false;
        _destinationSetter.target = sunSpot;
        yield return new WaitForSeconds(_localSunbatheTime);
        currentState = FishState.RegularSwim;
    }
}

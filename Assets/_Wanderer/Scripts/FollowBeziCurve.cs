using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

public class FollowBeziCurve : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] public float _hideTime = 2f;
    [SerializeField] public float _sunbatheTime = 20f;
    [SerializeField] public AIDestinationSetter _destinationSetter;
    public float fleeSpeed = 5f;

    public Transform hideSpot;
    public Transform sunSpot;
    public Transform _fishFollowing;
    private Coroutine _moveCoroutine;
    private Coroutine _hideCoroutine;
    public bool _isMovingAlongCurve = false;
    public bool beingChased = false;

    public Vector3[] _points;

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

    private void Start()
    {
        if (_objectToMove == null)
        {
            Debug.LogError("Object to move is not assigned!");
            return;
        }

        //GenerateCurve();
        _destinationSetter = this.gameObject.GetComponent<AIDestinationSetter>();
    }

    private void DefaultDestinationSetting()
    {
        StartCoroutine(DefaultSwimCoroutine());
    }

    private IEnumerator DefaultSwimCoroutine()
    {
        _isMovingAlongCurve = true;

        // Start with a random target
        int currentIndex = Random.Range(0, _targets.Length);
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
                    newIndex = Random.Range(0, _targets.Length);
                }
                while (newIndex == currentIndex);

                currentIndex = newIndex;
                _destinationSetter.target = _targets[currentIndex];
            }

            yield return null; // keep coroutine alive frame-by-frame
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
        _destinationSetter.target = hideSpot;
        yield return new WaitForSeconds(_hideTime);
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
        yield return new WaitForSeconds(_sunbatheTime);
        currentState = FishState.RegularSwim;
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
                    _hideCoroutine = StartCoroutine(FollowSequence());
                }
                break;
        }
    }

    private void StartMovingAlongCurve()
    {
        if (_points == null || _points.Length == 0)
        {
            Debug.LogError("No curve points available!");
            return;
        }

        _isMovingAlongCurve = true;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(MoveObjectAlongCurve());
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

    public IEnumerator RunSequence()
    {
        StopMovingAlongCurve();

        if (hideSpot != null)
        {
            while (Vector3.Distance(transform.position, hideSpot.position) > 0.1f)
            {
                transform.LookAt(hideSpot);
                transform.position = Vector3.MoveTowards(transform.position, hideSpot.position, fleeSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(_hideTime);
            currentState = FishState.RegularSwim;
        }
        else
        {
            Debug.LogError("Hide spot not assigned!");
        }

        _hideCoroutine = null;
    }

    public IEnumerator SunbatheSequenceDepreciated()
    {
        StopMovingAlongCurve();

        if (sunSpot != null)
        {
            while (Vector3.Distance(transform.position, sunSpot.position) > 0.1f)
            {
                transform.LookAt(sunSpot);
                transform.position = Vector3.MoveTowards(transform.position, sunSpot.position, fleeSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(_sunbatheTime);
            currentState = FishState.RegularSwim;
        }
        else
        {
            Debug.LogError("Sun spot not assigned!");
        }

        _hideCoroutine = null;
    }

    public IEnumerator FollowSequence()
    {
        StopMovingAlongCurve();

        if (_fishFollowing != null)
        {
            while (currentState == FishState.Follow)
            {
                Vector3 targetPos = _fishFollowing.position - _fishFollowing.forward * 3f;
                Vector3 direction = (targetPos - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);

                transform.position = Vector3.MoveTowards(transform.position, targetPos, _chaseSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Fish to follow not assigned!");
        }

        _hideCoroutine = null;
    }

    public IEnumerator MoveObjectAlongCurve()
    {
        while (true) // Infinite loop until stopped
        {
            for (int currentIndex = 0; currentIndex < _points.Length; currentIndex++)
            {
                if (!_isMovingAlongCurve)
                    yield break;

                Vector3 startPoint = _objectToMove.position;
                Vector3 targetPosition = _points[currentIndex];
                float journeyLength = Vector3.Distance(startPoint, targetPosition);
                float startTime = Time.time;

                while (Vector3.Distance(_objectToMove.position, targetPosition) > 0.01f)
                {
                    if (!_isMovingAlongCurve)
                        yield break;

                    float speedThisFrame = beingChased ? _chaseSpeed : _movementSpeed;
                    transform.LookAt(targetPosition);
                    float distanceCovered = (Time.time - startTime) * speedThisFrame;
                    float fractionOfJourney = distanceCovered / journeyLength;

                    _objectToMove.position = Vector3.Lerp(startPoint, targetPosition, fractionOfJourney);
                    yield return null;
                }

                _objectToMove.position = targetPosition;
            }
        }
    }

    private void GenerateCurve()
    {
        if (_targets.Length < 3)
        {
            Debug.LogError("Not enough targets to generate curve!");
            return;
        }

        _points = GetWholePath(_targets.Select(p => p.position).ToArray());
        _lineRenderer.positionCount = _points.Length;
        _lineRenderer.SetPositions(_points);
    }

    private Vector3[] GetWholePath(Vector3[] positions)
    {
        List<Vector3> targetPoints = new List<Vector3>();
        for (int i = 0; i < positions.Length - 1; i++)
        {
            Vector3 first = positions[i];
            Vector3 second = positions[i + 1];
            Vector3 mid = first + (second - first) / 2f;
            targetPoints.Add(first);
            targetPoints.Add(mid);
        }
        targetPoints.Add(positions.Last());

        int numBeziers = positions.Length - 2;
        List<Vector3> points = new List<Vector3>();
        points.AddRange(GetPointsLinear(targetPoints[0], targetPoints[1]));

        for (int i = 1; i <= numBeziers; i++)
        {
            int idx = i * 2 - 1;
            points.AddRange(GetPointsBezier(targetPoints[idx], targetPoints[idx + 1], targetPoints[idx + 2]));
        }

        points.AddRange(GetPointsLinear(targetPoints[^2], targetPoints[^1]));
        return points.ToArray();
    }

    private Vector3[] GetPointsLinear(Vector3 pt1, Vector3 pt2, int segments = 10)
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i <= segments; i++)
            points[i] = Vector3.Lerp(pt1, pt2, i / (float)segments);
        return points;
    }

    private Vector3[] GetPointsBezier(Vector3 pt1, Vector3 pt2, Vector3 pt3, int segments = 10)
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            points[i] = Mathf.Pow(1 - t, 2) * pt1 + 2 * (1 - t) * t * pt2 + Mathf.Pow(t, 2) * pt3;
        }
        return points;
    }
}

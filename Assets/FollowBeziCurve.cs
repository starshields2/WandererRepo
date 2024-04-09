using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.ARSubsystems;
using TasiYokan.Curve;

public class FollowBeziCurve : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private float _movementSpeed = 2f;

    public Transform hideSpot; // Changed GameObject to Transform
    private Coroutine _moveCoroutine;
    private bool _isMovingAlongCurve = false;

    private Vector3[] _points;
    public enum FishState
    {
        Idle,
        RegularSwim,
        Run,
        Hide
    }
    public FishState currentState = FishState.RegularSwim;

    private void Start()
    {
        if (_objectToMove == null)
        {
            Debug.LogError("Object to move is not assigned!");
            return;
        }

        GenerateCurve();
        currentState = FishState.RegularSwim;

        // Start moving the object along the curve
        if (currentState == FishState.RegularSwim)
            StartMovingAlongCurve();
    }

    private void Update()
    {
        switch (currentState)
        {
            case FishState.Idle:
                break;
            case FishState.RegularSwim:
                // Check if the coroutine is already running before starting a new one
                if (!_isMovingAlongCurve)
                    StartMovingAlongCurve();
                break;
            case FishState.Run:
                // Stop the coroutine if it's running
                if (_isMovingAlongCurve)
                    StopMovingAlongCurve();
                StartCoroutine(RunSequence());
                break;
            case FishState.Hide:
                if (_isMovingAlongCurve)
                    StopMovingAlongCurve();
                StartCoroutine(RunSequence());
              
                break;
            default:
                break;
        }
    }

    private void StartMovingAlongCurve()
    {
        // Check if _points are available and not empty
        if (_points == null || _points.Length == 0)
        {
            Debug.LogError("No curve points available!");
            return;
        }

        _isMovingAlongCurve = true;
        _moveCoroutine = StartCoroutine(MoveObjectAlongCurve());
    }

    private void StopMovingAlongCurve()
    {
        _isMovingAlongCurve = false;
        StopCoroutine(_moveCoroutine);
    }

    public IEnumerator RunSequence()
    {
        if (hideSpot != null) // Check if hideSpot is assigned
        {
            while (Vector3.Distance(transform.position, hideSpot.position) > 0.1f)
            {
                transform.LookAt(hideSpot);
                float speed = 1.5f;
                transform.position = Vector3.MoveTowards(transform.position, hideSpot.position, speed * Time.deltaTime);
                yield return null;
            }

            // If the fish has reached the hide spot, change its state
            yield return new WaitForSeconds(5f);
            currentState = FishState.RegularSwim;
        }
        else
        {
            Debug.LogError("Hide spot is not assigned!");
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
        for (var index = 0; index < positions.Length - 1; index++)
        {
            Vector3 firstPosition = positions[index];
            Vector3 secondPosition = positions[index + 1];
            Vector3 intermediate = firstPosition + (secondPosition - firstPosition) / 2f;
            targetPoints.Add(firstPosition);
            targetPoints.Add(intermediate);
        }
        targetPoints.Add(positions.Last());

        int numBeziers = positions.Length - 2;
        List<Vector3> points = new List<Vector3>();
        points.AddRange(GetPointsLinear(targetPoints[0], targetPoints[1]));
        for (int i = 1; i <= numBeziers; i++)
        {
            int startIndex = i * 2 - 1;
            points.AddRange(GetPointsBezier(targetPoints[startIndex], targetPoints[startIndex + 1], targetPoints[startIndex + 2]));
        }
        points.AddRange(GetPointsLinear(targetPoints[targetPoints.Count - 2], targetPoints[targetPoints.Count - 1]));
        return points.ToArray();
    }

    private Vector3[] GetPointsLinear(Vector3 pt1, Vector3 pt2, int lineDivisor = 10)
    {
        Vector3[] points = new Vector3[lineDivisor + 1];
        for (int i = 0; i < lineDivisor + 1; i++)
        {
            points[i] = Vector3.Lerp(pt1, pt2, i / (float)lineDivisor);
        }
        return points;
    }

    private Vector3[] GetPointsBezier(Vector3 pt1, Vector3 pt2, Vector3 pt3, int curveDivisor = 10)
    {
        Vector3[] points = new Vector3[curveDivisor + 1];
        for (int i = 0; i < curveDivisor + 1; i++)
        {
            float t = i / (float)curveDivisor;
            points[i] = Mathf.Pow(1 - t, 2) * pt1 + 2 * (1 - t) * t * pt2 + Mathf.Pow(t, 2) * pt3;
        }
        return points;
    }

    private IEnumerator MoveObjectAlongCurve()
    {
        int currentIndex = 0;
        while (true)
        {
            Vector3 targetPosition = _points[currentIndex];

            transform.LookAt(targetPosition);
            while (Vector3.Distance(_objectToMove.position, targetPosition) > 0.01f)
            {
                _objectToMove.position = Vector3.MoveTowards(_objectToMove.position, targetPosition, _movementSpeed * Time.deltaTime);
                yield return null;
            }

            currentIndex = (currentIndex + 1) % _points.Length;
        }
    }
}

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
    [SerializeField] public float _hideTime = 2f;
    [SerializeField] public float _sunbatheTime = 20f;
    public float fleeSpeed = 5f;

    public Transform hideSpot;
    public Transform sunSpot;
    private Coroutine _moveCoroutine;
    private Coroutine _hideCoroutine;
    public bool _isMovingAlongCurve = false;

    public Vector3[] _points;
    public enum FishState
    {
        Idle,
        RegularSwim,
        Run,
        Hide,
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

        GenerateCurve();
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
                    Debug.Log("Not moving along curve. Starting movement.");
                    StartMovingAlongCurve();  // Only start the coroutine once
                }
                break;

            case FishState.Run:
                // Handle run state logic here, if needed
                break;

            case FishState.Hide:
                _isMovingAlongCurve = false;
                // This will stop the curve movement immediately and start the hiding sequence
                if (!_isMovingAlongCurve && _hideCoroutine == null) // Only start hiding if not moving and hiding coroutine is not running
                {
                    StopMovingAlongCurve();  // Immediately stop the curve movement
                    Debug.Log("Start hiding sequence.");
                    _hideCoroutine = StartCoroutine(RunSequence()); // Ensure it only runs once
                }
                break;
            case FishState.Sunbathe:
                _isMovingAlongCurve = false;
                // This will stop the curve movement immediately and start the hiding sequence
                if (!_isMovingAlongCurve && _hideCoroutine == null) // Only start hiding if not moving and hiding coroutine is not running
                {
                    StopMovingAlongCurve();  // Immediately stop the curve movement
                    Debug.Log("Start sunbathe sequence.");
                    _hideCoroutine = StartCoroutine(SunbatheSequence()); // Ensure it only runs once
                }
                break;
            default:
                break;
        }
    }


    private void StartMovingAlongCurve()
    {
        _movementSpeed = 2f;
        StartCoroutine(CurveBuffer());
    }

    public IEnumerator CurveBuffer()
    {
        yield return new WaitForSeconds(1);
        if (_points == null || _points.Length == 0)
        {
            Debug.LogError("No curve points available!");
            yield return null;
        }

        _isMovingAlongCurve = true;
        _moveCoroutine = StartCoroutine(MoveObjectAlongCurve());
    }

    private void StopMovingAlongCurve()
    {
        if (_isMovingAlongCurve)
        {
            Debug.Log("Stopping current movement along curve.");
            _movementSpeed = 0;

            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
            _isMovingAlongCurve = false; // Reset this flag when stopping
        }
    }

    public IEnumerator RunSequence()
    {
        _isMovingAlongCurve = false;
        // Immediately stop any current movement and ensure the curve movement is stopped
        Debug.Log("Stopping movement before hiding...");
        StopMovingAlongCurve(); // This ensures any curve movement is stopped immediately

        // No wait for delay — make it immediate
        if (hideSpot != null) // Check if hideSpot is assigned
        {
            Debug.Log("Starting to move to hide spot...");

            // Move towards the hide spot immediately
            while (Vector3.Distance(transform.position, hideSpot.position) > 0.1f)
            {
                transform.LookAt(hideSpot);
                float speed = fleeSpeed;
                transform.position = Vector3.MoveTowards(transform.position, hideSpot.position, speed * Time.deltaTime);
                yield return null; // Keep moving the object to the hide spot
            }

            // Once close to the hide spot, stop and wait for a bit
            Debug.Log("Done hiding.");
            yield return new WaitForSeconds(_hideTime); // Wait for 5 seconds at the hide spot

            // After waiting, change state to RegularSwim
            currentState = FishState.RegularSwim;

            // Reset the hideCoroutine to allow re-triggering the hiding process
            _hideCoroutine = null;
        }
        else
        {
            Debug.LogError("Hide spot is not assigned!");
            _hideCoroutine = null;
        }
    }

    public IEnumerator SunbatheSequence()
    {
        _isMovingAlongCurve = false;
        // Immediately stop any current movement and ensure the curve movement is stopped
        Debug.Log("Stopping movement before hiding...");
        StopMovingAlongCurve(); // This ensures any curve movement is stopped immediately

        // No wait for delay — make it immediate
        if (sunSpot != null) // Check if hideSpot is assigned
        {
            Debug.Log("Starting to move to SUNBATHE spot...");

            // Move towards the hide spot immediately
            while (Vector3.Distance(transform.position, sunSpot.position) > 0.1f)
            {
                transform.LookAt(hideSpot);
                float speed = fleeSpeed;
                transform.position = Vector3.MoveTowards(transform.position, sunSpot.position, speed * Time.deltaTime);
                yield return null; // Keep moving the object to the hide spot
            }

            // Once close to the hide spot, stop and wait for a bit
            Debug.Log("Done hiding.");
            yield return new WaitForSeconds(_sunbatheTime); // Wait for seconds at the hide spot

            // After waiting, change state to RegularSwim
            currentState = FishState.RegularSwim;

            // Reset the hideCoroutine to allow re-triggering the hiding process
            _hideCoroutine = null;
        }
        else
        {
            Debug.LogError("Hide spot is not assigned!");
            _hideCoroutine = null;
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

    public IEnumerator MoveObjectAlongCurve()
    {
        int currentIndex = 0;

        while (currentIndex < _points.Length)
        {
            // Check if we should stop moving along the curve
            if (!_isMovingAlongCurve)
                yield break; // Stop the coroutine immediately if we are no longer moving along the curve

            // Start at the current point
            Vector3 startPoint = _objectToMove.position;
            Vector3 targetPosition = _points[currentIndex];

            // Interpolate between the start and target position smoothly over time
            float journeyLength = Vector3.Distance(startPoint, targetPosition);
            float startTime = Time.time;
            float distanceCovered = (Time.time - startTime) * _movementSpeed;

            // Move the object smoothly from the start position to the target
            while (distanceCovered < journeyLength)
            {
                if (!_isMovingAlongCurve)
                    yield break; // Break the loop if we're not moving along the curve anymore

                transform.LookAt(targetPosition);
                float fractionOfJourney = distanceCovered / journeyLength;
                _objectToMove.position = Vector3.Lerp(startPoint, targetPosition, fractionOfJourney);
                distanceCovered = (Time.time - startTime) * _movementSpeed;
                yield return null;
            }

            // Ensure the object reaches the exact target position after finishing the loop
            _objectToMove.position = targetPosition;

            // Move to the next point
            currentIndex++;
        }

        // End of the path, handle if needed
        _isMovingAlongCurve = false;
    }

}

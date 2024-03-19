using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FollowBeziCurve : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private float _movementSpeed = 2f;

    private Vector3[] _points;

    private void Start()
    {
        if (_objectToMove == null)
        {
            Debug.LogError("Object to move is not assigned!");
            return;
        }

        GenerateCurve();
        StartCoroutine(MoveObjectAlongCurve());
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
        if (_points == null || _points.Length == 0)
        {
            Debug.LogError("No curve points available!");
            yield break;
        }

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

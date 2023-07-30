using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float threadhold = 0.5f;
    public Transform movingObject;

    private Camera mainCamera;
    private Vector3 lastPos;
    
    public void Draw(Vector3 position)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            var worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;

            if (Vector3.Distance(worldPos, lastPos) >= threadhold)
            {
                lastPos = worldPos;
                Draw(worldPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(MovingObjectWithSpeed(5));
        }
    }

    public List<Vector3> GetMovingPosition()
    {
        var result = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(result);
        return new List<Vector3>(result);
    }

    public IEnumerator MovingObjectCoroutine()
    {
        var movingPos = GetMovingPosition();
        foreach (var pos in movingPos)
        {
            movingObject.transform.position = pos;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator MovingObjectWithSpeed(float speed)
    {
        var path = GetMovingPosition();
        int pathIndex = 0;
        var nextPos = movingObject.position;
        var moveDistancePerFrame = Time.deltaTime * speed;
        
        while (pathIndex < path.Count)
        {
            nextPos = path[pathIndex];
            var direction = nextPos - movingObject.position;
            var moveDistance = direction.magnitude;
            if (moveDistancePerFrame <= moveDistance)
            {
                movingObject.position += direction.normalized * moveDistancePerFrame;
                movingObject.up = direction.normalized;
                yield return null;
            }
            else
            {
                pathIndex++;
            }
        }
    }
}

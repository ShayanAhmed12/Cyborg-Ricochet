using UnityEngine;
public class LineTrail : MonoBehaviour
{
    private LineRenderer _lr;
    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.numCapVertices = 10;
        _lr.startWidth = 0.4f;
        _lr.endWidth = 0.05f;
    }
    public void RenderLine(Vector3 endpoint)
    {
        _lr.positionCount = 2;
        Vector3[] points = { transform.position, endpoint };
        _lr.SetPositions(points);
    }
    public void EndLine()
    {
        _lr.positionCount = 0;
    }
}

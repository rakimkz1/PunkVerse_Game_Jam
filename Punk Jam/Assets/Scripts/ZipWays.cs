using UnityEngine;
using UnityEngine.Splines;

public class ZipWays : MonoBehaviour
{
    public float t;
    private SplineContainer _spline;
    public float maxLength;
    public Transform startPoint;
    private BoxCollider[] boxColliders;

    private void Start()
    {
        _spline = GetComponent<SplineContainer>();
        boxColliders = GetComponents<BoxCollider>();
        maxLength = _spline.CalculateLength();
        boxColliders[0].center = _spline.Spline[0].Position;
        boxColliders[1].center = _spline.Spline[_spline.Spline.Count - 1].Position;
    }

    public Vector3 GetPosiotionInWay(float length)
    {
        return _spline.EvaluatePosition(length / maxLength);
    }
}

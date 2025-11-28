using UnityEngine;
using UnityEngine.Splines;

public class ZipWays : MonoBehaviour
{
    public float t;
    private SplineContainer _spline;
    private float lenth;
    private BoxCollider[] boxColliders;

    private void Start()
    {
        _spline = GetComponent<SplineContainer>();
        boxColliders = GetComponents<BoxCollider>();
        lenth = _spline.CalculateLength();
        boxColliders[0].center = _spline.Spline[0].Position;
        boxColliders[1].center = _spline.Spline[_spline.Spline.Count - 1].Position;
    }

    private void Update()
    {
        Debug.DrawRay(_spline.EvaluatePosition(t / _spline.CalculateLength()), Vector3.up * 10f, Color.red);
    }
}

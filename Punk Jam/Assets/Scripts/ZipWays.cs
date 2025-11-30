using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Splines;

public class ZipWays : MonoBehaviour
{
    public float t;
    private SplineContainer _spline;
    public float maxLength;
    public Transform startPoint;
    [SerializeField] private Transform textPos;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            PlayerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            ExitPlayer();
    }

    private void ExitPlayer()
    {
        textPos.DOScale(0, 0.8f).SetEase(Ease.OutElastic).From(0f);
    }

    private void PlayerEnter()
    {
        textPos.DOScale(1f, 0.8f).SetEase(Ease.OutElastic).From(0f);
    }
}

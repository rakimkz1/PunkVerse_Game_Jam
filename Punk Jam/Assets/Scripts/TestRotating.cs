using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotating : MonoBehaviour
{
	private void Start()
	{
		transform.DORotate(new Vector3(-89.98f, 100, 0), 10).SetEase(Ease.Linear);
	}
}

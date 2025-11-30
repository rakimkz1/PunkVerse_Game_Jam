using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	public float speed = 5f;
    public Transform initialPos, settingsPos, creditsPos;


	private Vector3 nextPos;
	private Vector3 nextRot;

	private Vector3 rotation;
	private bool isPlaying;

	private void Start()
	{
		transform.position = initialPos.position;
		transform.eulerAngles = initialPos.eulerAngles;
	}

	private void Update()
	{
		if (isPlaying)
		{
			transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
			rotation = Vector3.Lerp(rotation, nextRot, speed * Time.deltaTime);
			transform.eulerAngles = rotation;
			if ((transform.position - nextPos).magnitude <= 0.001)
			{
				transform.position = nextPos;
				rotation = nextRot;
				transform.eulerAngles = rotation;
				isPlaying = false;
			}
		}
	}

	public void GoToSettings()
	{
		nextPos = settingsPos.position;
		nextRot = settingsPos.eulerAngles;
		isPlaying = true;
	}

	public void GoToCredits()
	{
		nextPos = creditsPos.position;
		nextRot = creditsPos.eulerAngles;
		isPlaying = true;
	}

	public void GoToInitial()
	{
		nextPos = initialPos.position;
		nextRot = initialPos.eulerAngles;
		isPlaying = true;
	}

	public void StartButton()
	{
		SceneManager.LoadScene(5);
	}

	public void Exit()
	{
		// В билде
		Application.Quit();

		// В редакторе (чтобы не бесить себя отсутствием реакции)
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}

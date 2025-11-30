using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
	[SerializeField] private GameObject choices;
	[SerializeField] private Transform waipaoint;
	[SerializeField] private Animator animator;
	[SerializeField] private PlayerMovement pm;
	[SerializeField] private GameObject repairCutScene;

	private Transform player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			pm.OnStopMoving.Invoke();
			choices.SetActive(true);
			Debug.Log("FINAL");
			player = other.transform;
		}
	}

	public void Repair()
	{
		choices.SetActive(false);
		repairCutScene.SetActive(true);
        StartCoroutine(GOMENU());
    }

	public void Death()
	{
		choices.SetActive(false);
		Vector3 pos = waipaoint.position;
		pos.y = player.position.y;
		player.position = pos;
		animator.SetTrigger("CutScene");
		StartCoroutine(GOMENU());
	}

	private IEnumerator GOMENU(float value = 20)
	{
		yield return new WaitForSeconds(20);
		SceneManager.LoadScene(0);
	}
}

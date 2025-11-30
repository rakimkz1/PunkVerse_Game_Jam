using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalManager : MonoBehaviour
{
    [SerializeField] private GameObject choices;
	[SerializeField] private Transform waipaoint;
	[SerializeField] private Animator animator;
	[SerializeField] private PlayerMovement pm;

	private Transform player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			pm.OnStopMoving.Invoke() ;
			choices.SetActive(true);
			Debug.Log("FINAL");
			player = other.transform;
		}
	}

	public void Repair()
	{
		choices.SetActive(false);
	}

	public void Death()
	{
		choices.SetActive(false );
		Vector3 pos = waipaoint.position;
		pos.y = player.position.y;
		player.position = pos;
		animator.SetTrigger("CutScene");
	}
}

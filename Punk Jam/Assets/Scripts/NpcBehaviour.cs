using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    [SerializeField] private DialogueSystem.DialogueAsset[] dialogueAsset;
    [SerializeField] private DialoguePlayer dialoguePlayer;
	[SerializeField] private GameObject mecanism;

	private int count = 0;
	private bool isActive = false;

	private void Update()
	{
		if (!isActive && count < dialogueAsset.Length)
		{
			mecanism.SetActive(true);
			isActive = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (dialogueAsset != null && dialoguePlayer != null && count < dialogueAsset.Length && TutorialManager.Instance == null)
			{
				dialoguePlayer.StartDialogue(dialogueAsset[count++].StartNode);
			}
			else if(dialogueAsset != null && dialoguePlayer != null && count < dialogueAsset.Length && TutorialManager.Instance.TutorialStages != 2 && TutorialManager.Instance.TutorialStages != 5)
			{
                dialoguePlayer.StartDialogue(dialogueAsset[count++].StartNode);
            }
			mecanism.SetActive(false);
			isActive = false;
		}
	}

}

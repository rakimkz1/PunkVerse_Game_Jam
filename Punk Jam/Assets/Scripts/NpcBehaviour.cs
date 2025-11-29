using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    [SerializeField] private DialogueSystem.DialogueAsset[] dialogueAsset;
    [SerializeField] private DialoguePlayer dialoguePlayer;

	private int count = 0;

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
		}
	}

}

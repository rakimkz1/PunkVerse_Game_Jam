using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
	public class DialoguePlayer : MonoBehaviour
	{
		[Header("Data")]
		[SerializeField] private DialogueAsset dialogueAsset;

		[Header("UI")]
		[SerializeField] private TMP_Text speakerText;
		[SerializeField] private TMP_Text messageText;
		[SerializeField] private RectTransform[] choicesContainer;
		[SerializeField] private GameObject choiceButtonPrefab; // prefab with ChoiceButton + Button + TMP_Text

		[Header("Settings")]
		[SerializeField] private float charsPerSecond = 60f; // дл€ эффекта печати, 0 = мгновенно

		private DialogueNode currentNode;

		private void Start()
		{
			if (dialogueAsset == null)
			{
				Debug.LogError("DialoguePlayer: dialogueAsset is not assigned.", this);
				return;
			}

			StartDialogue(dialogueAsset.StartNode);
		}

		public void StartDialogue(DialogueNode start)
		{
			if (start == null)
			{
				Debug.LogError("Start node is null.");
				return;
			}

			currentNode = start;
			ShowNode(currentNode);
		}

		private void ClearChoices()
		{
			if (choicesContainer == null) return;
			for (int i = choicesContainer.Length - 1; i >= 0; i--)
			{
				for (int j = choicesContainer[i].childCount - 1; j >= 0; j--)
				{
					Destroy(choicesContainer[i].GetChild(j).gameObject);
				}
			}
		}

		private void ShowNode(DialogueNode node)
		{
			StopAllCoroutines();
			ClearChoices();

			if (node == null)
			{
				EndDialogue();
				return;
			}

			StartCoroutine(PlayMessages(node));
		}

		private IEnumerator PlayMessages(DialogueNode node)
		{
			foreach (var msg in node.Messages)
			{
				if (speakerText != null) speakerText.text = msg.SpeakerName;
				if (messageText != null)
				{
					if (charsPerSecond <= 0f)
					{
						messageText.text = msg.Content;
					}
					else
					{
						messageText.text = "";
						float delay = 1f / charsPerSecond;
						for (int i = 0; i < msg.Content.Length; i++)
						{
							messageText.text += msg.Content[i];
							yield return new WaitForSeconds(delay);
						}
					}
				}
				// Waiting till player press space or lmb.
				yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
			}

			// after all messages, we show choice of answers
			ShowChoices(node);
		}

		private void ShowChoices(DialogueNode node)
		{
			ClearChoices();

			if (node.Choices == null || node.Choices.Count == 0)
			{
				// have not answer choices, so we end our dialogue
				EndDialogue();
				return;
			}

			for (int i = 0; i < node.Choices.Count && i < choicesContainer.Length; i++)
			{
				var go = Instantiate(choiceButtonPrefab, choicesContainer[i]);
				var cb = go.GetComponent<ChoiceButton>();
				if (cb != null)
				{
					cb.Setup(node.Choices[i], OnChoiceSelected);
				}
				else
				{
					// fallback: if component didnt found
					var btn = go.GetComponent<Button>();
					var txt = go.GetComponentInChildren<TMP_Text>();
					if (txt != null) txt.text = node.Choices[i].ChoiceText;
					if (btn != null) btn.onClick.AddListener(() => OnChoiceSelected(node.Choices[i]));
				}
			}
		}

		private void OnChoiceSelected(DialogueChoice choice)
		{
			// перейти по выбранному узлу или закончить
			if (choice == null || choice.TargetNode == null)
			{
				EndDialogue();
			}
			else
			{
				currentNode = choice.TargetNode;
				ShowNode(currentNode);
			}
		}

		private void EndDialogue()
		{
			ClearChoices();
			if (speakerText != null) speakerText.text = "";
			if (messageText != null) messageText.text = "";
			// если нужно Ч отправить событие, выключить UI и т.д.
			Debug.Log("Dialogue ended.");
		}
	}
}

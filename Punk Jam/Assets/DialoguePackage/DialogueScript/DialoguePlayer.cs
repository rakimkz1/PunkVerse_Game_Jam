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
		[SerializeField] private Sprite[] sprites;

		[Header("UI")]
		[SerializeField] private GameObject panel;
		[SerializeField] private TMP_Text speakerText;
		[SerializeField] private TMP_Text messageText;
		[SerializeField] private RectTransform[] choicesContainer;
		[SerializeField] private GameObject choiceButtonPrefab; // prefab with ChoiceButton + Button + TMP_Text
		[SerializeField] private GameObject[] images;
		[SerializeField] private Image avatar;
		[SerializeField] private Image upAvatar;

		[Header("Settings")]
		[SerializeField] private float charsPerSecond = 60f; // дл€ эффекта печати, 0 = мгновенно
		[SerializeField] private PlayerMovement pm;

		private DialogueNode currentNode;

		private void Start()
		{
			if (dialogueAsset == null)
			{
				Debug.LogError("DialoguePlayer: dialogueAsset is not assigned.", this);
				return;
			}

			//StartDialogue(dialogueAsset.StartNode);
		}

		public void StartDialogue(DialogueNode start)
		{
			if (start == null)
			{
				Debug.LogError("Start node is null.");
				return;
			}

			panel.SetActive(true);
			currentNode = start;
			pm.OnStopMoving.Invoke();
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
			for (int i = 0; i < node.Messages.Count; i++)
			{
				var msg = node.Messages[i];
				if (speakerText != null) speakerText.text = msg.SpeakerName;
				if (messageText != null)
				{
					if (node.second <= i)
					{
						if (node.Id == "3")
						{
							avatar.sprite = sprites[int.Parse(node.Id) + 1];
							upAvatar.sprite = sprites[6];
							images[1].SetActive(true);
						}
						else
						{
							avatar.sprite = sprites[int.Parse(node.Id) + 1];
						}
						images[0].SetActive(true);

					}
					else if (node.first <= i)
					{
						avatar.sprite = sprites[int.Parse(node.Id)];
						images[0].SetActive(true);
					}

					if (charsPerSecond <= 0f)
					{
						messageText.text = msg.Content;
					}
					else
					{
						messageText.text = "";
						float delay = 1f / charsPerSecond;
						for (int j = 0; j < msg.Content.Length; j++)
						{
							messageText.text += msg.Content[j];
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
			images[0].SetActive(false);
			images[1].SetActive(false);
			pm.OnStartMoving.Invoke();
			if (TutorialManager.Instance != null)
			{
				TutorialManager.Instance.AddStage();
			}
			panel.SetActive(false);
			// если нужно Ч отправить событие, выключить UI и т.д.
			Debug.Log("Dialogue ended.");
		}
	}
}

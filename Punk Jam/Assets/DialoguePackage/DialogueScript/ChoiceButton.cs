using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
	[RequireComponent(typeof(Button))]
	public class ChoiceButton : MonoBehaviour
	{
		[SerializeField] private TMP_Text label;

		private Button button;
		private DialogueChoice choice;

		private void Awake()
		{
			button = GetComponent<Button>();
		}

		public void Setup(DialogueChoice c, System.Action<DialogueChoice> onClick)
		{
			choice = c;
			if (label != null) label.text = c?.ChoiceText ?? "??";
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => onClick?.Invoke(choice));
		}
	}
}

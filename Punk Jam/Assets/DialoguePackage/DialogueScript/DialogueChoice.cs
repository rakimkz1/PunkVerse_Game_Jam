using UnityEngine;

namespace DialogueSystem
{
	[System.Serializable]
	public class DialogueChoice
	{
		[SerializeField] private string choiceText;
		[SerializeField] private DialogueNode targetNode;

		public string ChoiceText => choiceText;
		public DialogueNode TargetNode => targetNode;

		public DialogueChoice() { }

		public DialogueChoice(string text, DialogueNode target)
		{
			choiceText = text;
			targetNode = target;
		}
	}
}

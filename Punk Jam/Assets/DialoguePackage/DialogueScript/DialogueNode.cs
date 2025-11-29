using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
	[CreateAssetMenu(fileName = "NewNode", menuName = "Dialogue System/Node")]
	public class DialogueNode : ScriptableObject
	{
		[SerializeField] private string id;
		[SerializeField] private List<DialogueMessage> messages = new List<DialogueMessage>();
		[SerializeField] private List<DialogueChoice> choices = new List<DialogueChoice>();
		public int first, second;

		// »нкапсул€ци€: только getters наружу
		public string Id => id;
		public IReadOnlyList<DialogueMessage> Messages => messages;
		public IReadOnlyList<DialogueChoice> Choices => choices;

		private void OnValidate()
		{
			if (string.IsNullOrEmpty(id))
			{
				id = name + "_" + System.Guid.NewGuid().ToString("N").Substring(0, 8);
			}
		}
	}
}

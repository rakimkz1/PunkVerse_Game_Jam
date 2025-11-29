using UnityEngine;

namespace DialogueSystem
{
	[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue Asset")]
	public class DialogueAsset : ScriptableObject
	{
		[SerializeField] private DialogueNode startNode;
		public DialogueNode StartNode => startNode;

		// helper to validate in editor if you want (optional)
		public bool IsValid() => startNode != null;
	}
}

using UnityEngine;

namespace DialogueSystem
{
	[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue Asset")]
	public class DialogueAsset : ScriptableObject
	{
		[SerializeField] private DialogueNode startNode;
		public DialogueNode StartNode => startNode;

		public bool IsValid() => startNode != null;
	}
}

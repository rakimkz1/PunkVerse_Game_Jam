using UnityEngine;

namespace DialogueSystem
{
	[System.Serializable]
	public class DialogueMessage
	{
		[SerializeField] private string speakerName;
		[SerializeField, TextArea(2, 6)] private string content;

		public string SpeakerName => speakerName;
		public string Content => content;

		public int spriteId;

		public DialogueMessage() { }

		public DialogueMessage(string speaker, string content)
		{
			this.speakerName = speaker;
			this.content = content;
		}
	}
}

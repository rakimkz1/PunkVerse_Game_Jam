using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TutorialDashTrail : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int inRow;
    public Transform body;
    [SerializeField] private TactMachine tactMachine;

    private void Update()
    {
        if (TutorialManager.Instance.TutorialStages != 2)
            return;
        body.gameObject.SetActive(true);

        if(Input.GetKeyDown(KeyCode.Space) && tactMachine.IsBeatTact())
        {
            inRow++;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !tactMachine.IsBeatTact())
        {
            inRow = 0;
        }
        text.text = inRow.ToString();

        if(inRow == 5)
        {
            TutorialManager.Instance.TutorialStages++;
            TutorialManager.Instance.currentPoint++;
            TutorialManager.Instance.Move(TutorialManager.Instance.wayPoints[TutorialManager.Instance.currentPoint].position);
            body.gameObject.SetActive(false);
        }
    }
}

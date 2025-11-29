using TMPro;
using UnityEngine;

public class AttackTrail : MonoBehaviour, IAttackTarget
{
    public TactMachine tactMachine;
    public AudioClip attackHit;
    public GameObject body;
    public TextMeshProUGUI numberText;
    public int inRow;
    public void Attacked(float damage)
    {
        AudioManager.instance.PlayAudioOneShot(attackHit, 1f);
    }

    private void Update()
    {
        if (TutorialManager.Instance.TutorialStages != 3)
            return;
        body.gameObject.SetActive(true);

        if (Input.GetMouseButtonDown(0) && tactMachine.IsBeatTact())
        {
            inRow++;
        }
        if (Input.GetMouseButtonDown(0) && !tactMachine.IsBeatTact())
        {
            inRow = 0;
        }
        numberText.text = inRow.ToString();

        if(inRow == 4)
        {
            TutorialManager.Instance.TutorialStages++;
            TutorialManager.Instance.currentPoint++;
            TutorialManager.Instance.Move(TutorialManager.Instance.wayPoints[TutorialManager.Instance.currentPoint].position);
            body.SetActive(false);
        }
    }
}

using UnityEngine;

public class AttackTestTarget : MonoBehaviour, IAttackTarget
{
    public void Attacked(float damage)
    {
        Debug.Log("Been Attacked");
    }
}

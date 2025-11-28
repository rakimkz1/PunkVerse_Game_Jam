using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public float aprochRange;
    public float moveingToTargetTime;
    public float damage;

    private TactMachine _tactMachine;
    private PlayerZip playerZip;

    private void Awake()
    {
        _tactMachine = GetComponent<TactMachine>();
        playerZip = GetComponent<PlayerZip>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerZip.isZiping && _tactMachine.IsBeatTact())
        {
            SearchAttackTarget();
        }
    }

    private void SearchAttackTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);

        for(int i =0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.GetComponent<IAttackTarget>() != null)
            {
                AttackTarget(hits[i].gameObject);
                return;
            }
        }
    }

    private async Task AttackTarget(GameObject target)
    {
        float time = 0f;
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        Vector3 moveToPos = targetPos - (targetPos - transform.position).normalized * aprochRange; 
        while (time < moveingToTargetTime)
        {
            await Task.Yield();
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, moveToPos, time / moveingToTargetTime);
        }

        target.GetComponent<IAttackTarget>().Attacked(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

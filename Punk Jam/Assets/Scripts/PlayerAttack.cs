using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public float aprochRange;
    public float moveingToTargetTime;
    public float damage;
    public float impactForce;

    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private AudioClip misAttackSound;
    [SerializeField] private AudioClip attackSound;
    private TactMachine _tactMachine;
    private PlayerZip playerZip;
    private Rigidbody _rb;
    private PlayerMovement playerMovement;
    private bool isAttackable = true;

    private void Awake()
    {
        _tactMachine = GetComponent<TactMachine>();
        playerZip = GetComponent<PlayerZip>();
        playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
        playerMovement.OnStartMoving += () =>
        {
            isAttackable = true;
        };
        playerMovement.OnStopMoving += () =>
        {
            isAttackable = false;
        };
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerZip.isZiping && _tactMachine.IsBeatTact() && isAttackable)
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
                anim.Attack();
                AudioManager.instance.PlayAudioOneShot(attackSound, 1f);
                AttackTarget(hits[i].gameObject);
                return;
            }
        }
        anim.Attack();
        AudioManager.instance.PlayAudioOneShot(misAttackSound, 1f);
    }

    private async Task AttackTarget(GameObject target)
    {
        float time = 0f;
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        Vector3 moveToPos = targetPos - (targetPos - transform.position).normalized * aprochRange;
        playerMovement.TargetRotate(targetPos, 0.6f);
        while (time < moveingToTargetTime)
        {
            await Task.Yield();
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, moveToPos, time / moveingToTargetTime);
        }

        _rb.velocity += (target.transform.position).normalized * impactForce;

        target.GetComponent<IAttackTarget>().Attacked(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

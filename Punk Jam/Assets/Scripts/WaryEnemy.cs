using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class WaryEnemy : MonoBehaviour, IAttackTarget
{
    public float tact;
    public float Damage;
    public float movingTime;

    public AnimationCurve movingCurve;

    public Transform[] wayPoints;
    public Transform playerPosition;
    public bool isAttackPreparing;
    public bool isAttacking;
    public bool isIdel;
    public float attackRange;
    private int currentWay;
    private int currentDiraction = 1;
    private Animator animator;
    public void Attacked(float damage)
    {

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartTact());
    }

    private void Update()
    {
        if(Vector3.Distance(playerPosition.position, transform.position) < attackRange)
        {
            isAttackPreparing = true;
        }
    }

    IEnumerator StartTact()
    {
        while (true)
        {
            yield return new WaitForSeconds(tact);
            BeatTact();
        }
    }

    private void BeatTact()
    {
        if (isAttackPreparing && !isAttacking)
            PreparintToAttack();
        else if (isAttacking)
            Attack();
        else if (isIdel)
            IdelTime();
        else
            Move();
    }

    private async Task Move()
    {
        if (isAttacking)
            return;
        float time = 0f;
        if (currentWay == 0 || currentWay == wayPoints.Length - 1)
            currentDiraction *= -1;
        while(time < movingTime)
        {
            transform.position = Vector3.Lerp(wayPoints[currentWay].position, wayPoints[currentWay + currentDiraction].position, movingCurve.Evaluate(time / movingTime));
            time += Time.deltaTime;
            await Task.Yield();
        }
        currentWay += currentDiraction;
    }

    private void PreparintToAttack()
    {
        isAttacking = true;
        animator.SetTrigger("prepaire");
    }
    private void IdelTime()
    {
        animator.SetTrigger("idel");
        isIdel = false;
    }

    private void Attack()
    {
        if(Vector3.Distance(playerPosition.position, transform.position) < attackRange)
        {
            playerPosition.GetComponent<PlayerHealthManager>().TakeDamage(Damage);
        }
        isAttacking = false;
        isAttackPreparing = false;
        animator.SetTrigger("attack");
        isIdel = true;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < wayPoints.Length-1; i++)
        {
            Gizmos.DrawLine(wayPoints[i].position, wayPoints[i + 1].position);
        }
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

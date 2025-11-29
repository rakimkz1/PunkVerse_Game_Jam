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
    public float rotateSpeed;
    public float attackRange;
    public float prepairingRange;
    private int currentWay;
    private int currentDiraction = 1;
    private bool isPrepered;
    private Transform playerPosition;
    private Animator animator;
    public void Attacked(float damage)
    {

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        StartCoroutine(StartTact());
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
        if (Vector3.Distance(transform.position, playerPosition.position) < prepairingRange)
        {
            PrepareAttack();
        }
        else if (isPrepered)
        {
            AttackPlayer();
        }
        else
            Move();
    }

    private void AttackPlayer()
    {
        animator.SetTrigger("attack");
        animator.SetBool("idel", false);
        isPrepered = false;

        if(Vector3.Distance(transform.position, playerPosition.position) < attackRange)
        {
            PlayerHealthManager health = playerPosition.GetComponent<PlayerHealthManager>();
            health.TakeDamage(Damage);
        }
    }

    private void PrepareAttack()
    {
        animator.SetTrigger("prepaire");
        animator.SetBool("idel", false);
        isPrepered = true;
    }

    private async Task Move()
    {
        float time = 0f;
        animator.SetBool("idel", true);
        if (currentWay == 0 || currentWay == wayPoints.Length - 1)
            currentDiraction *= -1;
        while(time < movingTime)
        {
            transform.position = Vector3.Lerp(wayPoints[currentWay].position, wayPoints[currentWay + currentDiraction].position, movingCurve.Evaluate(time / movingTime));
            RotateBody();
            time += Time.deltaTime;
            await Task.Yield();
        }
        currentWay += currentDiraction;
    }

    private void RotateBody()
    {
        Quaternion rot = Quaternion.LookRotation(wayPoints[currentWay + currentDiraction].position - wayPoints[currentWay].position, wayPoints[currentWay + currentDiraction].up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotateSpeed);
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

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public bool isMoving;
    public bool isZiping;
    [SerializeField] private PlayerMovement movement;
    private Animator animator;
    public int maxAttack;
    public float attackcComboColdown;

    private int currentAttack = 0;
    private float currentAttackComboColdown;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement.OnStopMoving += () =>
        {
            isMoving = false;
            isZiping = false;
        };
    }
    public void DashAnim()
    {
        animator.SetTrigger("dash");
    }

    public void Attack()
    {
        if(currentAttackComboColdown != 0f)
        {
            currentAttack = (currentAttack + 1) % maxAttack;
            currentAttackComboColdown = attackcComboColdown;
            animator.SetInteger("attackNumber", currentAttack);
            animator.SetTrigger("attack");
        }
        else
        {
            currentAttackComboColdown = attackcComboColdown;
            currentAttack = 0;
            animator.SetInteger("attackNumber", 0);
            animator.SetTrigger("attack");
        }
    }

    private void Update()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isZiping", isZiping);
    }
}
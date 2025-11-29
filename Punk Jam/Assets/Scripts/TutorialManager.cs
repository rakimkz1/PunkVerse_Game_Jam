using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public float movingSpeed;
    public float rotateSpeed;
    public Transform[] wayPoints;

    public int currentPoint;
    public Vector3 lookingDiriction;
    public Vector3 movingDiriction;

    public int TutorialStages;

    [SerializeField] private Transform player;

    private bool isLookingAtPlayer;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Quaternion to = Quaternion.LookRotation(lookingDiriction - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, rotateSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, movingDiriction, movingSpeed * Time.deltaTime);

        if(transform.position == movingDiriction)
        {
            lookingDiriction = new Vector3(player.position.x, transform.position.y, player.position.z);
        }
    }

    public void AddStage()
    {
        TutorialStages++;
        if(TutorialStages == 1) { }
    }

    public void Move(Vector3 to)
    {
        movingDiriction = to;
        lookingDiriction = to;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isKeyFound;
    public int sceneNumber;
    public bool isOpen;
    private Animator anim;
    public bool isPlayerEnterInTriger;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isKeyFound && isPlayerEnterInTriger)
        {
            Debug.Log("openedDoor");
            anim.SetTrigger("open");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerEnterInTriger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerEnterInTriger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isOpen && collision.gameObject.tag == "Player")
        {
            MoveToNextScene();
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    private void MoveToNextScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}

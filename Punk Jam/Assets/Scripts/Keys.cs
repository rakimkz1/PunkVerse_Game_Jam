using DG.Tweening;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public Door door;

    private void Start()
    {
        transform.DOMoveY(transform.position.y + 1f, 1f).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0f, 180f, 90f), 2f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.isKeyFound = true;
            gameObject.SetActive(false);
        }
    }
}

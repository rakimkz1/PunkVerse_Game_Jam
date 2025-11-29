using UnityEngine;

public class Keys : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.isKeyFound = true;
            gameObject.SetActive(false);
        }
    }
}

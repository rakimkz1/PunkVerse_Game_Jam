using UnityEngine;

public class PlayerZip : MonoBehaviour
{

    public float zipSpeed;
    [SerializeField] private Transform zipingPoint;
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && other.gameObject.GetComponents<ZipWays>() != null)
        {
            ZipAttach(other.gameObject.GetComponent<ZipWays>());
        }
    }

    private void ZipAttach(ZipWays zipWay)
    {
        transform.position
    }
}    

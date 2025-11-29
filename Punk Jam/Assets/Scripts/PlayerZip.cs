using System.Threading.Tasks;
using UnityEngine;

public class PlayerZip : MonoBehaviour
{
    public float zipSpeed;
    public bool isZiping;
    [SerializeField] private Transform zipingPoint;
    [SerializeField] private PlayerAnimation anim;
    private float _nowLength;
    private Rigidbody rb;
    private PlayerMovement movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && other.gameObject.GetComponent<ZipWays>() != null)
        {
            ZipAttach(other.gameObject.GetComponent<ZipWays>());
        }
    }

    private async Task ZipAttach(ZipWays zipWay)
    {
        isZiping = true;
        rb.isKinematic = true;
        anim.isZiping = true;
        movement.bodyView.rotation = zipWay.transform.rotation;
       
        while(zipWay.maxLength > _nowLength)
        {
            await Task.Yield();
            transform.position = zipWay.GetPosiotionInWay(_nowLength) + (transform.position - zipingPoint.position);
            Debug.DrawRay(zipWay.GetPosiotionInWay(_nowLength), Vector3.up);
            _nowLength += zipSpeed * Time.deltaTime;
        }
        _nowLength = 0f;
        anim.isZiping = false;
        rb.isKinematic = false;
        isZiping = false;
    }
}    
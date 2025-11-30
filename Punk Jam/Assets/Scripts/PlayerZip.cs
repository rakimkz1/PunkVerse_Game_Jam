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
    private bool isInZipWayZone;
    private ZipWays wayInZone;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ZipWays>() != null)
        {
            isInZipWayZone = true;
            wayInZone = other.gameObject.GetComponent<ZipWays>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<ZipWays>() != null)
        {
            isInZipWayZone = false;
            wayInZone = null; ;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInZipWayZone)
            ZipAttach(wayInZone);
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
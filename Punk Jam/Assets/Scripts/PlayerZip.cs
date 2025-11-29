using System.Threading.Tasks;
using UnityEngine;

public class PlayerZip : MonoBehaviour
{

    public float zipSpeed;
    public bool isZiping;
    [SerializeField] private Transform zipingPoint;
    [SerializeField] private PlayerAnimation anim;
    private float _nowLength;
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
        anim.isZiping = true;
        while(zipWay.maxLength > _nowLength)
        {
            await Task.Yield();
            transform.position = zipWay.GetPosiotionInWay(_nowLength) - zipingPoint.localPosition;
            Debug.DrawRay(zipWay.GetPosiotionInWay(_nowLength), Vector3.up);
            _nowLength += zipSpeed * Time.deltaTime;
        }
        anim.isZiping = false;
        isZiping = false;
    }
}    
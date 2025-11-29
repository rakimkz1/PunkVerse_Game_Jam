using System.Threading.Tasks;
using UnityEngine;

public class PlayerZip : MonoBehaviour
{

    public float zipSpeed;
    public bool isZiping;
    [SerializeField] private Transform zipingPoint;
    private float _nowLength;
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && other.gameObject.GetComponents<ZipWays>() != null)
        {
            Debug.Log(other.gameObject.name);
            ZipAttach(other.gameObject.GetComponent<ZipWays>());
        }
    }

    private async Task ZipAttach(ZipWays zipWay)
    {
        isZiping = true;
        while(zipWay.maxLength > _nowLength)
        {
            await Task.Yield();
            transform.position = zipWay.GetPosiotionInWay(_nowLength) - zipingPoint.localPosition;
            Debug.DrawRay(zipWay.GetPosiotionInWay(_nowLength), Vector3.up);
            _nowLength += zipSpeed * Time.deltaTime;
        }
        isZiping = false;
    }
}    

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Vector3 offset = Vector3.zero;
    
    private void LateUpdate()
    {

        transform.position = target.position + offset;

    }
}

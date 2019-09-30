using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [Range(0,1)]
    [SerializeField] float smoothSpeed;
    
    
    private void LateUpdate()
    {

        transform.position = target.position + offset;

    }
}

using UnityEngine;

public class camera_follow: MonoBehaviour{
    // where camara will point towards
    public Transform target; 

    // how far from the target it will be
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Reset(Vector3 target_position, Vector3 target_offset){
        target.position = target_position;
        target.offset = target_offset;
        // camera.GetComponent<camera_follow>().target.position = center_point;
        // camera.GetComponent<camera_follow>().offset = new Vector3(0.0f, 30.0f, -100.0f);
    }


    void LateUpdate(){        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

}
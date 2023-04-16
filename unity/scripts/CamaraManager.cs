using UnityEngine;

///  Main camera manager script.
/// <summary>
///
/// </summary>
///
public class CamaraManager: MonoBehaviour{
    // where camara will point towards
    private Vector3 target_position = new Vector3(7.0f, 0.0f, -4.0f);

    // how far from the target it will be
    private Vector3 offset;

    private Vector3 offset_init = new Vector3(0.0f, 30.0f, -100.0f);
    private Vector3 offset_end = new Vector3(0.0f, 30.0f, 100.0f);

    // speed of camera(drone) flight
    private float smoothSpeed = 0.125f;
    private float delta_t = 0.3f;

    void start(){
        offset = offset_init;
    }

    void LateUpdate(){        
        // t += 1;
        // Vector3 desiredPosition = target_position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * t);
        // transform.position = smoothedPosition;
        // transform.LookAt(target_position);

        // update offset of camera as a drone flight
        if (offset[2] >= 100){
           offset = offset_init;
        }
        offset = offset + new Vector3(0.0f, 0.0f, 1.0f) * delta_t;
        Vector3 desiredPosition = target_position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target_position);
    }

    // void Update(){ 
    //     // move camera as a drone flight
    //     if (transform.offset[2] >= 100){
    //        transform.offset = offset_init;
    //     }
    //     transform.offset = transform.offset + new Vector3(0.0f, 0.0f, 1.0f) * delta_t;
    // }

    public void Reset(Vector3 target_position_, Vector3 target_offset){
        target_position = target_position_;
        offset = target_offset;
        // camera.GetComponent<camera_follow>().target.position = center_point;
        // camera.GetComponent<camera_follow>().offset = new Vector3(0.0f, 30.0f, -100.0f);
    }

    void Randomize(){
        // TODO random altura del dron
        target_position = new Vector3(7.0f, 0.0f, -4.0f);
        offset = new Vector3(0.0f, 30.0f, -100.0f);
    }
}
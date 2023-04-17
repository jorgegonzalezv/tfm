using UnityEngine;
using tfm;

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
    private Vector3 offset_end = new Vector3(0.0f, 10.0f, -100.0f);

    // speed of camera(drone) flight
    private float smoothSpeed = 0.125f;
    private float delta_t = 0.3f;

    void start(){
        offset = offset_init;
    }

    void LateUpdate(){  
        // update offset of camera as a drone flight
        if (offset[2] >= 100){
           offset = offset_end;
        }
        offset = offset + new Vector3(0.0f, 0.0f, 1.0f) * delta_t;
        Vector3 desiredPosition = target_position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target_position);
    }

    public void Reset(Vector3 target_position_, Vector3 target_offset){
        if (offset[1] == 30){
            offset[1] = Randomize.randomInt(5, 40); // random flight height, better define
        }else {
            offset = offset_init;
        }
        target_position = target_position_;
        // offset = target_offset;
        Debug.Log("Drone height: " + offset[1]);
    }
}
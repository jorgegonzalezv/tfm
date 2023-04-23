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

    // speed of camera(drone) flight
    private float smoothSpeed = 0.125f;
    private float delta_t = 0.3f;

    // dataset parameters
    // private int reset_counter;
    private float[] flight_height = {5, 10, 15, 20, 30, 40, 50, 60};


    void start(){
        offset = offset_init;
    }

    void LateUpdate(){  
        // update offset of camera as a drone flight
        // if (offset[2] >= 100){
        //    offset = offset_end;
        // }
        offset = offset + new Vector3(0.0f, 0.0f, 1.0f) * delta_t;
        Vector3 desiredPosition = target_position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target_position);
    }

    public void Reset(int reset_counter){
        reset_counter = reset_counter % flight_height.Length;

        // target_position = target_position_;
        offset = offset_init;
        offset[1] = flight_height[reset_counter];
        
        // reset_counter += 1;
        // Debug.Log("Drone height: " + offset[1] + " Counter: "+ reset_counter);
    }
}
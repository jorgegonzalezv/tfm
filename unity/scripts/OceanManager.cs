using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Crest;

public class OceanManager : MonoBehaviour
{
    // public GameObject ocean; 
    private OceanRenderer oceanRenderer;

    public GameObject waves;
    private ShapeFFT wavesRenderer;

    // dataset parameters
    // private int reset_counter;

    // ocean
    // globalwindspeed

    // waves
    private float[] windSpeed = {0, 10, 20, 30, 50, 70, 90, 120};
    private float[] waveWindWeight = {0f, 0.1f, 0.2f, 0.3f, 0.5f, 0.7f, 0.9f, 1f};
    private float[] waveAngle = {-180, -100, -20, 0, 50, 70, 90, 120};
    public int max_counter = 8;

    // Start is called before the first frame update
    void Start(){
        oceanRenderer = gameObject.GetComponent<OceanRenderer>();
        wavesRenderer = waves.GetComponent<ShapeFFT>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Reset(int reset_counter){
        // ocean
        reset_counter = reset_counter % windSpeed.Length;
        wavesRenderer._windSpeed = windSpeed[reset_counter];

        // waves
        wavesRenderer._weight = waveWindWeight[reset_counter];

        wavesRenderer._waveDirectionHeadingAngle = waveAngle[reset_counter];
        //reset_counter += 1;

        // Debug.Log("Wind speed: " + oceanRenderer._globalWindSpeed + " Counter: " + reset_counter);
        // Debug.Log("Wave weight: " + wavesRenderer._weight + " Counter: " + reset_counter);

    }

    void Randomize(){

    }
}

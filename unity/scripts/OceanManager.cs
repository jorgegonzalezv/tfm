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
    private int reset_counter;

    // ocean
    private float[] globalWindSpeed = {0, 10, 20, 30, 50, 70, 90, 120};
    // waves
    private float[] waveWindWeight = {0f, 0.1f, 0.2f, 0.3f, 0.5f, 0.7f, 0.9f, 1f}; // es de las waves


    // Start is called before the first frame update
    void Start(){
        oceanRenderer = gameObject.GetComponent<OceanRenderer>();
        wavesRenderer = waves.GetComponent<ShapeFFT>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Reset(){
        // ocean
        oceanRenderer._globalWindSpeed = globalWindSpeed[reset_counter];

        // waves
        wavesRenderer._weight = waveWindWeight[reset_counter];
        reset_counter += 1;

        Debug.Log("Wind speed: " + oceanRenderer._globalWindSpeed);
        Debug.Log("Wave weight: " + wavesRenderer._weight);

    }

    void Randomize(){

    }
}

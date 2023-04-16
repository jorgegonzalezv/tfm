using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Crest;

public class OceanManager : MonoBehaviour
{
    // public GameObject ocean; 

    private OceanRenderer oceanRenderer;

    // Start is called before the first frame update
    void Start(){
        oceanRenderer = gameObject.GetComponent<OceanRenderer>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Reset(){
        // if (reset_counter%2 == 0){
        //    oceanRenderer._globalWindSpeed = 150;        
        oceanRenderer._globalWindSpeed = 99;
    }

    void Randomize(){

    }
}

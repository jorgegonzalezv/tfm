/*
    Example script.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class cubo_manager : MonoBehaviour
{
	//public GameObject Cube;
    // Start is called before the first frame update
    public bool flag = true;

    //public Vector3 init_point = new Vector3(0.0f,3.0f,-4.0f);
    //public Vector3 end_point = new Vector3(0.0f,0.0f,-4.0f);

    public Vector3 init_point = new Vector3(3.0f, 0.0f, -4.0f); // (y, x, z)
    public Vector3 end_point = new Vector3(-9.0f, 0.0f, -4.0f);

    public float t = 0.0f;
    public float delta = 0.1f;

    void Start()
    {
        Debug.Log(string.Format("FIRST POSITION: {0}", transform.position));
        transform.position = init_point;
    	InvokeRepeating("UpdateCube", 2.0f, 1.0f);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCube()
    {
        transform.position = (1 - t) * init_point + t * end_point;
        t = delta + t;        
        Debug.Log(string.Format("Position: {0}", transform.position));

        /*
    	if (flag == true){
    		Cube.GetComponent<Renderer>().enabled = false;
    		flag = false;
    	}else{
    		Cube.GetComponent<Renderer>().enabled = true;
    		flag = true;
    	}*/
    }
}

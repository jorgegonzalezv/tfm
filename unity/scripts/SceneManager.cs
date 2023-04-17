using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using tfm;

public class SceneManager : MonoBehaviour
{
    // global configuration
    private int counter = 0;
    private int reset_counter = 0;

    // camera
    public Camera cam;
    public GameObject camera;
    private CamaraManager cameraManager;  // evitar?

    public Vector3 center_point = new Vector3(7.0f, 0.0f, -4.0f);
    public Vector3 offset = new Vector3(0.0f, 30.0f, -100.0f);
    public float delta_t = 0.3f;

    // ocean
    public GameObject ocean;
    private OceanManager oceanManager;  // evitar?

    // scene objects
    public GameObject boat1; 
    public GameObject boat2;
    public GameObject person;
    public List<GameObject> objects = new List<GameObject>(); 


    /*
        Start is called before the first frame update
    */
    void Start() {
        // 0. init periodic tasks
        InvokeRepeating("SaveGroundTruthAll", 2.0f, 1.0f);
        InvokeRepeating("Reset", 2.0f, 5.0f);


        // 0. init variables
		objects.Add(boat1);
        objects.Add(boat2);
		objects.Add(person);

        // set unvisible bounding spheres
        // for(int i=0; i<objects.Count; i++){
        //     objects[i].transform.Find("Bounding").gameObject.GetComponent<Renderer>().enabled = false;
        // }

        Reset();
    }

    /*
        Reset scene with random parameters.
    */
    void Reset(){
        // reset scene with random params
        oceanManager = ocean.GetComponent<OceanManager>();
        cameraManager = camera.GetComponent<CamaraManager>();

        // TODO POR AQUI!!!
        // 1. set up ocean conditions
        oceanManager.Reset();  // quizas puede hacerse directamente en camaraManager

        // 2. set up scene objects
        boat1.transform.position = new Vector3(0.0f, 0.0f, -4.0f);
        boat2.transform.position = new Vector3(7.0f, 0.0f, -4.0f);
        person.transform.position = new Vector3(14.0f, 0.0f, -4.0f);
        
        // set unvisible non-participant objects
        //foo.GetComponent<Renderer>().enabled = false;

        // 3. define dron flight
        cameraManager.Reset(center_point, offset);  // quizas puede hacerse directamente en camaraManager
    }

    /*
        Save ground truth for currentñy visible gameobjects
    */
    void SaveGroundTruthAll(){
        // save ground truth for every gameobjects
        for(int i=0; i<objects.Count; i++){
            SaveGroundTruth(objects[i].transform.Find("Bounding").gameObject);
        }
        // SaveGroundTruth(person.transform.Find("Bounding").gameObject);
        counter = counter + 1;
    }

    void SaveGroundTruth(GameObject selected_object){
        // https://docs.unity3d.com/ScriptReference/Renderer-bounds.html
        // get gameobject pixel position
        var path = "dataset/debug.txt";
        var createText = "";
        var points = ObjectDection.getObjectBoundingBox(selected_object, cam);
        createText = counter+","+points[0]+"," +points[1]+","+points[2]+"," +points[3]+"\n";
        File.AppendAllText(path, createText);
    }
}

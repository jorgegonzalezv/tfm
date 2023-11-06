using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using tfm;

public class SceneManager : MonoBehaviour
{
    // screenshot
    private int superSize = 4;
    private int counter = 0;

    // global configuration
    private int reset_counter = 0;
    private int OCEAN_COUNTER = 0;
    private int CAMARA_COUNTER = 1;
    private int[] reset_counters = {0, 0};

    // camera
    public Camera cam;
    public GameObject camera;
    private CamaraManager cameraManager;  // evitar?

    public Vector3 center_point = new Vector3(7.0f, 0.0f, -4.0f);
    public float delta_t = 0.3f;

    // ocean
    public GameObject ocean;
    private OceanManager oceanManager;  // evitar?

    // scene objects
    public GameObject boat1; 
    public GameObject boat2;
    public GameObject person1;
    public GameObject person2;
    public GameObject person3;

    public string[] labels = {
        "boat", "boat", "person","person","person" 
    };
    public List<GameObject> objects = new List<GameObject>(); 


    /*
        Start is called before the first frame update
    */
    void Start() {
        // 0. init variables
		objects.Add(boat1);
        objects.Add(boat2);
		objects.Add(person1);
		objects.Add(person2);
		objects.Add(person3);

        // get managers
        oceanManager = ocean.GetComponent<OceanManager>();
        cameraManager = camera.GetComponent<CamaraManager>();

        // reset scene with random params
        Reset();

        Time.timeScale = 1;
        // Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // init periodic tasks
        InvokeRepeating("Step", 0.2f, 2.0f);
    }


    void Step(){
        if (counter % 7 == 0){
            Reset();
        }
        // cameraManager.Freeze();
        Time.timeScale = 0;
        // Time.fixedDeltaTime = 0.02f * Time.timeScale;

        TakeScreenShot();
        SaveGroundTruthAll();
        counter = counter + 1;
        // cameraManager.UnFreeze();
        Time.timeScale = 1;
        // Time.fixedDeltaTime = 0.02f * Time.timeScale;


        // counter = counter + 1;
    }
    /*
        Reset scene with random parameters.
    */
    void Reset(){
        Debug.Log("Reset counters: "+ reset_counters[OCEAN_COUNTER] + " " + reset_counters[1] );
        
        // 1. set up ocean conditions
        oceanManager.Reset(reset_counters[OCEAN_COUNTER]);  // quizas puede hacerse directamente en camaraManager

        // 2. set up scene objects
        ResetObjects();
        
        // 3. define dron flight
        cameraManager.Reset(reset_counters[CAMARA_COUNTER]);  // quizas puede hacerse directamente en camaraManager

        reset_counters[CAMARA_COUNTER] += 1;
        if (reset_counters[CAMARA_COUNTER] == cameraManager.max_counter){ // 9
            reset_counters[CAMARA_COUNTER] = 0;
            reset_counters[OCEAN_COUNTER] += 1;
        }

        if (reset_counters[OCEAN_COUNTER] == oceanManager.max_counter){
            Debug.Log("END OF SIMULATION");
            Debug.Break();
        }
    }

    void ResetObjects(){
       Vector3 random_translation = new Vector3(Randomize.randomFloat(-10, 10), -0.5f, Randomize.randomFloat(-10, 10));
        boat1.transform.position =  boat1.transform.position + random_translation;
        boat1.transform.Rotate(0, Randomize.randomFloat(0, 180), 0);

        random_translation = new Vector3(Randomize.randomFloat(-10, 10), -0.5f, Randomize.randomFloat(-10, 10));
        boat2.transform.position = new Vector3(7.0f, 0.0f, -4.0f) + random_translation;
        boat2.transform.Rotate(0, Randomize.randomFloat(0, 180), 0);

        random_translation = new Vector3(Randomize.randomFloat(-10, 10), -0.5f, Randomize.randomFloat(-10, 10));
        person1.transform.position = new Vector3(14.0f, 0.0f, -4.0f) + random_translation;
        person1.transform.Rotate(0, Randomize.randomFloat(0, 180), 0);

        random_translation = new Vector3(Randomize.randomFloat(-10, 10), -0.5f, Randomize.randomFloat(-10, 10));
        person2.transform.position = new Vector3(14.0f, 0.0f, -4.0f) + random_translation;
        person2.transform.Rotate(0, Randomize.randomFloat(0, 180), 0);

        random_translation = new Vector3(Randomize.randomFloat(-10, 10), -0.5f, Randomize.randomFloat(-10, 10));
        person3.transform.position = new Vector3(14.0f, 0.0f, -4.0f) + random_translation;
        person3.transform.Rotate(0, Randomize.randomFloat(0, 180), 0);

        // set unvisible non-participant objects
        if (reset_counters[CAMARA_COUNTER] % 2 == 0){
            person2.SetActive(false);
        }else{
            person2.SetActive(true);
        }
    }

    /*
        Save ground truth for currentñy visible gameobjects
    */
    void SaveGroundTruthAll(){
        // save ground truth for every gameobjects
        for(int i=0; i<objects.Count; i++){
            if (objects[i].active == true){
                SaveGroundTruth(objects[i].transform.Find("Bounding").gameObject, labels[i]);
            }
        }
    }

    void SaveGroundTruth(GameObject selected_object, string label){
        // https://docs.unity3d.com/ScriptReference/Renderer-bounds.html
        // get gameobject pixel position
        var path = "dataset/debug.txt";
        var createText = "";
        var points = ObjectDection.getObjectBoundingBox(selected_object, cam);
        createText = counter+","+points[0]+"," + points[1]+","+points[2]+"," +points[3] +"," + label +"\n";
        File.AppendAllText(path, createText);
    }
    void TakeScreenShot(){
    	ScreenCapture.CaptureScreenshot(string.Format("dataset/screenshot{0}.png", counter), superSize);
    }

}

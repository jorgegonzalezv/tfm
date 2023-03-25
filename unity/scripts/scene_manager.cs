using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;
using System.IO;

public class scene_manager : MonoBehaviour
{
    int counter = 0;

    // camera
    public Camera cam;
    public GameObject camera;
    public Vector3 center_point = new Vector3(7.0f, 0.0f, -4.0f);
    public float delta_t = 0.3f;

    // ocean
    public GameObject ocean; 
    private OceanRenderer oceanRenderer;

    // scene objects
    public GameObject boat1; 
    public GameObject boat2;
    public GameObject person;
    public List<GameObject> objects = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        // 0. init periodic tasks
        InvokeRepeating("SaveGroundTruthAll", 2.0f, 1.0f);   

        // 0. init variables
		    objects.Add(boat1);
        objects.Add(boat2);
		    objects.Add(person);

        // set unvisible bounding spheres
        for(int i=0; i<objects.Count; i++){
            objects[i].transform.Find("Bounding").gameObject.GetComponent<Renderer>().enabled = false;
        }
        
        oceanRenderer = ocean.GetComponent<OceanRenderer>(); 

        // 1. set up ocean conditions
        oceanRenderer._globalWindSpeed = 150;

        // 2. set up scene objects
        boat1.transform.position = new Vector3(0.0f, 0.0f, -4.0f);
        boat2.transform.position = new Vector3(7.0f, 0.0f, -4.0f);
        person.transform.position = new Vector3(14.0f, 0.0f, -4.0f);
        
        // set unvisible non-participant objects
        //foo.GetComponent<Renderer>().enabled = false;

        // 3. define dron flight
        camera.GetComponent<camera_follow>().target.position = center_point;
        camera.GetComponent<camera_follow>().offset = new Vector3(0.0f, 30.0f, -100.0f);
    }

    // Update is called once per frame
    void Update()
    { 
        // move camera as a drone flight
        if (camera.GetComponent<camera_follow>().offset[2] >= 100){
            camera.GetComponent<camera_follow>().offset = new Vector3(0.0f, 30.0f, -100.0f);
        }

        camera.GetComponent<camera_follow>().offset = camera.GetComponent<camera_follow>().offset + new Vector3(0.0f, 0.0f, 1.0f) * delta_t;
    }

    void Reset(){
        // reset scene with random params
    }

    void SaveGroundTruthAll(){
        // update auxiliary bounding boxes
        //UpdateBounding();

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
        //var selected_object = objects[gameobject_id];
        // -- opcion 1 --
        if (1 == 0){
            Vector3 screenPos = cam.WorldToScreenPoint(selected_object.transform.position);
            var bounds = selected_object.GetComponent<Renderer>().bounds;
            //var bounds = selected_object.bounds;

            Debug.Log("Bounds: center: "+ bounds.center+", extents:"+bounds.extents+" ::: "+selected_object.transform.position+"\n");      
            var pcen =  cam.WorldToScreenPoint(bounds.center);
            var pmax = cam.WorldToScreenPoint(bounds.center + bounds.extents);
            var pmin = cam.WorldToScreenPoint(bounds.center - bounds.extents);

            //var createText = counter+","+screenPos.x +"," +screenPos.y+"\n";
            createText = counter+","+pmax.x +"," +pmax.y+","+pmin.x +"," +pmin.y+","+pcen.x +"," +pcen.y+"\n";
        }

        if (1 == 1){
            // -- opcion 2 --
            var points = Get_object_bounding_box(selected_object, cam);
            createText = counter+","+points[0]+"," +points[1]+","+points[2]+"," +points[3]+"\n";
        }

        File.AppendAllText(path, createText);
    }

    // Credit to: https://gist.github.com/ZackAkil/ce6604fd5ac008756f938047ce73d9d5
    private List<float> Get_object_bounding_box(GameObject game_object, Camera cam)
    {
        // This is a relativly intense way as it is looking at each mesh point to calculate the bounding box.
        // but it produces perfect bounding boxes so yolo.

        // get the mesh points
        // Vector3[] vertices = game_object.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] vertices = game_object.GetComponent<MeshFilter>().mesh.vertices;

        // apply the world transforms (position, rotation, scale) to the mesh points and then get their 2D position
        // relative to the camera
        Vector2[] vertices_2d = new Vector2[vertices.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices_2d[i] = cam.WorldToScreenPoint(game_object.transform.TransformPoint( vertices[i]));
        }

        // find the min max bounds of the 2D points
        Vector2 min = vertices_2d[0];
        Vector2 max = vertices_2d[0];
        foreach (Vector2 vertex in vertices_2d)
        {
            min = Vector2.Min(min, vertex);
            max = Vector2.Max(max, vertex);
        }

        List<float> points = new List<float>();
        points.Add(min.x);
        points.Add(min.y);
        points.Add(max.x);
        points.Add(max.y);
        return points;
    }

    // void UpdateBounding(){
    //     var bounding_sphere = person.transform.Find("Bounding").gameObject;
    //     // bounding_sphere.transform.position = person.transform.position + Vector3.up;
    //     //bounding_sphere.transform.position = person.GetComponent<Renderer>().bounds.center;

    //     //bounding_sphere.GetComponent<Renderer>().enabled = false;

    // }
}

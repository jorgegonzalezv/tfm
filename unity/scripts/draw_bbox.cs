using UnityEngine;
using System.IO;


public class draw_bbox : MonoBehaviour
{
    public GameObject Cube;
    private int counter = 0;
    void Start()
    {
        InvokeRepeating("Update", 2.0f, 1.0f);   
        var fileName = "draw_bbox_log.txt";
        var sr = File.CreateText(fileName);
        sr.WriteLine ("This is my file.");
        sr.Close();
    }

    // Update is called once per frame
    void Update()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(3,0,0), 10); 
        //ScreenCapture.CaptureScreenshot("dataset/SomeLevel" + str(counter)+".png");
        //counter = 1 + counter;
        // screenshot

        // save to file bbox

    }
    //var r = obj.GetComponent<Renderer>();
    //var bounds = r.bounds;
    //Gizmos.matrix = Matrix4x4.identity;
    //Gizmos.color = Color.blue;
    
    //Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);

    //var fileName = "draw_bbox_log.txt";
    //var sr = File.CreateText(fileName);
    //sr.WriteLine ("This is my file.");
    //sr.Close();
    
}
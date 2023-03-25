using UnityEngine;

public class screen_shot : MonoBehaviour
{
    int counter = 0;
    int superSize = 4;

    void Start()
    {
    	InvokeRepeating("TakeScreenShot", 2.0f, 1.0f);   
    }
    void TakeScreenShot()
    {
    	ScreenCapture.CaptureScreenshot(string.Format("dataset/screenshot{0}.png", counter), superSize);
        //Debug.Log(string.Format("A screenshot was taken: {0}!", counter));
        counter = counter + 1;
    }
}

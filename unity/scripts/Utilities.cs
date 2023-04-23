using UnityEngine;
using System.Collections.Generic;

namespace tfm {

    public class ObjectDection {

        /* 
        Get bounding box around given game object. 
        Credit to: https://gist.github.com/ZackAkil/ce6604fd5ac008756f938047ce73d9d5
        */
        public static List<float> getObjectBoundingBox(GameObject game_object, Camera cam)
        {
            // This is a relativly intense way as it is looking at each mesh point to calculate the bounding box.
            // but it produces perfect bounding boxes so yolo.

            // get the mesh points
            // Vector3[] vertices = game_object.GetComponent<MeshFilter>().mesh.vertices;
            Vector3[] vertices = game_object.GetComponent<MeshFilter>().mesh.vertices;

            // apply the world transforms (position, rotation, scale) to the mesh points and then get their 2D position
            // relative to the camera
            Vector2[] vertices_2d = new Vector2[vertices.Length];
            for (var i = 0; i < vertices.Length; i++){
                vertices_2d[i] = cam.WorldToScreenPoint(game_object.transform.TransformPoint( vertices[i]));
            }

            // find the min max bounds of the 2D points
            Vector2 min = vertices_2d[0];
            Vector2 max = vertices_2d[0];
            foreach (Vector2 vertex in vertices_2d){
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
    }


    public class Randomize {
        
        // public static int randomInt(int lower, int upper){
        //     return Random.Range(lower, upper);
        // }

        public static float randomFloat(float lower, float upper){
            return Random.Range(lower, upper);
        }
    }


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreePlacer : EditorWindow
{    
    public int numberOfObjects; // number of objects to place    
    public GameObject[] objectsToPlace; // GameObject to place
    public Vector2 scaleRange;


    [MenuItem("ZacTools/Place Trees")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        GetWindow(typeof(TreePlacer));
    }

    private void OnGUI()
    {
        numberOfObjects = EditorGUILayout.IntField("Number of objects", numberOfObjects);
        scaleRange = EditorGUILayout.Vector2Field("Scale range", scaleRange);

        //https://answers.unity.com/questions/859554/editorwindow-display-array-dropdown.html
        SerializedObject so = new SerializedObject(this);
        SerializedProperty stringsProperty = so.FindProperty("objectsToPlace");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties

        if (GUILayout.Button("Place!"))
        {
            Place();
        }
    }

    public void Place()
    {
        //Destroy old instance
        var old = GameObject.Find("Trees");
        if (old) DestroyImmediate(old);        

        //Create daddy
        var parent = new GameObject("Trees");

        //Get terrain
        var terrain = FindObjectOfType<Terrain>();

        var terrainWidth = (int)terrain.terrainData.size.x;
        var terrainLength = (int)terrain.terrainData.size.z;
        
        var terrainPosX = (int)terrain.transform.position.x;
        var terrainPosZ = (int)terrain.transform.position.z;

        //Spawn objs
        int placedObjects = 0;
        int loopBreak = 0;
        while(placedObjects < numberOfObjects)
        {
            //We've placed too many trees!
            loopBreak++;
            if (loopBreak > 5000) return;

            int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);            
            int posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength);

            // get the terrain height at the random position
            float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));

            //Check if valid spawn
            var placepos = new Vector3(posx, posy, posz);
            if (Physics.OverlapSphere(placepos, 1).Length > 1) continue;

            //Get random rotation
            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);

            GameObject newObject = Instantiate(objectsToPlace[Random.Range(0, objectsToPlace.Length)], new Vector3(posx, posy, posz), rot);
            newObject.transform.parent = parent.transform;
            newObject.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
            placedObjects++;
        }
    }
}

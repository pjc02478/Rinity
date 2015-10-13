using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

//using Rinity;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

[CustomEditor(typeof(TestScript))]
public class TestEditor : Editor
{
    public bool init = false;


    private void Load()
    {
        Console.WriteLine("REQ");

        var src = "{\"src\" : \"class AA{public void Foo(){}}\"}";
        var data = Encoding.UTF8.GetBytes(src);
        var w = new WWW("http://localhost:9900/ca/parse_one", data, new Dictionary<string, string>() {
            { "Content-Type", "application/json" }
        });

        Console.WriteLine("REQ");
        while (w.isDone == false) ;
        Debug.Log(w.text);
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("A"))
        {
            Load();
        }

    }
}

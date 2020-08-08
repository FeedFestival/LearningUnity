using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		GUILayout.BeginArea(new Rect(Screen.width /2 - 100, Screen.height /2 -100, 200, 200));
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Start Game")){
			Debug.Log("Click");
		}
		GUILayout.Button("Select Level");
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
		
	}
}

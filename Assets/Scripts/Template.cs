// Template.cs
// Author : Fragmads
// Package : Misc
// Last modification date : 19/08/2012
//
// Template : This script aim to present the structure of a Unity script as it will be used in Soul Wars
// I still recommand spending some time on tutorial or searching for forums if it is needed, or if I don't already have the answer.
// 
// State : Uncomplete



using UnityEngine;
using System.Collections;
// Add this one to use List<T> 
using System.Collections.Generic;

// An unity object/component is son of MonoBehaviour.
public class Template : MonoBehaviour {
	
	
	// Properties
	//
	
	// Property visible and editable directly from the editor
	public int TestInt = 10;
	
	// List can also be easily edited from the editor
	public List<GameObject> TestArray = null;  
	
	// Private property, to be used only inside this class
//	private string TestString = "test";
	
	
	// Methods
	//
	
	// Use this for initialization
	void Start () {
			// Note that you should'nt use new directly to create an Unity object
			// Therefore, the Start methods allow you to execute operation at the creation of an object
			// since you are not supposed to overload the "new" method
		
		
		// If the test value hasn't been changed from the editor
		if (this.TestInt != 10){
			
			// I make a Debug comment
			Debug.Log("Template.Start () : You didn't changed TestInt value");
						
		}
		
	}
	
		
	// Update is called once per frame
	void Update () {
	
		
		int i = 0;
		// While i get over 9000
		while(i < 9000){
			
			// Increment i
			i++;
			
			
			
			// Lol 11+!!!1 :) this code is supposed to execute itself on a 60Hz base, for every game object that has this script attached
			// And I'am just chillin over 9000, LOL that will not spoil ressources at all
			
			// In other word, do what you must do on a per frame basis inside Update () and absolutely nothing else.
			
		}
				
		
		
		// Debug.Log("Template.Update () : Please take time to name your Debug line correctly, and don't forget to comment them once you're done," +
		//	 	"especially if they happen during Update () ");
		
				
		
	}
	
	
	// Some other methods are checked by Unity, see Monobehaviour doc.
	// GUI events are handled here.
	void OnGUI(){
		
	}
	
	
	
	// Custom methods	
	//
	
	/// <summary>
	/// Selects the random point.
	/// </summary>
	/// <returns>
	/// The random point.
	/// </returns>
	public Vector2 SelectRandomPoint (){
		
		return new Vector2 (0,0);
	}
	
		
	
}

// Camera.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 04/09/2012
//
// Camera : Script managing the movement and the focus of the in game camera
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VSCamera : MonoBehaviour {
	
	// Properties
	//
	
	public List<AEntity> InterestEntity;
	
	public float maxX = 15;
	public float maxY = 15;
	public float maxZ;
	
	public float FoVMax = 70;
	public float FoVMin = 50;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		// Make this camera Look at the orientation point
		Vector2 orientationPoint = this.GetOrientationPoint();
		
		//Debug.Log("VSCamera - Orientation : x "+orientationPoint.x+" y "+orientationPoint.y);
		
		this.camera.transform.LookAt( new Vector3(orientationPoint.x, orientationPoint.y, 0));
		
		// Adjust the FoV according to the distance between interest point
		float maxDist = 0;
		
		// Search the max distance
		foreach(AEntity e in this.InterestEntity){
			
			maxDist = Mathf.Max(maxDist, Vector2.Distance(orientationPoint, new Vector2(e.gameObject.transform.position.x, e.gameObject.transform.position.y)));
			
		}
		
		// Calculate the new Field of View
		maxDist = Mathf.Min(maxDist, this.maxX);
		
		float wantedFoVPercent = (maxDist*100)/this.maxX;
		
		float fov = (wantedFoVPercent*(this.FoVMax - this.FoVMin))/ 100; 
		
		// Apply the new FoV
		this.camera.fov = fov + this.FoVMin;
				
	}
	
	private Vector2 GetOrientationPoint(){
		
		if(this.InterestEntity.Count != 0){
			List<Vector2> pointOfInterest = new List<Vector2>();
			
			// For every entity of interest
			foreach(AEntity e in this.InterestEntity){
				
				// Point of interest can't go out of camera bounds
				float x1, y1 = 0;
			
				x1 = Mathf.Max(e.gameObject.transform.position.x, -this.maxX);
				x1 = Mathf.Min(x1, this.maxX);
				
				y1 = Mathf.Max(e.gameObject.transform.position.y, -this.maxY);
				y1 = Mathf.Min(y1, this.maxY);
				
				// Add the point of interest to the list
				pointOfInterest.Add(new Vector2(x1, y1));
				
			}
			
			float averageX = 0;
			float averageY = 0;
			
			// The point of interest is the average of every point of interest
			foreach(Vector2 v in pointOfInterest){
				
				averageX += v.x;
				averageY += v.y;
				
			}
			
			// Return the orientation point
			return new Vector2(averageX/pointOfInterest.Count, averageY/pointOfInterest.Count); 
		}
		else{
			
			return new Vector2(0,0);
			
		}
		
	} 
	
	
}

﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
                   
public class Tiling : MonoBehaviour {
	
	//marge d'erreur 
	public int offsetX = 2;
	
	//permet de savoir si il y a deja un copie droite ou gauche 
	public bool hasARightBuddy = false; 
	public bool hasALeftBuddy = false;
	
	public bool reverseScale = false;
	
	//longueur de notre element (sprite)
	private float spriteWidth = 0f; 
	private Camera cam;
	// permet d'avoir une variable contenant la main  camera  pour de meilleur performenceera cam; 
	private Transform myTransform;
	
	void Awake(){
		cam = Camera.main;
		myTransform = transform;
		
		
	}
	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(hasALeftBuddy == false || hasARightBuddy == false ){
			
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
			
			float edgeVisiblePositionRight= (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft= (myTransform.position.x - spriteWidth/2)  + camHorizontalExtend;
			
			if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false){
				
				MakeNewBuddy(1);
				hasARightBuddy= true;
			}else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy ==false){
				MakeNewBuddy(-1);
				hasALeftBuddy= true;
			}
		}
	}
	
	void MakeNewBuddy(int rightOrLeft){
		
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y,myTransform.position.z);
		
		Transform newBuddy = (Transform)Instantiate (myTransform,newPosition,myTransform.rotation);
		if( reverseScale == true){
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*(-1),newBuddy.localScale.y,newBuddy.localScale.z);
		}
		               
		newBuddy.parent=myTransform.parent;
		
		if(rightOrLeft > 0){
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}else {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	
	public int menuid;
	public GameObject gcamera;
	public AudioClip menu_hover;
	public Object[] image;
	
	void Start()
	{
		renderer.material.mainTexture = (Texture)image[0];
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}
	
	// Use this for initialization
	void OnMouseEnter () {
		renderer.material.mainTexture = (Texture)image[1];
		gcamera.audio.PlayOneShot(menu_hover);
	}
	
	// Update is called once per frame
	void OnMouseExit () {
		renderer.material.mainTexture = (Texture)image[0];
	}
	
	void OnMouseDown()
	{
		if(menuid == 0) Application.LoadLevel(2);
		else if(menuid == 1) Application.LoadLevel(3);
	}
}
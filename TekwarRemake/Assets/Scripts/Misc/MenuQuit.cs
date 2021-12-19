using UnityEngine;
using System.Collections;

public class MenuQuit : MonoBehaviour {
	
	public Camera maincam;
	public AudioClip sound;
	bool isactive = false;
	bool blocked = false;
	
	void Start()
	{
		if(GameObject.Find("intro")) blocked = true;
	}
	
	void FixedUpdate ()
	{
		if(blocked && !GameObject.Find("intro")) StartCoroutine(Wait());
		
		if(Input.GetKey(KeyCode.Escape))
		{
			if(!isactive && !blocked)
			{
				renderer.enabled = true;
				isactive = true;
				maincam.audio.PlayOneShot(sound);
			}
		}
		else if(Input.GetKey(KeyCode.Y) && isactive)
		{
			Application.Quit();
		}
		else if(Input.GetKey(KeyCode.N) && isactive)
		{
			renderer.enabled = false;
			isactive = false;
		}
	}
	
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.0f);
		blocked = false;
	}
}

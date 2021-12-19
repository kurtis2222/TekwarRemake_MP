using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour {

	public Camera maincam;
	MovieTexture movie;
	public AudioClip menu_music;
		
	void Start ()
	{
		movie = renderer.material.mainTexture as MovieTexture;
		audio.clip = movie.audioClip;
		audio.Play();
		movie.Play();
		StartCoroutine(EndIntro());
	}
	
	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			maincam.transform.rotation = new Quaternion(0,0,0,0);
			maincam.audio.clip = menu_music;
			maincam.audio.Play();
			StopCoroutine("EndIntro");
			Destroy(gameObject);
		}
	}
	
	IEnumerator EndIntro()
	{
		yield return new WaitForSeconds(71.0f);
		maincam.transform.rotation = new Quaternion(0,0,0,0);
		maincam.audio.clip = menu_music;
		maincam.audio.Play();
		Destroy(gameObject);
	}
}

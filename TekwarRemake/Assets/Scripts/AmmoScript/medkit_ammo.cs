using UnityEngine;
using System.Collections;

public class medkit_ammo : MonoBehaviour {
	
	public GUIText hud_info;
	GameObject playerobj;
	public AudioClip pick_snd;

    void FixedUpdate()
    {
		if(playerobj == null)
			playerobj = GameObject.Find("Player(Clone)");
		else
		{
			if(Vector3.Distance(transform.position,playerobj.transform.position) < 1.0f &&
				playerobj.networkView.isMine && gameObject.renderer.enabled)
			{
				if(playerobj.GetComponent<MainScript>().hp < 100 ||
					playerobj.GetComponent<MainScript>().cos < 100)
				{
				hud_info.enabled = true;
				hud_info.text = "MEDIC KIT";
				playerobj.audio.PlayOneShot(pick_snd);
				playerobj.GetComponent<MainScript>().hp = 100;
				playerobj.GetComponent<MainScript>().cos = 100;
				playerobj.GetComponent<MainScript>().count = 3;
				StartCoroutine(Respawn());
				gameObject.renderer.enabled = false;
				}
			}
		}
	}
	
	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(90.0f);
		gameObject.renderer.enabled = true;
	}
}

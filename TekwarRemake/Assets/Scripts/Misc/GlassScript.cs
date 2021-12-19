using UnityEngine;
using System.Collections;

public class GlassScript : MonoBehaviour {
	
	public Mesh broken_mesh;
	public AudioClip glass_snd;
	
	void Start()
	{
		
	}
	
	public void Break()
	{
		networkView.RPC("BreakMP",RPCMode.All);
	}
	
	[RPC]
	void BreakMP()
	{
		collider.enabled = false;
		audio.PlayOneShot(glass_snd);
		GetComponent<MeshFilter>().mesh = broken_mesh;
	}
}

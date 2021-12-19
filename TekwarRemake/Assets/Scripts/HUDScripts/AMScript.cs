using UnityEngine;
using System.Collections;

public class AMScript : MonoBehaviour {
	
	GameObject playerobj;
	float tmp;
	float am;
	int current;
	float am_check;
	float am_max;
	
	void Start()
	{
		am_max = guiTexture.pixelInset.width;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
		if(playerobj == null)
			playerobj = GameObject.Find("Player(Clone)");
		else if(playerobj != null)
		{
			current = playerobj.GetComponentInChildren<WeaponScript>().currweap;
			if(current != 0)
			{
			am = playerobj.GetComponentInChildren<WeaponScript>().ammo[current-1];
			if(am_check != am)
			{
			tmp = am / 100 * am_max;
			guiTexture.pixelInset = new Rect(
				guiTexture.pixelInset.x,
				guiTexture.pixelInset.y,
				tmp,
				guiTexture.pixelInset.height);
			}
			current = playerobj.GetComponentInChildren<WeaponScript>().currweap;
			am_check = playerobj.GetComponentInChildren<WeaponScript>().ammo[current-1];
			}
			else
			{
				am = 0;
				am_check = 0;
				guiTexture.pixelInset = new Rect(
				guiTexture.pixelInset.x,
				guiTexture.pixelInset.y,
				0,
				guiTexture.pixelInset.height);
			}
		}
	}
}
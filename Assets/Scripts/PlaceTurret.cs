using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
	[SerializeField]
	GameObject turretPF1, turretPF2;

	GameObject selectedTurret;
	GameObject turret;

	bool TurretPlaceable()
	{
		return turret == null;
	}

    private void Update()
    {
		if (Input.GetKeyDown("a"))
			selectedTurret = turretPF1;
		if (Input.GetKeyDown("s"))
			selectedTurret = turretPF2;
    }

    private void OnMouseUp()
	{
		if (TurretPlaceable())
		{
			turret = (GameObject)Instantiate(selectedTurret, transform.position, Quaternion.identity);
			
			AudioSource placeSound = gameObject.GetComponent<AudioSource>();
			placeSound.PlayOneShot(placeSound.clip);

			// TODO: Reduce Cash
		}
	}
}

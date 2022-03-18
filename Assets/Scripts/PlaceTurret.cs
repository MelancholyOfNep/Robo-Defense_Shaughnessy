using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
	GameManager gameManager;

	[SerializeField]
	GameObject turretPF1, turretPF2, turretPF3, turretPF4;

	[SerializeField]
	string Turret1Key = "a", Turret2Key = "s", Turret3Key = "d", Turret4Key = "f";

	GameObject selectedTurret;
	GameObject turret;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		selectedTurret = turretPF1;
		gameManager.SelectedUnit(0);
	}

	bool TurretPlaceable()
	{
		return turret == null;
	}

	private void Update()
	{
		if (Input.GetKeyDown(Turret1Key))
		{
			selectedTurret = turretPF1;
			gameManager.SelectedUnit(0);
		}

		if (Input.GetKeyDown(Turret2Key))
        {
			selectedTurret = turretPF2;
			gameManager.SelectedUnit(1);
		}

		if (Input.GetKeyDown(Turret3Key))
		{
			selectedTurret = turretPF3;
			gameManager.SelectedUnit(2);
		}

		if (Input.GetKeyDown(Turret4Key))
		{
			selectedTurret = turretPF4;
			gameManager.SelectedUnit(3);
		}
	}

	private void OnMouseUp()
	{
		if (TurretPlaceable()
			&& gameManager.money >= selectedTurret.GetComponent<TurretData>().cost
			&& selectedTurret != null) // make sure this works
		{
			turret = (GameObject)Instantiate(selectedTurret, transform.position, Quaternion.identity);
			
			AudioSource placeSound = gameObject.GetComponent<AudioSource>();
			placeSound.PlayOneShot(placeSound.clip);

			gameManager.money -= selectedTurret.GetComponent<TurretData>().cost;
		}

		else if (gameManager.money < selectedTurret.GetComponent<TurretData>().cost) // idk if this is right, check that it works
        {
			gameManager.TriggerAlert(0);
        }
		else if (TurretPlaceable() == false) // idk if this is right, check that it works
		{
			gameManager.TriggerAlert(1);
		}
	}
}

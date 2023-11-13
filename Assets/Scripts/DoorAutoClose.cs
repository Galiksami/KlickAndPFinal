using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAutoClose : MonoBehaviour
{
	public Animator doorAnimator;
	private bool isDoorOpen = false;
	public float autoCloseDelay = 8.0f; // Seconds to wait before auto-closing the door

	// If the Animator is not set, try to get it on start
	void Start()
	{
		if (doorAnimator == null)
		{
			doorAnimator = GetComponent<Animator>();
		}
	}

	// Method to open the door and start the auto-close coroutine
	public void OpenDoor()
	{
		isDoorOpen = true;
		doorAnimator.SetBool("IsOpen", true);
		StopAllCoroutines(); // Stop any existing auto-close coroutines
		StartCoroutine(CloseDoorAfterDelay()); // Start the auto-close coroutine
	}

	// Coroutine to close the door after a delay
	private IEnumerator CloseDoorAfterDelay()
	{
		yield return new WaitForSeconds(autoCloseDelay);
		isDoorOpen = false;
		doorAnimator.SetBool("IsOpen", false);
	}

	// Method to directly close the door (e.g., called by the DoorController when the door is manually closed)
	public void CloseDoor()
	{
		isDoorOpen = false;
		doorAnimator.SetBool("IsOpen", false);
		StopAllCoroutines(); // Stop the auto-close coroutine, since we're closing the door manually
	}

	// You can call this method to check if the door is open
	public bool IsDoorOpen()
	{
		return isDoorOpen;
	}
}

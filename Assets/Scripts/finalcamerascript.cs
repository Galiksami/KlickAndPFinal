using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalcamerascript : MonoBehaviour
{
	public Transform player; // The player object the camera will orbit around
	public float rotationSpeed = 100.0f; // Speed of rotation around the player
	public float zoomSpeed = 10f; // The speed at which the camera zooms
	public float minZoom = 5f; // Minimum zoom distance
	public float maxZoom = 20f; // Maximum zoom distance

	private Vector3 _offset; // The initial offset from the player

	void Start()
	{
		// Calculate the initial offset from the player
		_offset = transform.position - player.position;
	}

	void Update()
	{
		// Rotation input from keyboard
		float horizontalInput = 0f;
		if (Input.GetKey(KeyCode.A))
		{
			horizontalInput = 1f; // Rotate left
		}
		else if (Input.GetKey(KeyCode.D))
		{
			horizontalInput = -1f; // Rotate right
		}

		// Rotation input from the middle mouse button
		if (Input.GetMouseButton(2)) // Middle mouse button is indexed as 2
		{
			horizontalInput = Input.GetAxis("Mouse X");
		}

		// Apply rotation
		if (horizontalInput != 0)
		{
			Quaternion rotation = Quaternion.Euler(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
			_offset = rotation * _offset;
		}

		// Handle zoom with mouse scroll wheel
		float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		_offset += _offset.normalized * scroll;
		_offset = Vector3.ClampMagnitude(_offset, maxZoom);

		// Ensure the camera doesn't go inside the player model
		if (_offset.magnitude < minZoom)
		{
			_offset = _offset.normalized * minZoom;
		}

		// Set camera position based on new offset and look at the player
		transform.position = player.position + _offset;
		transform.LookAt(player.position);
	}
}

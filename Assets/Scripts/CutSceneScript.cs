using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public BoxCollider triggerCollider;
    public GameObject bookObject; // Reference to the book GameObject.
    public float proximityDistance = 3f;

    private bool hasCutscenePlayed = false;

    private void Start()
    {
        // Ensure the cutscene director is not null before using it.
        if (cutsceneDirector == null)
        {
            cutsceneDirector = GetComponent<PlayableDirector>();
        }

        if (cutsceneDirector == null)
        {
            Debug.LogError("Cutscene director not assigned or found. Attach a PlayableDirector component or assign it in the inspector.");
        }

        // Initially hide the book.
        if (bookObject != null)
        {
            bookObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Book GameObject not assigned. Attach a GameObject or assign it in the inspector.");
        }
    }

    private void Update()
    {
        // Check if the player is near the trigger collider.
        if (IsPlayerNearCollider() && !hasCutscenePlayed)
        {
            // Check if the 'E' key is pressed using Unity's Input System.
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // Play the cutscene.
                PlayCutscene();

                // Show the book after the cutscene is played.
                if (bookObject != null)
                {
                    bookObject.SetActive(true);
                }
            }
        }
    }

    private bool IsPlayerNearCollider()
    {
        // Check if the triggerCollider is assigned.
        if (triggerCollider != null)
        {
            // Calculate the distance between the player and the collider center.
            float distance = Vector3.Distance(transform.position, triggerCollider.bounds.center);

            // Check if the player is within the specified proximity distance.
            return distance <= proximityDistance;
        }
        else
        {
            Debug.LogError("Trigger collider not assigned. Attach a BoxCollider component or assign it in the inspector.");
            return false;
        }
    }

    private void PlayCutscene()
    {
        // Check if the cutscene director is assigned.
        if (cutsceneDirector != null)
        {
            // Play the cutscene.
            cutsceneDirector.Play();
            hasCutscenePlayed = true; // Set the flag to true to indicate that the cutscene has been played.
        }
        else
        {
            Debug.LogError("Cutscene director not assigned. Attach a PlayableDirector component or assign it in the inspector.");
        }
    }
}

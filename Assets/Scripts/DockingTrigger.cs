using UnityEngine;
using UnityEngine.Playables;

public class DockingTrigger : MonoBehaviour
{
    [Header("References (Assign in Inspector)")]
    [SerializeField] private PlayableDirector inGameDirector;  // Your end Timeline
    [SerializeField] private Collider dockingCollider;  // Assign child SphereCollider (isTrigger=true, radius ~20-50 for "above" zone)
    [SerializeField] private float dockingHeightOffset = 5f;  // Extra check: player must be ABOVE transport by this much

    [Header("Docking Zone Setup")]
    [SerializeField] private LayerMask playerLayerMask = -1;  // Default: all layers

    private bool hasDocked = false;
    private Player playerRef;  // Cached

    private void Start()
    {
        // Initially disable collider to prevent accidental docking before spawn
        if (dockingCollider != null)
            dockingCollider.enabled = false;

        // Cache player (assumes single Player in scene)
        playerRef = FindObjectOfType<Player>();
        if (playerRef == null)
            Debug.LogError("No Player found for docking!", this);
    }

    // Method called from InGameTrigger OnTriggerEnter (after enabling renderers)
    public void EnableDockingZone()
    {
        if (dockingCollider != null)
            dockingCollider.enabled = true;
        Debug.Log("Docking zone activated — approach from above!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasDocked || !other.CompareTag("Player")) return;

        // Double-check: is player ABOVE transport?
        float playerY = other.transform.position.y;
        float transportY = transform.position.y;
        if (playerY < transportY + dockingHeightOffset)
        {
            Debug.LogWarning($"Player too low (y={playerY}, need >{transportY + dockingHeightOffset}) — adjust approach!");
            return;
        }

        // Valid docking!
        hasDocked = true;
        DockAndStartCutscene();
    }

    private void DockAndStartCutscene()
    {
        if (playerRef == null || inGameDirector == null)
        {
            Debug.LogError("Missing player or director for docking!", this);
            return;
        }

        // 1. Disable player control
        playerRef.DisableControl();  // Assumes your Player script has this

        // 2. Attach to transport
        playerRef.AttachToTransport(transform);

        // 3. Play fly-away Timeline
        inGameDirector.Play();

        // 4. Disable docking zone
        if (dockingCollider != null)
            dockingCollider.enabled = false;

        Debug.Log("DOCKED! Cutscene started — transport flying away.");
    }
}

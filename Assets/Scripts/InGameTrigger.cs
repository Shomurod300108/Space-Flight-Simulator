using UnityEngine;
using UnityEngine.Playables;

public class InGameTrigger : MonoBehaviour
{
    [Header("References (Assign in Inspector)")]
    [SerializeField] private Player player;  // Not used here anymore, but keep if needed elsewhere
    [SerializeField] private GameObject transportShip;  // Root GO
    // Removed: inGameDirector — moved to DockingTrigger
    // Note: PlayableDirector should now be assigned to transportShip's DockingTrigger script

    private Renderer[] renderers;
    private bool played = false;  // Repurposed: now for spawn only

    private void Start()
    {
        if (transportShip == null) 
        {
            Debug.LogError("Transport ship reference is null!", this);
            return;
        }

        // Ensure root is active (for Timeline later)
        transportShip.SetActive(true);
        
        // Hide via renderers only
        renderers = transportShip.GetComponentsInChildren<Renderer>(true);
        foreach (var r in renderers)
        {
            if (r != null) r.enabled = false;
        }

        // Optional: Position transport above trigger/player if needed
        // transportShip.transform.position = transform.position + Vector3.up * 50f;  // Adjust height
    }

    private void OnTriggerEnter(Collider other)
    {
        if (played || !other.CompareTag("Player")) return;
        played = true;

        // 1. Enable visibility
        foreach (var r in renderers)
        {
            if (r != null) r.enabled = true;
        }

        // 2. Enable docking detection (if using collider method — see below)
        // DockingTrigger will handle attach + Timeline

        Debug.Log("Transport spawned — fly to docking zone above it!");
    }
}


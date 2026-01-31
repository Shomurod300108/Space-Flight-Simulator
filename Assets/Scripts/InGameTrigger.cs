using UnityEngine;
using UnityEngine.Playables;

public class InGameTrigger : MonoBehaviour
{
    [Header("References (Assign in Inspector)")]
    [SerializeField] private GameObject transportShip;
    [SerializeField] private PlayableDirector inGameDirector;

    private bool played = false;

    private void Start()
    {
        if (transportShip != null)
            transportShip.SetActive(false);

        if (inGameDirector != null)
            inGameDirector.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!played && other.CompareTag("Player"))
        {
            played = true;

            if (transportShip != null)
                transportShip.SetActive(true);

            if (inGameDirector != null)
                inGameDirector.Play();

            Debug.Log("In-game cutscene triggered");
        }
    }
}


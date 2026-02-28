using UnityEngine;
using UnityEngine.Playables;

public class InGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _transportShip;      
    [SerializeField] private PlayableDirector _inGameDirector; 

    private bool _hasTriggered = false;

    private void Awake()
    {
        if (_transportShip != null)
            _transportShip.SetActive(false);  

        if (_inGameDirector != null)
        {
            _inGameDirector.gameObject.SetActive(false);  
        }
        else
        {
            Debug.LogError("PlayableDirector not assigned in InGameTrigger!", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered || !other.CompareTag("Player"))
            return;

        _hasTriggered = true;

        if (_transportShip != null)
            _transportShip.SetActive(true); 

        if (_inGameDirector != null)
        {
            _inGameDirector.gameObject.SetActive(true);
            _inGameDirector.Play();  
            Debug.Log("Cutscene started via trigger");
        }
    }
}

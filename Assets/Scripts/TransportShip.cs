using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportShip : MonoBehaviour
{
    [SerializeField] private GameObject _endDirector;
    [SerializeField] private GameObject _restartButton;
    
    private void Start()
    {
        if (_endDirector != null)
            _endDirector.SetActive(false);

        if (_restartButton != null)
            _restartButton.SetActive(false);
        

    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            _endDirector.SetActive(true);
            _restartButton.SetActive(true);
        }
    }
}

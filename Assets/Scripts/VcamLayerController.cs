using UnityEngine;
using Cinemachine;

public class VcamLayerController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;  
    [SerializeField] private CinemachineVirtualCamera _exteriorVcam;  
    [SerializeField] private CinemachineVirtualCamera _interiorVcam;  
    [SerializeField] private GameObject _cockpitRoot; 

    private bool _isInteriorView = false; 

    void Start()
    {
        SetExteriorView();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_isInteriorView)
            {
                SetExteriorView();
            }
            else
            {
                SetInteriorView();
            }
        }
    }

    private void SetInteriorView()
    {
        _interiorVcam.Priority = 30;   
        _exteriorVcam.Priority = 10;

        if (_cockpitRoot != null)
        {
            _cockpitRoot.SetActive(true);
        }

        _isInteriorView = true;
    }

    private void SetExteriorView()
    {
        _exteriorVcam.Priority = 30;
        _interiorVcam.Priority = 10;

        if (_cockpitRoot != null)
        {
            _cockpitRoot.SetActive(false);
        }

        _isInteriorView = false;
    }
}

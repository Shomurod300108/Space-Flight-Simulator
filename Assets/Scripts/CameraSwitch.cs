using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera _cockpitCam;
    public CinemachineVirtualCamera _thirdPersonCam;
    public CinemachineBlendListCamera _cinematicCam;
    public float _idleTime = 5f;
    private float _idleTimer;
    private bool _cinematicActive;
    private int _currentCam; 
    [SerializeField] private float _introCutSceneTime;
    [SerializeField] private PlayableDirector _introCutSceneDirector;

    void Start()
    {
        ActivateCockpit();

        _introCutSceneDirector = GameObject.Find("IntroCutScene").GetComponent<PlayableDirector>();
    }

    void Update()
    {
        HandleInput();
        HandleIdleCheck();
    }

    void HandleInput()
    {
        if (Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || _introCutSceneDirector.time < _introCutSceneTime)
        {
            _idleTimer = 0f;

            if (_cinematicActive)
            {
                ExitCinematic();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_currentCam == 0)
                ActivateThirdPerson();
            else
                ActivateCockpit();
        }
    }

    void HandleIdleCheck()
    {
        if (_cinematicActive) return;

        _idleTimer += Time.deltaTime;

        if (_idleTimer >= _idleTime)
        {
            EnterCinematic();
        }
    }

    void ActivateCockpit()
    {
        _cockpitCam.Priority = 20;
        _thirdPersonCam.Priority = 10;
        _cinematicCam.Priority = 0;
        _currentCam = 0;
        _cinematicActive = false;
    }

    void ActivateThirdPerson()
    {
        _thirdPersonCam.Priority = 20;
        _cockpitCam.Priority = 10;
        _cinematicCam.Priority = 0;
        _currentCam = 1;
        _cinematicActive = false;
    }

    void EnterCinematic()
    {
        _cinematicCam.Priority = 30;
        _cockpitCam.Priority = 0;
        _thirdPersonCam.Priority = 0;
        _cinematicActive = true;
    }

    void ExitCinematic()
    {
        if (_currentCam == 0)
            ActivateCockpit();
        else
            ActivateThirdPerson();
    }
}



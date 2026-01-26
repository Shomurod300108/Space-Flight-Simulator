using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _defaultForwardSpeed = 15f;
    [SerializeField] private float _speedChange = 10f;
    [SerializeField] private float _minSpeed = 5f;
    [SerializeField] private float _maxSpeed = 40f;
    [SerializeField] private float _pitchSpeed = 50f;
    [SerializeField] private float _yawSpeed = 50f;
    [SerializeField] private float _rollSpeed = 50f;
    [SerializeField] private float _maxPitchAngle = 45f;
    [SerializeField] private float _introCutSceneTime;
    [SerializeField] private PlayableDirector _introCutSceneDirector;
    public CinemachineBlendListCamera _introCutSceneCam;
    private float _currentSpeed;
    private float _pitch;
    private float _yaw;
    private float _roll;
    private bool _canControlShip = false;

    void Start()
    {
        _currentSpeed = _defaultForwardSpeed;
        _introCutSceneDirector = GameObject.Find("IntroCutScene").GetComponent<PlayableDirector>();
        _introCutSceneDirector.stopped += OnIntroFinished; 
    }

    void Update()
    {
        if (!_canControlShip)
            return;

        HandleRotation();
        HandleSpeed();
        MoveForward();
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.W)) _pitch += _pitchSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) _pitch -= _pitchSpeed * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, -_maxPitchAngle, +_maxPitchAngle);

        if (Input.GetKey(KeyCode.A)) _yaw -= _yawSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) _yaw += _yawSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q)) _roll += _rollSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) _roll -= _rollSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(_pitch, _yaw, _roll);
        transform.rotation = targetRotation;
    }

    private void HandleSpeed()
    {
        if (Input.GetKey(KeyCode.T))
            _currentSpeed += _speedChange * Time.deltaTime;

        if (Input.GetKey(KeyCode.G))
            _currentSpeed -= _speedChange * Time.deltaTime;

        _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime, Space.Self);
    }

    private void OnIntroFinished(PlayableDirector director)
    {
        _canControlShip = true;
        _introCutSceneCam.Priority = 0;
    }

    private void OnDestroy()
    {
        _introCutSceneDirector.stopped -= OnIntroFinished;
    }
}



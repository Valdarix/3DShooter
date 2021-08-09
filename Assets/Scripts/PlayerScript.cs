using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private Camera _mainCamera;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }
      
    void Update()
    {
        CameraFollow();  
        MovePlayer();
    }

    private void MovePlayer()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
      
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * playerSpeed);
      
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
      
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
      
        _playerVelocity.y += gravityValue * Time.deltaTime;

        _playerVelocity = transform.TransformDirection(_playerVelocity);
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void CameraFollow()
    {
        var xPos = Input.GetAxis("Mouse X");
        var yPos = Input.GetAxis("Mouse Y");

        var rot = transform.localEulerAngles;
        rot.y += xPos;
        transform.rotation = Quaternion.AngleAxis(rot.y, Vector3.up);

        var cameraRot = _mainCamera.gameObject.transform.localEulerAngles;
        cameraRot.x += yPos;
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(cameraRot.x, Vector3.right);

    }
}

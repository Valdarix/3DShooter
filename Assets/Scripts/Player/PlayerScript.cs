using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using Unity.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float mouseSensitivy = 0.5f;
    
    private CharacterController _controller;
    private Vector3 _dir;
    private Vector3 _velocity;
    private bool _groundedPlayer;
    private Camera _mainCamera;
    

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.Log("Character Controller is Null");
        }
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.Log("Main Camera is Null");
        }
        
        // Lock Cursor on Start
        ToggleMouseCursorLock();
    }
      
    void Update()
    {
        CameraFollow();  
        MovePlayer();
        if (Input.GetButtonDown("CursorToggle"))
        {
            ToggleMouseCursorLock();
        }
    }

    private static void ToggleMouseCursorLock()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void MovePlayer()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            _dir = new Vector3(horizontal, 0, vertical);
            _velocity = _dir * playerSpeed;
            
            _velocity = transform.TransformDirection(_velocity);
            
            if (Input.GetButtonDown("Jump") && _groundedPlayer)
            {
                _velocity.y = jumpHeight;
            }
        }

        _velocity.y += gravityValue * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
    

    private void CameraFollow()
    {
        var xPos = Input.GetAxis("Mouse X");
        var rot = transform.localEulerAngles;
        rot.y += xPos * mouseSensitivy;
        transform.rotation = Quaternion.AngleAxis(rot.y, Vector3.up);
        

        var yPos = Input.GetAxisRaw("Mouse Y");
        var cameraRot = _mainCamera.transform.localEulerAngles;
        cameraRot.x += -yPos * mouseSensitivy;
        cameraRot.x = Mathf.Clamp(cameraRot.x, 0f, 26f);
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(cameraRot.x, Vector3.right);

    }
}

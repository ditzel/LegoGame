using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //Input
    protected Player Player;

    //Parameters
    protected const float RotationSpeed = 100;

    //Camera Controll
    public Vector3 CameraPivot;
    public float CameraDistance;
    protected float InputRotationX;
    protected float InputRotationY;

    protected Vector3 CharacterPivot;
    protected Vector3 LookDirection;

    protected Vector3 LastMousePos;

    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<Player>();
        LastMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //scroll to zoom
        CameraDistance = Mathf.Clamp(CameraDistance + Input.mouseScrollDelta.y, 0, 10);

        //input
        InputRotationX = InputRotationX + Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime % 360f;
        InputRotationY = Mathf.Clamp(InputRotationY - Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime, -88f, 88f);

        //left and forward
        var characterForward = Quaternion.AngleAxis(InputRotationX, Vector3.up) * Vector3.forward;
        var characterLeft = Quaternion.AngleAxis(InputRotationX + 90, Vector3.up) * Vector3.forward;

        //look and run direction
        var runDirection = characterForward * Input.GetAxisRaw("Vertical") + characterLeft * Input.GetAxisRaw("Horizontal");
        LookDirection = Quaternion.AngleAxis(InputRotationY, characterLeft) * characterForward;

        //set player values
        Player.Input.RunX = runDirection.x;
        Player.Input.RunZ = runDirection.z;
        Player.Input.LookX = LookDirection.x;
        Player.Input.LookZ = LookDirection.z;
        Player.Input.Jump = Input.GetKey(KeyCode.Space);

        CharacterPivot = Quaternion.AngleAxis(InputRotationX, Vector3.up) * CameraPivot;

    }

    private void LateUpdate()
    {
        //set camera values
        Camera.main.transform.position = (transform.position + CharacterPivot) - LookDirection * CameraDistance;
        Camera.main.transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
    }
}
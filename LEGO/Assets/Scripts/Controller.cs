using System;
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

    public RingMenu MainMenuPrefab;
    protected RingMenu MainMenuInstance;

    protected float InputRotationX;
    protected float InputRotationY;

    protected Vector3 CharacterPivot;
    protected Vector3 LookDirection;

    protected Vector3 LastMousePos;

    [HideInInspector]
    public ControllerMode Mode;

    // Use this for initialization
    void Start()
    {
        SetMode(ControllerMode.Play);
        Player = FindObjectOfType<Player>();
        LastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {


        if (Mode == ControllerMode.Build)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetMode(ControllerMode.Menu);
                MainMenuInstance = Instantiate(MainMenuPrefab, FindObjectOfType<Canvas>().transform);
                MainMenuInstance.callback = MenuClick;
            }
        }

        if (Mode == ControllerMode.Play)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SetMode(ControllerMode.Build);
            }
        }
        else if (Mode == ControllerMode.Build || Mode == ControllerMode.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SetMode(ControllerMode.Play);
            }
        }

        if (Mode == ControllerMode.Build || Mode == ControllerMode.Play)
        {
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
        else
        {
            //set player values
            Player.Input.RunX = 0;
            Player.Input.RunZ = 0;
            Player.Input.LookX = 0;
            Player.Input.LookZ = 0;
            Player.Input.Jump = false;
        }

    }

    private void MenuClick(string path)
    {
        Debug.Log(path);
        var paths = path.Split('/');
        GetComponent<PlaceBrick>().SetPrefab(int.Parse(paths[1]), int.Parse(paths[2]));
        SetMode(ControllerMode.Build);
    }

    public void SetMode(ControllerMode mode)
    {
        Mode = mode;
        if (mode != ControllerMode.Menu && MainMenuInstance != null)
            Destroy(MainMenuInstance);

        switch (mode)
        {
            case ControllerMode.Build:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case ControllerMode.Menu:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case ControllerMode.Play:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }


    private void LateUpdate()
    {
        //set camera values
        Camera.main.transform.position = (transform.position + CharacterPivot) - LookDirection * CameraDistance;
        Camera.main.transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
    }

    public enum ControllerMode
    {
        Play,
        Build,
        Menu
    }
}
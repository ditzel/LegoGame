using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public InputStr Input;
    public struct InputStr
    {
        public float LookX;
        public float LookZ;
        public float RunX;
        public float RunZ;
        public bool Jump;
    }

    public const float Speed = 10f;
    public const float JumpForce = 7f;

    public float FallMultiplier = 3f;
    public float JumpMulitplier = 2f;

    protected Rigidbody Rigidbody;
    protected Quaternion LookRotation;
    protected bool CanJump;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        var inputRun = Vector3.ClampMagnitude(new Vector3(Input.RunX, 0, Input.RunZ), 1);
        var inputLook = Vector3.ClampMagnitude(new Vector3(Input.LookX, 0, Input.LookZ), 1);

        Rigidbody.velocity = new Vector3(inputRun.x * Speed, Rigidbody.velocity.y, inputRun.z * Speed);

        //rotation to go target
        if (inputLook.magnitude > 0.01f)
            LookRotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, inputLook, Vector3.up), Vector3.up);

        transform.rotation = LookRotation;

        //Jump
        Rigidbody.velocity += Vector3.up * Physics.gravity.y * (Rigidbody.velocity.y < 0 ? (FallMultiplier - 1) : (JumpMulitplier - 1) * (Input.Jump ? 0f : 1f))* Time.fixedDeltaTime;
        CanJump = CanJump || DistanceFromGroud() <= 0;

        if (Input.Jump && CanJump)
        {
            CanJump = false;
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, JumpForce, Rigidbody.velocity.z);
        }

    }

    public float DistanceFromGroud()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out var hitinfo, 10f, LegoLogic.LayerMaskLego))
            return hitinfo.distance - 0.21f;
        else
            return float.MaxValue;
    }
}
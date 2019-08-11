using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    protected Animator Animator;
    protected Player Player;
    protected Rigidbody Rigidbody;

    protected Vector3 LeanTowards;
    protected Vector3 LeanTowardsVel;

    // Start is called before the first frame update
    void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = transform.parent.GetComponent<Rigidbody>();
        Player = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var lean = new Vector3(Player.Input.RunX, 0, Player.Input.RunZ);
        LeanTowards = Vector3.SmoothDamp(LeanTowards, lean, ref LeanTowardsVel, 0.1f);

        transform.rotation = Quaternion.FromToRotation(transform.parent.up, Vector3.up * 3f + LeanTowards) * transform.parent.rotation;

        Animator.SetFloat("Speed", Mathf.Sign(Vector3.Dot(Rigidbody.velocity, Rigidbody.transform.forward)) * Vector3.Project(Rigidbody.velocity, Rigidbody.transform.forward).magnitude);
        Animator.SetFloat("Side", Mathf.Sign(Vector3.Dot(Rigidbody.velocity, Rigidbody.transform.right)) * Vector3.Project(Rigidbody.velocity, Rigidbody.transform.right).magnitude);
        Animator.SetFloat("Jump", Mathf.Clamp(Player.DistanceFromGroud(), 0, .2f) * 5f);
    }
}

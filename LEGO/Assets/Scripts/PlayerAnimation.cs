using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    protected Animator Animator;
    protected Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Animator.SetFloat("Speed", Mathf.Sign(Vector3.Dot(Rigidbody.velocity, Rigidbody.transform.forward)) * Vector3.Project(Rigidbody.velocity, Rigidbody.transform.forward).magnitude);
        Animator.SetFloat("Side", Mathf.Sign(Vector3.Dot(Rigidbody.velocity, Rigidbody.transform.right)) * Vector3.Project(Rigidbody.velocity, Rigidbody.transform.right).magnitude);
    }
}

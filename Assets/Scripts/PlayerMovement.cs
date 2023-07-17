using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Animator _anim;
    private Vector3 _input;
    private Rigidbody _rb;

    //Movement values
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _sprintSpeed = 5;
    [SerializeField] private float _rotSpeed = 360;
    private bool isSprinting = false;
    public bool lockMovement = false;
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
     
    }

    public void LockMovement(bool toggle)
    {
        lockMovement = toggle;
    }


    private void Update()
    {
        if (lockMovement)
        {
            return;
        }

        GatherInput();
        Look();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }else { isSprinting = false; }
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var skewedInput = matrix.MultiplyPoint3x4(_input);

            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _rotSpeed * Time.deltaTime);
        }
 
    }

    private void Move()
    {


        float targetSpeed;
        if (isSprinting)
        {
            targetSpeed = _sprintSpeed;
        } else
        {
            targetSpeed = _speed;
        }

        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * targetSpeed * Time.deltaTime);
        _anim.SetFloat("moveSpeed", _input.magnitude * targetSpeed);

    }

}

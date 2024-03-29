
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 9.8f;
    public float jumpForce;
    public float speed;
    public Animator animator;
    private float _fallVelocity = 0;
    private CharacterController _characterController;
    private Vector3 _moveVector;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Перемещение
        _moveVector = Vector3.zero;
        var run = 0;

        if (Input.GetKey(KeyCode.W))
        {
            _moveVector += transform.forward;
            run = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= transform.forward;
            run = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
            run = 3;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
            run = 4;
        }

        animator.SetInteger("Run 1", run);

        //Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
            _fallVelocity = -jumpForce;
        }
    }
    void FixedUpdate()
    {
        //Движение
        _characterController.Move(_moveVector * speed * Time.fixedDeltaTime);

        //Падение и прыжок
        _fallVelocity += gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.down * _fallVelocity * Time.fixedDeltaTime);

        //Скорость падения
        if (_characterController.isGrounded)
        {
            _fallVelocity = 0;
        }
    }
}
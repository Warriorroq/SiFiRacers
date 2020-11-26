using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CarMovement : NetworkBehaviour
{
    [SerializeField] private Vector3 maxSpeed = Vector3.zero;
    [SerializeField] private Vector3 moveVelocity = Vector3.zero;
    [SerializeField] private float speed = 0;    
    private GameObject cam;
    private Rigidbody rb;
    private Gun gun;
    private float delta;
    private int frame;
    void Start(){
        moveVelocity = new Vector3(0, 0, 0);
        cam = GetComponentInChildren<Camera>().gameObject;
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
    }

    private void FixedUpdate(){
        if (!this.isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponentInChildren<AudioListener>().enabled = false;
        }

        if (this.isLocalPlayer)
        {
            CalmVelocity();

            MoveForwardBackward();

            MoveSides();
            MoveUpDown();
            gun.Move();


            rb.velocity = moveVelocity.z * transform.forward;
            rb.velocity = new Vector3(rb.velocity.x, moveVelocity.y, rb.velocity.z);

            transform.Rotate(0, moveVelocity.x * Time.deltaTime * 4f, 0);

            transform.eulerAngles = new Vector3(-moveVelocity.y, transform.eulerAngles.y, -moveVelocity.x);
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, transform.eulerAngles.y, -moveVelocity.x / 2f);
        }
    }

    private void MoveForwardBackward()
    {
        float a = Input.GetAxis("Vertical") * speed;
        if (moveVelocity.z + a <= maxSpeed.z && moveVelocity.z + a >= -maxSpeed.z)
            moveVelocity.z += a;
    }

    private void MoveUpDown(){
        if (Input.GetKey(KeyCode.Space) && moveVelocity.y <= maxSpeed.y)
            moveVelocity.y += speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftControl) && moveVelocity.y >= -maxSpeed.y)
            moveVelocity.y -= speed * Time.deltaTime;
    }

    private void MoveSides() {
        float a = Input.GetAxis("Horizontal") * speed / 4f;
        if (moveVelocity.x + a <= maxSpeed.x && moveVelocity.x + a >= -maxSpeed.x)
            moveVelocity.x += a;
    }
    
    private void CalmVelocity(){
        if (Input.GetAxis("Vertical") == 0){
            if (moveVelocity.z != 0)
                moveVelocity.z += -moveVelocity.z * Time.deltaTime * 2;

            if (Mathf.Abs(moveVelocity.z) <= 0.05)
                moveVelocity.z = 0;
        }

        if (Input.GetAxis("Horizontal") == 0){
            if (moveVelocity.x != 0)
                moveVelocity.x += -moveVelocity.x * Time.deltaTime * 2;

            if (Mathf.Abs(moveVelocity.x) <= 0.05)
                moveVelocity.x = 0;
        }

        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Space)){
            if (moveVelocity.y != 0)
                moveVelocity.y += -moveVelocity.y * Time.deltaTime * 2;

            if (Mathf.Abs(moveVelocity.y) <= 0.05)
                moveVelocity.y = 0;
        }
    }
}

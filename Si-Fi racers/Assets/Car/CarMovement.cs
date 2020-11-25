using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Vector3 maxSpeed = Vector3.zero;
    [SerializeField] private Vector3 moveVelocity = Vector3.zero;
    [SerializeField] private float speed = 0;    
    private GameObject cam;
    private Rigidbody rb;
    void Start(){
        moveVelocity = new Vector3(0, 0, 0);
        cam = GetComponentInChildren<Camera>().gameObject;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CalmVelocity();

        if (Mathf.Abs(moveVelocity.z) <= Mathf.Abs(maxSpeed.z))
            MoveForwardBackward();

        if (Mathf.Abs(moveVelocity.x) <= Mathf.Abs(maxSpeed.x))
            MoveSides();

        if (Mathf.Abs(moveVelocity.y) <= Mathf.Abs(maxSpeed.y))
            MoveUpDown();

        rb.velocity = moveVelocity;

        transform.eulerAngles = new Vector3(-moveVelocity.y, 0, -moveVelocity.x);
        cam.transform.eulerAngles = new Vector3(-moveVelocity.y, 0, -moveVelocity.x/2f);
    }
    private void MoveForwardBackward()
    {
        if (Input.GetKey(KeyCode.W))
            moveVelocity.z += speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            moveVelocity.z -= speed * Time.deltaTime;
    }
    private void MoveUpDown()
    {
        if (Input.GetKey(KeyCode.Space))
            moveVelocity.y += speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftControl))
            moveVelocity.y -= speed * Time.deltaTime;
    }
    private void MoveSides()
    {
        if (Input.GetKey(KeyCode.A))
            moveVelocity.x -= speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            moveVelocity.x += speed * Time.deltaTime;
    }
    private void CalmVelocity(){
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)){
            if (moveVelocity.z != 0)
                moveVelocity.z += -moveVelocity.z * Time.deltaTime * 2;

            if (Mathf.Abs(moveVelocity.z) <= 0.05)
                moveVelocity.z = 0;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
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

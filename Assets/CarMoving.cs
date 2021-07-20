using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed
{
    public float x;
    public float y;
    public float z;

    public Speed(float a1, float a2, float a3) 
    {
        this.x = a1; 
        this.y = a2;
        this.z = a3;
    }
}

public enum side : int { 
    bottom = 0,
    left = 1,
    right = 2,
    top = 3
} 

//public class Side {
//    public side side;
//    public Vector3 *coordinates;
//    public Vector3 gravity;

//    public Side(Vector3* coordinates) {
//        this->coordinates = coordinates;
//        this->side = side.bottom;
//    }

//    public void changeSide() {

//        switch (side) {
//            case side.bottom: {
//                (coordinates->X, coordinates.Y) = (coordinates->Y, coordinates.X);
//                break;
//            }
//            case side.left: {
//                (coordinates->X, coordinates.Y) = (coordinates->Y, coordinates.X);
//                break;
//            }
//            case side.right:{
//                (coordinates->X, coordinates.Y) = (coordinates->Y, coordinates.X);
//                break;
//            }
//            case side.top:{
//                (coordinates->X, coordinates.Y) = (coordinates->Y, coordinates.X);
//                   break;
//            }
//        }
        
//    }
//}



public class CarMoving : MonoBehaviour
{
    public const float gravityConst = 9.8f;
    public Vector3 gravity = new Vector3(0, gravityConst, 0);
    public Rigidbody rb;
    public GameObject car; //Модель машины

    public GameObject brokenPrefab; //Префаб сломанной машины
    public GameObject modelHolder; //Объект, в который помещается модель

    //public Controls control; //Скрипт управления, он будет добавлен позже

    Vector3 maxSpeed = new Vector3(0.5f, 0.5f, 0.5f);
    Vector3 minSpeed = new Vector3(0.001f, 0.001f, 0.001f);
    Vector3 speed = new Vector3(0, 0, 0);
    float RoadWidth = 10f;
    float CarWidthX = 3f;
    float CarWidthY = 2.5f;

    public side Side = side.bottom;
    float ChangeSideCooldown = 0.5f;
    float NextSideChange = 0.0f;

    private bool isAlive = true; //Жива ли машина. Если да, то она будет двигаться
    private bool isKilled = false; //Эта переменная нужна, чтобы триггер сработал только один раз

    public List<GameObject> wheels; //Колёса машины

    //TODO: Оптимизировать и разделить на разные скрипты

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void changeSide() {

        //Gravity
        //if (gravity.x > 0)
        //{
        //    gravity.y = gravityConst;
        //    gravity.x = 0;
        //}
        //else
        //{
        //    gravity.x = gravityConst;
        //    gravity.y = 0;
        //}
        

        //Disable force to stop moving car
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //Position and camera change 
        switch (Side) {
            case side.bottom: {
                    transform.position = new Vector3( (-1) * RoadWidth/2 + CarWidthY, RoadWidth - CarWidthX, transform.position.z);
                    Side = side.left;
                    transform.Rotate(0, 0, 270);
                    gravity.x = (-1) * gravityConst;
                    gravity.y = 0;
                    break;
            }
            case side.left: {
                    transform.position = new Vector3(RoadWidth - transform.position.y, RoadWidth - CarWidthY, transform.position.z);
                    Side = side.top;
                    transform.Rotate(0, 0, 270);
                    gravity.x = 0;
                    gravity.y = gravityConst;
                    break;
            }
            case side.top:
            {
                    transform.position = new Vector3(RoadWidth / 2 - CarWidthX, RoadWidth - transform.position.x, transform.position.z);
                    Side = side.right;
                    transform.Rotate(0, 0, 270);
                    gravity.x = gravityConst;
                    gravity.y = 0;
                    break;
            }
            case side.right:
            {
                    transform.position = new Vector3(RoadWidth - transform.position.y, RoadWidth - transform.position.x, transform.position.z);
                    Side = side.bottom;
                    transform.Rotate(0, 0, 270);
                    gravity.x = 0;
                    gravity.y = (-1) * gravityConst;
                    break;
            }
            
        }
       
        Physics.gravity = gravity;


    }

    public void MakeAMove() {
        switch (Side) {

            case (side.bottom):
                {
                    transform.position = new Vector3(transform.position.x + speed.x, transform.position.y, transform.position.z + speed.z);
                    break;
                }
            case (side.left):
                {
                    transform.position = new Vector3(transform.position.x + speed.y, transform.position.y, transform.position.z + speed.z);
                    break;
                }
            case (side.top):
                {
                    transform.position = new Vector3(transform.position.x - speed.x, transform.position.y, transform.position.z + speed.z);
                    break;
                }
            case (side.right):
                {
                    transform.position = new Vector3(transform.position.x - speed.y, transform.position.y, transform.position.z + speed.z);
                    break;
                }
        }
       
    }

    public void ChangeSpeed() {

        float moveSide = Input.GetAxis("Horizontal");  //Когда игрок будет нажимать на стрелочки влево или вправо, сюда будет добавляться 1f или -1f
        float moveForward = Input.GetAxis("Vertical"); //То же самое, но со стрелочками вверх и вниз

        if (moveSide != 0)
        {
            speed.x += minSpeed.x * moveSide;
        }
        else if (speed.x > 0)
        {
            speed.x -= minSpeed.x;
        }

        if (moveForward != 0)
        {
            speed.z += minSpeed.z * moveForward;
        }
        else if (speed.z > 0)
        {
            speed.z -= minSpeed.z;
        }

        if (speed.x >= maxSpeed.x)
        {
            speed.x = maxSpeed.x;
        }
        if (speed.y >= maxSpeed.y)
        {
            speed.y = maxSpeed.y;
        }
        if (speed.z >= maxSpeed.z)
        {
            speed.z = maxSpeed.z;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (isAlive) {

            ChangeSpeed();
            MakeAMove();

            if (Input.GetButton("Fire1") && Time.time > NextSideChange)
            {
                NextSideChange = Time.time + ChangeSideCooldown;

                changeSide();
                
            }
            
        }
        
    }
}

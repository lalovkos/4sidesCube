using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Prefabs
    public GameObject roadPrefab_;

    //Preferences
    public float changeSideCooldownSec_;
    public float fullChangeTime_;
    public float playerWidth_;

    private enum eSide : int { 
        bottom = 1,
        left = 2,
        right = 3,
        top = 4
    }

    private eSide side_ = eSide.bottom;
    private float NextSideChange_;
    private Vector3 movePosition_;
    private float stepDistance_;

    // Start is called before the first frame update
    void Start()
    {
        NextSideChange_ = Time.time;
        movePosition_ = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > NextSideChange_)
        {
            NextSideChange_ = Time.time + changeSideCooldownSec_;
            roadPrefab_.GetComponent<RoadScript>().ChangeSide(transform.position.z);
        }
        //else
        //{
        //    if (Vector3.Distance(transform.position, movePosition_) > 0.01f)
        //    {
        //        stepDistance_ = Vector3.Distance(transform.position, movePosition_) / fullChangeTime_;
        //        transform.position = Vector3.MoveTowards(transform.position, movePosition_, stepDistance_ * Time.deltaTime);
        //    }
        //}

    }

    public void changeSide() 
    {
        float BlockWidth = roadPrefab_.GetComponent<RoadParts>().GetBlockWidth();
        switch (side_)
        {
            case (eSide.bottom):
                {
                    movePosition_ = new Vector3(transform.position.y, transform.position.x, transform.position.z);
                    side_ = eSide.left;
                    break;
                }
            case (eSide.left):
                {
                    movePosition_ = new Vector3(transform.position.y, -transform.position.x, transform.position.z);
                    side_ = eSide.top;
                    break;
                }
            case (eSide.top):
                {
                    movePosition_ = new Vector3(-transform.position.y, transform.position.x, transform.position.z);
                    side_ = eSide.right;
                    break;
                }
            case (eSide.right):
                {
                    movePosition_ = new Vector3(transform.position.y, -transform.position.x, transform.position.z);
                    side_ = eSide.bottom;
                    break;
                }
        }
    }
}

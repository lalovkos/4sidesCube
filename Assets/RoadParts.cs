using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParts : MonoBehaviour
{

    //Preferences
    [SerializeField] private float roadBlockLenght;
    [SerializeField] private float roadBlockWidht;
    [SerializeField] private float roadZMovingSpeedPerSec;

    private float roadZMovingSpeedPerFrame;

    public float GetBlockLenght()
    {
        return roadBlockLenght;
    }

    public float GetBlockWidth()
    {
        return roadBlockWidht;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        float currentStep  = roadZMovingSpeedPerSec * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - currentStep);

        if (transform.position.z < -1000)
        {
            Delete();
        }    
    
    }

    public void Delete()
    {
        Destroy(this);

    }
}

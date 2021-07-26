using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //GameObjects
    [SerializeField] private GameObject roadObject;  

    //Preferences
    [SerializeField] private float playerWidth;
    [SerializeField] private float playerLength;
    [SerializeField] private float playerVisibilityRange;

    // Start is called before the first frame update
    private void Start()   
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public float GetVisibilityRange()
    {
        return playerVisibilityRange;
    }
}

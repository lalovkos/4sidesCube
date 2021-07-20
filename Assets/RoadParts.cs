using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadParts : MonoBehaviour
{

    //Preferences
    public int distanceBeforeDestroyed;
    public int RoadBlockLenght;
    public int RoadBlockWidht;

    public RoadParts InstantiateRoadPart(Vector3 position)
    {
        transform.position = position;
        Instantiate(this, position, Quaternion.identity);
        return this;
    }

    public float GetBlockLenght() 
    {
        return RoadBlockLenght;
    }

    public float GetBlockWidth()
    {
        return RoadBlockWidht;
    }

    public bool FarEnough(float z) //Проверка, проехала ли машина игрока этот блок на достаточное расстояние
    {

        if (z > transform.position.z + RoadBlockLenght + distanceBeforeDestroyed)
        {
            return true; 
        }
        else 
        { 
            return false; 
        }
        
    }

    public void Delete()
    {
        Destroy(gameObject); //Удаление блока
    }

}
using System.Collections;
using System.Collections.Generic;  // TODO: Лишние библиотеки
using UnityEngine;

public class RoadParts : MonoBehaviour
{

    //Preferences
    public int distanceBeforeDestroyed;  // TODO: private
    public int RoadBlockLenght;
    public int RoadBlockWidht;

    public RoadParts InstantiateRoadPart(Vector3 position) // TODO: Инстанцирует сам себя? Лучше пусть это делает какой-нибудь SpawnController
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

    public bool FarEnough(float z) //Ïðîâåðêà, ïðîåõàëà ëè ìàøèíà èãðîêà ýòîò áëîê íà äîñòàòî÷íîå ðàññòîÿíèå
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
        Destroy(gameObject); //Óäàëåíèå áëîêà
    }

}

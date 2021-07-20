using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    //Road blocks
    public List<RoadParts> blocks;     //Road blocks collection
    public GameObject roadBlocksPrefab;

    //Player prefab
    public GameObject playerPrefab;
    public float playerVision;

    // Start is called before the first frame update
    void Start()
    {
        blocks.Add(roadBlocksPrefab.GetComponent<RoadParts>().InstantiateRoadPart(transform.position));
    }

    // Update is called once per frame
    //TODO: Разобраться в обращениях к префабам и к самим объектам
    void Update()
    {
        float playerZPosition = playerPrefab.GetComponent<PlayerScript>().transform.position.z;

        float lastRoadBlockZPosition = blocks[blocks.Count - 1].transform.position.z;

        if (playerZPosition > lastRoadBlockZPosition - playerVision) 
        {
            //Create new road block
            float newRoadBlockPosition = lastRoadBlockZPosition + blocks[blocks.Count - 1].GetBlockLenght();
            RoadParts block = roadBlocksPrefab.GetComponent<RoadParts>().InstantiateRoadPart(new Vector3(transform.position.x, transform.position.y, newRoadBlockPosition));
            blocks.Add(block); //Добавление блока в коллекцию

        }

        foreach (RoadParts block in blocks)
        {
            bool farEnough = block.GetComponent<RoadParts>().FarEnough(playerZPosition); //Проверка, проехал ли игрок этот блок

            if (farEnough) //Если проехал
            {
                blocks.Remove(block); //Удаление блока из коллекции
                block.GetComponent<RoadParts>().Delete(); //Удаление блока со сцены
            }
        }
    }

    public void ChangeSide(float z) 
    {
        foreach (RoadParts block in blocks) 
        {
            if ( z > block.transform.position.z && z < block.transform.position.z + block.RoadBlockLenght ) 
            {
                block.transform.Rotate(0, 0, 90);
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //Контейнер для блоков дороги
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject roadPart;
    [SerializeField] private GameObject road;
    [SerializeField] private int MinimumRoadParts;


    private float playerVisibilityRange;
    private float roadBlockLenght;
    private List<GameObject> roadBlocks;
    private float lastRoadBlockZCoordinate;

    // Start is called before the first frame update
    private void Start()
    {
        
        //Размещение и заполнение списка блоков дороги
        playerVisibilityRange = player.GetComponent<PlayerController>().GetVisibilityRange();
        roadBlockLenght       = roadPart.GetComponent<RoadParts>().GetBlockLenght();
        float playerZPosition = player.GetComponent<PlayerController>().transform.position.z;

        //Устанавливаем  число блоков дороги
        int blocksNumber = (int) Mathf.Ceil(playerVisibilityRange / roadBlockLenght);
        blocksNumber = (blocksNumber < MinimumRoadParts) ? MinimumRoadParts : blocksNumber;
        
        //Создаем и заполняем список блоков дороги
        roadBlocks = new List<GameObject>();
        for (int i = 0; i < blocksNumber; i++)
        {
            Vector3 newRoadblockPos = new Vector3(transform.position.x, transform.position.y, playerZPosition + i * roadBlockLenght);
            GameObject newRoadBlock = Instantiate(roadPart, newRoadblockPos, Quaternion.identity) as GameObject;
            newRoadBlock.SetActive(true);
            newRoadBlock.transform.SetParent(road.transform);
            roadBlocks.Add(newRoadBlock);
        }

        //Последний блок на границе видимости
        lastRoadBlockZCoordinate = playerZPosition + (roadBlocks.Count-1) * roadBlockLenght;
    }

    // Update is called once per frame
    private void Update()
    {
        //Проверяем блоки, проехали ли мы их, если проехали, перемещаем блоки вперед
        float playerZPosition = player.GetComponent<PlayerController>().transform.position.z;
        lastRoadBlockZCoordinate = lastZPosition();
        foreach (var block in roadBlocks)
        {
            //TODO: Оптимизировать
            if (playerZPosition > block.transform.position.z + roadBlockLenght) //Позиция игрока находится за частью дороги
            {
                //Перемещаем блок вперед
                block.transform.position = new Vector3(transform.position.x, transform.position.y, lastRoadBlockZCoordinate + roadBlockLenght); 

            }
        }
        
    }

    private float lastZPosition()
    {
        float lastZPos = 0;
        foreach (var block in roadBlocks)
        {
            if (lastZPos < block.transform.position.z)
            {
                lastZPos = block.transform.position.z;
            }
        }

        return lastZPos;
    }
}

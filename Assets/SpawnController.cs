using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnController : MonoBehaviour  // Получается какой-то God object, что нарушает ООП. Он должен только спаунить. Не должен перемещать блоки
{
    //Êîíòåéíåð äëÿ áëîêîâ äîðîãè
    [SerializeField] private GameObject player; // Где-то ещё понадобится ссылка на Player. Везде прокидывать на него ссылки через инспектор не очень. Сделай его Singleton
    [SerializeField] private GameObject roadPart; // Лучше сделать префаб и убрать со сцены
    [SerializeField] private GameObject road;
    [SerializeField] private int MinimumRoadParts;


    private float playerVisibilityRange;
    private float roadBlockLenght;
    private List<GameObject> roadBlocks;
    private float lastRoadBlockZCoordinate;

    // Start is called before the first frame update
    private void Start()
    {
        
        //Ðàçìåùåíèå è çàïîëíåíèå ñïèñêà áëîêîâ äîðîãè
        playerVisibilityRange = player.GetComponent<PlayerController>().GetVisibilityRange();
        roadBlockLenght       = roadPart.GetComponent<RoadParts>().GetBlockLenght();
        float playerZPosition = player.GetComponent<PlayerController>().transform.position.z;

        //Óñòàíàâëèâàåì  ÷èñëî áëîêîâ äîðîãè
        int blocksNumber = (int) Mathf.Ceil(playerVisibilityRange / roadBlockLenght);
        blocksNumber = (blocksNumber < MinimumRoadParts) ? MinimumRoadParts : blocksNumber;
        
        //Ñîçäàåì è çàïîëíÿåì ñïèñîê áëîêîâ äîðîãè
        roadBlocks = new List<GameObject>(); // Слишком много разнгообразной логики в одном методе, лучше вынести спаун в отдельный метод. К тому же, если ты захочешь сделать спаун в другой момент, то у тебя будет дублироваться код.
        for (int i = 0; i < blocksNumber; i++)
        {
            Vector3 newRoadblockPos = new Vector3(transform.position.x, transform.position.y, playerZPosition + i * roadBlockLenght);
            GameObject newRoadBlock = Instantiate(roadPart, newRoadblockPos, Quaternion.identity) as GameObject; // И так GameObject, приводить не нужно
            newRoadBlock.SetActive(true);
            newRoadBlock.transform.SetParent(road.transform);
            roadBlocks.Add(newRoadBlock);
        }

        //Ïîñëåäíèé áëîê íà ãðàíèöå âèäèìîñòè
        lastRoadBlockZCoordinate = playerZPosition + (roadBlocks.Count-1) * roadBlockLenght;
    }

    // Update is called once per frame
    private void Update() // Логика не SpawnController
    {
        //Ïðîâåðÿåì áëîêè, ïðîåõàëè ëè ìû èõ, åñëè ïðîåõàëè, ïåðåìåùàåì áëîêè âïåðåä
        float playerZPosition = player.GetComponent<PlayerController>().transform.position.z; // Постоянно обращаешься, хотя значение у transform.position.z одинаковое. GetComponent<PlayerController>() не нужно, просто player.transform.position.z;
        lastRoadBlockZCoordinate = lastZPosition();
        foreach (var block in roadBlocks)
        {
            //TODO: Îïòèìèçèðîâàòü
            if (playerZPosition > block.transform.position.z + roadBlockLenght) //Ïîçèöèÿ èãðîêà íàõîäèòñÿ çà ÷àñòüþ äîðîãè
            {
                //Ïåðåìåùàåì áëîê âïåðåä
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

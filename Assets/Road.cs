using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public List<GameObject> blocks; //Коллекция всех дорожных блоков
    public GameObject Car; //Игрок
    public GameObject roadPrefab; //Префаб дорожного блока
    public GameObject carPrefab; //Префаб машины NPC
    public GameObject coinPrefab; //Префаб монеты
    public int i = 0;
    private System.Random rand = new System.Random(); //Генератор случайных чисел

    // Start is called before the first frame update
    void Start()
    {
        var block = Instantiate(roadPrefab, new Vector3(0, 0, 50), Quaternion.identity);
        blocks.Add(block);
    }

    // Update is called once per frame
    void Update()
    {
        float z = Car.GetComponent<CarMoving>().rb.position.z; //Получение положения игрока

        var last = blocks[blocks.Count - 1]; //Номер дорожного блока, который дальше всех от игрока

        if (z > last.transform.position.z - 5 * 50f) //Если игрок подъехал к последнему блоку ближе, чем на 10 блоков
        {
            //Инстанцирование нового блока
            var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x, last.transform.position.y, last.transform.position.z + 50f), Quaternion.identity);
            block.transform.SetParent(gameObject.transform); //Перемещение блока в объект Road
            blocks.Add(block); //Добавление блока в коллекцию

        }

        foreach (GameObject block in blocks)
        {
            bool fetched = block.GetComponent<RoadBlock>().Fetch(z); //Проверка, проехал ли игрок этот блок

            if (fetched) //Если проехал
            {
                blocks.Remove(block); //Удаление блока из коллекции
                block.GetComponent<RoadBlock>().Delete(); //Удаление блока со сцены
            }
        }
    }
}
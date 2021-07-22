using System.Collections;
using System.Collections.Generic;  // TODO: не нужные библиотеки желательно удалять
using UnityEngine;

public class PlayerScript : MonoBehaviour // TODO: имя класса должно более чётко описывать свою задачу. Если правильно понял, этот скрипт управляет объектом Player. Стоит назвать его PlayerController.
{
    //Prefabs
    public GameObject roadPrefab_;  // TODO: 1. Если это префаб, то нужно сделать roadPrefab префабом и указывать ссылку на него. У тебя просто ссылка на объект на сцене - это не префаб.
                                   //       2. Поля класса следует помечать модификатором доступа private, а если ты хочешь к ним обратиться, то надо использовать [SerializeField].

    //Preferences
    public float changeSideCooldownSec_;  // TODO: То же самое про private и не стоит в конце ставить нижний слеш.
    public float fullChangeTime_;
    public float playerWidth_;

    private enum eSide : int {  // TODO: Не обязательно наследовать от int. Первый символ должен быть заглавным. Лучше этот enum вообще вынести в отдельный файл, что бы не загрязнять класс.
        bottom = 1,
        left = 2,
        right = 3,
        top = 4
    }

    private eSide side_ = eSide.bottom;
    private float NextSideChange_;  // TODO: в большинстве компаний поддерживаются стандарты microsoft: private со строчной буквы, public с заглавной.
    private Vector3 movePosition_;
    private float stepDistance_;

    // Start is called before the first frame update
    void Start()   // TODO: Все методы и поля желательно помечать модификатором доступа.
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

    public void changeSide()   // TODO: 1. Методы начинаются с заглавной буквы. 2. Стоит выделить логику выбора стороны в отдельный класс, к примеру ChangeSideController, и прокинуть ссылку здесь.
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

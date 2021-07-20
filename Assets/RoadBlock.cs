using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public bool Fetch(float z) //ѕроверка, проехала ли машина игрока этот блок на достаточное рассто€ние
    {
        bool result = false;

        if (z > transform.position.z + 100f)
        {
            result = true; //≈сли машина проехала на 100f от блока, то возвращаетс€ true
        }

        return result;
    }

    public void Delete()
    {
        Destroy(gameObject); //”даление блока
    }
}

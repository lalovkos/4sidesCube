using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidesChanger : MonoBehaviour // Изменяет сторону, а не управляет перемещением Player. Лишняя логика перемещения.
{
    private enum ESides : int // Лучше в отдельный файл вынести Enum
    {  
        bottom = 1,
        left = 2,
        right = 3,
        top = 4
    }

    
    [SerializeField] private ESides currentSide;

    //Êîîðäèíàòû, íà êîòîðûå áóäåò ïåðåíîñèòü èãðîêà
    [SerializeField] private Vector3 bottomSideCoordinates;
    [SerializeField] private Vector3 leftSideCoordinates;
    [SerializeField] private Vector3 rightSideCoordinates;
    [SerializeField] private Vector3 topSideCoordinates;

    //Âðåìÿ îòêàòà ñìåíû ñòîðîíû
    [SerializeField] private float changeSideCooldownSec;
    [SerializeField] private float fullSideChangeTime;


    private Vector3 moveTowardsCoordinates; //Êîîðäèíàòû, ê êîòîðûì äâèãàåòñÿ èãðîê
    private float moveTowardsStep;          //Øàã çà ñåêóíäó // Лучше присвоить значение, а не вычислять. Вдруг захочется сделать бонус, который будет увеличивать скорость игры и перемещение тогда то же должно быть быстрее.
    private float moveTowardsAngle;         //Ïîâîðîò  çà ñåêóíäó
    private float nextSideChange;           //Âðåìÿ êîãäà ìîæíî áóäåò èçìåíèòü ñòîðîíó
    private float AngleLeft;                //Óãîë îñòàâøèéñÿ äëÿ ïîâîðîòà

    private const float kRotateAngle = -90; //Óãîë ïîâîðîòà

    //TODO: Ñäåëàòü íà÷àëüíûå êîîðäèíàòû â ñîîòâåòñòâèè ñ íà÷àëüíîé ñòîðîíîé
    // Start is called before the first frame update
    private void Start()
    {
        moveTowardsCoordinates = bottomSideCoordinates;
    }

    // Update is called once per frame
    private void Update() { 

        if (Input.GetButton("Fire1") && Time.time > nextSideChange)
        {
            nextSideChange = Time.time + changeSideCooldownSec;
            ChangeSide();
        }

        if (Vector3.Distance(transform.position, moveTowardsCoordinates) > 0.0001) // Логика перемещения в другом контроллере должна быть.
        {
            //Ïåðåìåùàåì èãðîêà íà íîâóþ òî÷êó è âðàùàåì
            transform.position = Vector3.MoveTowards(transform.position, moveTowardsCoordinates,
                moveTowardsStep * Time.deltaTime);
            transform.Rotate(0, 0, moveTowardsAngle * Time.deltaTime);
            AngleLeft -= moveTowardsAngle * Time.deltaTime;
        }
        else
        {   
            //Äëÿ òîãî, ÷òîáû óãîë áûë ðàâíûì â êîíöå ïîâîðîòà
            if (AngleLeft != 0)
            {
                transform.Rotate(0, 0, AngleLeft);
                AngleLeft = 0;
            }
        }

    }

    public void ChangeSide()
    {
        switch (currentSide) 
        {
            case (ESides.bottom): 
                {
                    currentSide = ESides.left;
                    moveTowardsCoordinates = leftSideCoordinates;
                    break;
                }
            case (ESides.left):
                {
                    currentSide = ESides.top;
                    moveTowardsCoordinates = topSideCoordinates;
                    break;
                }
            case (ESides.top):
                {
                    currentSide = ESides.right;
                    moveTowardsCoordinates = rightSideCoordinates;
                    break;
                }
            case (ESides.right):
                {
                    currentSide = ESides.bottom;
                    moveTowardsCoordinates = bottomSideCoordinates;
                    break;
                }
        }

        moveTowardsStep = Vector3.Distance(transform.position, moveTowardsCoordinates) / fullSideChangeTime;
        moveTowardsAngle = kRotateAngle / fullSideChangeTime;
        AngleLeft = -90;

    }
}

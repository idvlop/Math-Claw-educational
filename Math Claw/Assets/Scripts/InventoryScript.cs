using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public delegate void TaskSolveEvent(GameObject obj);
    public static event TaskSolveEvent OnSlotBallUsed;

    private readonly GameObject[] InventoryArray = new GameObject[3] { null, null, null };
    public GameObject InventoryPanel;
    public GameObject GrabManager;

    private void OnEnable()
    {
        TaskSolver.OnCloseTaskPressed += MoveBallToInventory;
    }

    private void OnDisable()
    {
        TaskSolver.OnCloseTaskPressed -= MoveBallToInventory;
    }

    void MoveBallToInventory(GameObject currentBall)
    {
        var index = GetFistEmptySlot(InventoryArray);
        if (index == -1)
        {
            currentBall.transform.parent = null; //Костыль на удаление, 
            currentBall.SetActive(false); //TODO заменить на предупреждение об удалении
        }
        else
        {
            SetBallToUI(currentBall, index);
            InventoryArray[index] = currentBall;
        }
    }
    
    int GetFistEmptySlot(GameObject[] array)
    {
        for(var i=0; i < array.Length; i++)
        {
            if (array[i] == null)
                return i;
        }
        return -1; //Пустой слот не найден
    }
    void SetBallToUI(GameObject ball, int slotIndex)
    {
        var rb = ball.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        ball.transform.parent = InventoryPanel.transform.GetChild(slotIndex);
        ball.transform.localPosition = Vector3.zero;
        ball.transform.localRotation = Quaternion.identity;
        
        var spriteBall = ball.GetComponent<SpriteRenderer>();
        spriteBall.sortingLayerName = "UI";
        spriteBall.sortingOrder = 1;
    }

    void SetBallToDefault(GameObject ball) //Заготовка для взятия гасяпона из инвенторя
    {
        ball.transform.parent = GrabManager.transform; 
        ball.transform.localPosition = new Vector3(0.087f, -2.93f, 0); //Точка внутри клешни
        var spriteBall = ball.GetComponent<SpriteRenderer>();
        spriteBall.sortingLayerName = "Default";
        spriteBall.sortingOrder = 0;
    }

    public void OnSlotButton(GameObject slot)
    {
        if (!MovementGrabber.isBallInHands && slot.transform.childCount > 0)
        {
            var ball = slot.transform.GetChild(0);
            var slotIndex = int.Parse(slot.name[slot.name.Length - 1].ToString());
            SetBallToDefault(ball.gameObject);
            InventoryArray[slotIndex] = null;
            OnSlotBallUsed(ball.gameObject);
        }

    }
}

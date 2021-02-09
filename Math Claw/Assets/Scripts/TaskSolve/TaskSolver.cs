using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TaskSolver : MonoBehaviour
{
    public GameObject TaskPanel;
    public GameObject BrokenGashaponPanel;
    public GameObject UsedPool;

    [HideInInspector] public static GameObject CurrentGashapon; 
    public delegate void TaskSolveEvent(GameObject obj);
    public static event TaskSolveEvent OnCloseTaskPressed;
    public static event TaskSolveEvent OnSolveTaskPressed;

    public GameObject Box3Elements;
    public GameObject Box2Elements;
    public GameObject PoolGashapons;

    private static bool[] AnsweredItems;
    private static Task GashTask;

    public void OnCloseTaskButton()
    {
        OnCloseTaskPressed(CurrentGashapon);
        TaskPanel.SetActive(false);
    }

    public void OnSolveTaskButton()
    {
        //Debug.Log("obj = " + CurrentGashapon);
        //OnSolveTaskPressed(CurrentGashapon);
        TaskPanel.SetActive(false);
        var gash = CurrentGashapon.GetComponent<Gashapon>();
        //Debug.Log("gash = " + gash);
        GashTask = gash.task;
        //foreach (var e in GashTask.InputValues)
        //    Debug.Log(e + ";");
        //Debug.Log(GashTask.InputValues.Length);
        var itemsCount = 1 + GashTask.InputValues.Length;
        AnsweredItems = new bool[itemsCount];
        //Debug.Log("count = " + AnsweredItems.Length);
        //foreach (var e in AnsweredItems)
        //    Debug.Log(e);
        Set3Box();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(000);
        if (collision.CompareTag("BallTrigger"))
        {
            Debug.Log(111);
            if (Box3Elements.activeInHierarchy)
            {
                Debug.Log(222);
                if (!AnsweredItems[0])
                {
                    if ((GashTask.FormulaId == 0 && gameObject.CompareTag("SolveTrigger_0")) ||
                        (GashTask.FormulaId == 1 && gameObject.CompareTag("SolveTrigger_1")) ||
                        (GashTask.FormulaId == 2 && gameObject.CompareTag("SolveTrigger_2")))
                    {
                        Debug.Log(31111);
                        AnsweredItems[0] = true;
                        Set2Box();
                    }
                    else
                    {
                        Debug.Log(32222);
                        BreakGashapon(CurrentGashapon);
                    }
                }
            }
            else if (Box2Elements.activeInHierarchy)
            {

            }
        }
    }

    private void BreakGashapon(GameObject gashapon)
    {
        BrokenGashaponPanel.SetActive(true);
        RemoveGashapon(gashapon);
        SetPoolGashapons();
    }

    public void RemoveGashapon(GameObject gashapon)
    {
        gashapon.transform.parent = UsedPool.transform;
        gashapon.SetActive(false);
        
    }

    private void Set3Box()
    {
        PoolGashapons.SetActive(false);
        Box2Elements.SetActive(false);
        Box3Elements.SetActive(true);
    }

    private void Set2Box()
    {
        PoolGashapons.SetActive(false);
        Box3Elements.SetActive(false);
        Box2Elements.SetActive(true);
    }

    private void SetPoolGashapons()
    {
        Box3Elements.SetActive(false);
        Box2Elements.SetActive(false);
        PoolGashapons.SetActive(true);
    }
    
}

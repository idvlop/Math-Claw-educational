using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartGame : MonoBehaviour
{
    public delegate void ButtonPressed();
    public static event ButtonPressed OnGrabPressed;
    public GameObject GrabberManager;
    public Transform PoolParent; // Gashapons Pool

    public static bool IsGrubbing = false;

    /// <summary>
    /// Load array of available gashapons
    /// </summary>
    [SerializeField] private GameObject[] gashaponPrefabs;

    public List<GameObject> currentGashapons = new List<GameObject>();

    private void OnEnable()
    {
        MovementGrabber.PickedUpBall += OnPickUpBall;
        //GrabberManager.GrabIsOvered += GrabIsOvered;
    }

    private void OnDisable()
    {
        MovementGrabber.PickedUpBall -= OnPickUpBall;
        //GrabberManager.GrabIsOvered -= GrabIsOvered;
    }

    void OnPickUpBall()
    {
        var gash = GrabberManager.transform.GetChild(0).GetComponent<Gashapon>();
        gash.OpenGashapon(); //Открываем вытянутый гасяпон
        TaskSolver.CurrentGashapon = gash.gameObject;
    }


    public void Start() {
        StartCoroutine(SpawnGashapons(10));
    }

    public void Start(int count) {
        if (currentGashapons.Count < 25)
            StartCoroutine(SpawnGashapons(count));
    }

    public IEnumerator SpawnGashapons(int count) {
        yield return new WaitForSeconds(0.2f);
        var gashaponsSpawnCount = Math.Min(count, GameManager.Tasks.First().Count() - 1);
        for (var i = 0; i < gashaponsSpawnCount; i++)
        {
            var x = Random.Range(-2f, 2f);
            var y = Random.Range(0.7f, 1.2f);
            
            var weightMax = gashaponPrefabs.Sum(gashapon => gashapon.GetComponent<Gashapon>().weight);
            var randomWeight = Random.Range(0, weightMax);
            var currentWeight = 0;
            var newGashapon = gashaponPrefabs[0];
            
            foreach (var gashapon in gashaponPrefabs)
            {
                currentWeight += gashapon.GetComponent<Gashapon>().weight;
                if (randomWeight < currentWeight) {
                    newGashapon = gashapon;
                    //  newGashapon.GetComponent<Gashapon>().task = Task.GetRandomTask(newGashapon.GetComponent<Gashapon>().toyRarity);
                    break;
                }
            }

            newGashapon.name = "Gashapon_";
            newGashapon.name += newGashapon.GetComponent<Gashapon>().type == 2 ? "Toy" : "Currency";
            currentGashapons.Add(newGashapon);

            Instantiate(newGashapon, new Vector3(x, y, 0), new Quaternion(), PoolParent);
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void OnGrabButton() 
    {
        OnGrabPressed();
    }

}

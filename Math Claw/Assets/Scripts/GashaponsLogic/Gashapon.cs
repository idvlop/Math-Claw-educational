using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gashapon : MonoBehaviour {
    public int weight;
    public int type;
    public int currency;
    
    [HideInInspector]
    public int toyId;
    public int toyRarity;
    public int health;
    public Task task;

    public Gashapon(int weight, int type, int currency, int toyId, int toyRarity) {
        this.weight = weight;
        this.type = type; // 0 - coins, 1 - premium, 2 - toy
        this.currency = currency;
        this.toyId = toyId; // Toy;GetRandomToyId();
        this.toyRarity = toyRarity;
        this.health = toyRarity + 1;
        task = Task.GetRandomTask(toyRarity);
    }

    public void OpenGashapon() {
        switch (type) {
            case 0:
                Player.coins += currency;
                break;
            case 1:
                Player.premium += currency;
                break;
            default:
                GameManager.OpenTaskWindow(this);
                break;
        }
    }
}

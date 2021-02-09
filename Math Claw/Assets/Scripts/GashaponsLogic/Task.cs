using System;
using System.Linq;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;
public class Task {
    public readonly int Id;
    public readonly int Difficulty;
    public readonly int FormulaId;

    public readonly double[] NeededValues;
    public readonly double[] InputValues;
    public readonly string Text;
    public bool Used;

    public Task(int id, int difficulty, int formulaId, double[] neededValues, double[] inputValues, string text) {
        Id = id;
        Difficulty = difficulty;
        FormulaId = formulaId;
        NeededValues = neededValues;
        InputValues = inputValues;
        Text = text;
    }

    public static Task GetRandomTask(int difficulty) {
        var tasks = GameManager.Tasks[difficulty];

        Task randTask;
        var enumerable = tasks.ToList();
        if (enumerable.Count(task => task.Used) < 2)
            randTask = GetNextTask();
        else {
            randTask = enumerable.Where(task => !task.Used).OrderBy(task => Guid.NewGuid()).First();
            GameManager.Tasks[difficulty].First(task => task.Id == randTask.Id).Used = true;
        }
        return randTask;
    }

    public static Task GetNextTask() {
        var tasks = GameManager.Tasks[0];

        var task = tasks.First(t => !t.Used);
        task.Used = true;
        return task;
    }
}
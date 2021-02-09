using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameObject mainBottomInventoryPanel;
    private static GameObject mainBottomTaskPanel;
    
    private static GameObject taskPanelObject;
    private static Text taskText;

    private static GameObject taskWatchPanel;
    private static Text taskWatchText;
    
    private static GameObject formulaPanelObject;
    private static Button[] formulaButtons;

    private static GameObject variablesPanel3ElemObject;
    private static GameObject variablesPanel2ElemObject;
    private static Button[] variables3ElemButtons;
    private static Button[] variables2ElemButtons;

    private static GameObject variablesWatchPanel;
    private static Text[] variablesWatchText;
    
    private static GameObject box2Elem;
    private static GameObject box3Elem;
    
    private static GameObject leftBallCollider; 
    private static GameObject middleBallCollider;
    private static GameObject rightBallCollider; 
    private static GameObject bigRightBallCollider; 
    
    private static GameObject damageGashaponObject;
    private static GameObject breakGashaponObject;

    private static IEnumerable<GameObject> ToysSlots;

    private static GameObject toyWinPanelObject;

    private static GameObject collectionPanel;
    private static IEnumerable<Image> collectionToysImages;
    
    private const string _easyTasksFilename = "Assets/Resources/Tasks/easyTasks.txt";
    private const string _normalTasksFilename = "Assets/Resources/Tasks/normalTasks.txt";

    public static IEnumerable<Task>[] Tasks;
    public static IEnumerable<Task> EasyTasks = new List<Task>();
    public static IEnumerable<Task> NormalTasks = new List<Task>();

    public Gashapon currentGashapon;
    public static IEnumerable<GameObject> SavedGashapons;

    public static GameObject PoolGashapons;
    public static GameObject Box3Elements;
    public static GameObject Box2Elements;
    public static RawImage GrabButton;

    public static bool HaveTaskOnSolve;

    public void Start() {
        PoolGashapons = GameObject.Find("GashaponsPool");
        Box2Elements = GameObject.Find("2ElemBox");
        Box3Elements = GameObject.Find("3ElemBox");
        Debug.Log(PoolGashapons);
        Debug.Log(Box2Elements);
        Debug.Log(Box3Elements);
        GrabButton = GameObject.Find("GrabButton").GetComponent<RawImage>();
        mainBottomInventoryPanel = GameObject.Find("MainBottomInventoryPanel");
        mainBottomTaskPanel = GameObject.Find("MainBottomTaskPanel");
        mainBottomTaskPanel.SetActive(false);
        
        taskPanelObject = GameObject.Find("TaskPanel");
        taskText = taskPanelObject
            .GetComponentsInChildren<Text>(true)
            .First(go => go.gameObject.name == "TaskText")
            .GetComponent<Text>();
        taskPanelObject.SetActive(false);

        taskWatchPanel = GameObject.Find("TaskWatchPanel");
        taskWatchText = taskWatchPanel
            .GetComponentsInChildren<Text>(true)
            .First(go => go.gameObject.name == "TaskWatchText")
            .GetComponent<Text>();
        taskWatchPanel.SetActive(false);

        formulaPanelObject = GameObject.Find("TaskFormulaPanel3Elem");
        formulaButtons = formulaPanelObject
            .GetComponentsInChildren<Button>(true);
        formulaPanelObject.SetActive(false);
        
        variablesPanel3ElemObject = GameObject.Find("TaskVariablesPanel3Elem");
        variables3ElemButtons = variablesPanel3ElemObject
            .GetComponentsInChildren<Button>(true);
        variablesPanel3ElemObject.SetActive(false);
        
        variablesPanel2ElemObject = GameObject.Find("TaskVariablesPanel2Elem");
        variables2ElemButtons = variablesPanel2ElemObject
            .GetComponentsInChildren<Button>(true);
        variablesPanel2ElemObject.SetActive(false);
        
        variablesWatchPanel = GameObject.Find("VariablesWatchPanel");
        variablesWatchText = variablesWatchPanel
            .GetComponentsInChildren<Text>();
        variablesWatchPanel.SetActive(false);

        box2Elem = GameObject.Find("2ElemBox");
        box2Elem.SetActive(false);
        box3Elem = GameObject.Find("3ElemBox");
        box3Elem.SetActive(false);

        leftBallCollider = GameObject.Find("LeftBallCollider");
        middleBallCollider = GameObject.Find("MiddleBallCollider");
        rightBallCollider = GameObject.Find("RightBallCollider");
        bigRightBallCollider = GameObject.Find("BigRightBallCollider");
        
        damageGashaponObject = GameObject.Find("GashaponDamagedPanel");
        damageGashaponObject.SetActive(false);
        
        breakGashaponObject = GameObject.Find("GashaponBrokenPanel");
        breakGashaponObject.SetActive(false);
        
        toyWinPanelObject = GameObject.Find("ToyWinPanel");
        toyWinPanelObject.SetActive(false);

        collectionPanel = GameObject.Find("CollectionPanel");
        collectionToysImages = collectionPanel
            .GetComponentsInChildren<Image>();
        collectionPanel.SetActive(false);

        //  ToysSlots = new GameObject[3];
        //  ToysSlots = GameObject.Find("ToysPanel")
        //      .GetComponentsInChildren<GameObject>(true)
        //      .Where(go => go.gameObject.name == "Toy")
        //      .AsEnumerable();
        
        SavedGashapons = new GameObject[3];
        
        CollectTasksFromFiles();
        DebugLogTasks(EasyTasks);
    }

    private void FixedUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "Gashapon_Coins(Clone)" && 
                    SavedGashapons.Count(x => x != null) < 3) {
                    SavedGashapons = GameManager.SavedGashapons.Append(hit.collider.gameObject);
                    Debug.Log(GameManager.SavedGashapons.Count(x => x != null));
                    hit.collider.gameObject.SetActive(false);
                    //  ToysSlots.First(go => !go.GetComponentInChildren<Image>().enabled)
                    //      .GetComponentInChildren<Image>().enabled = true;
                }
            }
        }
    }

    private void DebugLogTasks(IEnumerable<Task> tasks) {
        foreach (var task in tasks) {
            var str = "";
            str += "Id=" + task.Id;
            str += "; Difficulty=" + task.Difficulty;
            str += " FormulaId=" + task.FormulaId;
            str += "; NeededValues="; //+ task.NeededValues;
            foreach (var e in task.NeededValues)
                str += e + ";";
            str += "; InputValues="; //+ task.InputValues;
            foreach (var e in task.InputValues)
                str += e + ";";
            str += "; Text=" + task.Text;
            
            Debug.Log(str);
        }
    }

    private static void CollectTasksFromFiles() {
        EasyTasks = CollectTasks(EasyTasks, _easyTasksFilename);
        NormalTasks = CollectTasks(NormalTasks, _normalTasksFilename);

        Tasks = new []{EasyTasks, NormalTasks};
        Tasks = (IEnumerable<Task>[]) Tasks.AsEnumerable();
    }

    private static Task CreateTaskFromString(string line) {
        var str = line.Split(';');
        var needed = str[3].Trim().Split(' ');
        var input = str[4].Trim().Split(' ');
        
        var id = int.Parse(str[0]);
        var difficulty = int.Parse(str[1]);
        var formulaId = int.Parse(str[2]);
        var neededValues = new double[needed.Length];
        var inputValues = new double[input.Length];
        var text = str[5];

        for (var i = 0; i < needed.Length; i++) 
            neededValues[i] = double.Parse(needed[i]);
        
        for (var i = 0; i < input.Length; i++) 
            inputValues[i] = double.Parse(input[i]);

        return new Task(id, difficulty, formulaId, neededValues, inputValues, text);
    }

    private static IEnumerable<Task> CollectTasks(IEnumerable<Task> tasks, string pathToFile) {
        if (pathToFile.Contains("norm"))
            return tasks;
        
        tasks = tasks.Append(CreateTaskFromString("1;0;0;5;5 10 150;Сколькими способами 5 человек могут встать в очередь в билетную кассу?"));
        tasks = tasks.Append(CreateTaskFromString("2;0;0;8;1 8 80;Восемь студентов пишут ответ на экзаменационный вопрос. Сколькими способами их могут последовательно вызвать отвечать?"));
        tasks = tasks.Append(CreateTaskFromString("3;0;0;3;3 4 9;Сколько различных трёхзначных чисел можно составить при помощи цифр 4, 7, 9? (Цифры в записи числа не повторяются)."));
        tasks = tasks.Append(CreateTaskFromString("4;0;0;7;3 4 7;Сколько слов можно получить, переставляя буквы в слове Игрушка?"));
        tasks = tasks.Append(CreateTaskFromString("5;1;0;16;16 17;Семнадцать девушек водят хоровод. Сколькими различными способами они могут встать в круг?"));
        tasks = tasks.Append(CreateTaskFromString("6;0;1;4 7;4 7 11;Сколькими способами можно выбрать обед, состоящий из 4 блюд, из 7 различных блюд?"));
        tasks = tasks.Append(CreateTaskFromString("7;0;1;2 18;2 18 153;Чемпионат России по шахматам проводится в один круг. Сколько играется партий, если участвуют 18 шахматистов?"));
        tasks = tasks.Append(CreateTaskFromString("8;0;1;2 5;2 5 10;Сколькими способами можо выбрать 2 книги из 5?"));
        tasks = tasks.Append(CreateTaskFromString("9;1;1;4 10;4 6 10;Учащимся дали список из 10 книг, которые рекомендуется прочитать во время каникул. Сколькими способами ученик может выбрать из них 6 книг?"));
        tasks = tasks.Append(CreateTaskFromString("10;1;1;5 7;3 5 7;В студенческой столовой продают сосиски в тесте, ватрушки и пончики. Сколькими способами можно приобрести пять пирожков?"));
        tasks = tasks.Append(CreateTaskFromString("11;0;2;4 9;4 5 9;В пассажирском поезде 9 вагонов. Сколькими способами можно рассадить в поезде 4 человека, при условии, что все они должны ехать в различных вагонах?"));
        tasks = tasks.Append(CreateTaskFromString("12;0;2;5 11;5 6 11;Сколькими способами можно выбрать четырёх человек на четыре различные должности, если имеется девять кандидатов на эти должности?"));
        tasks = tasks.Append(CreateTaskFromString("13;0;2;3 7;3 4 7;Сколькими способами можно составить флаг, состоящий из трех горизонтальных полос различных цветов, если имеется материал пяти цветов?"));
        tasks = tasks.Append(CreateTaskFromString("14;0;2;3 36;3 33 36;Боря, Дима и Володя сели играть в «очко». Сколькими способами им можно сдать по одной карте? (колода содержит 36 карт)"));
        tasks = tasks.Append(CreateTaskFromString("15;1;2;2 4;2 4 16 8;Сколькими способами можно взять на руки 2 котов, одного на левую руку, другого - на правую, из 4 котов?"));

        return tasks; //    Костыль, потому что при билде меняется расположение папок, файлов и иная структура проекта
        try {
            var reader = new StreamReader(pathToFile);
            while (!reader.EndOfStream) {
                var str = reader.ReadLine();
                tasks = tasks.Append(CreateTaskFromString(str));
            }

            return tasks;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static void OpenTaskWindow(Gashapon gashapon) {
        gashapon.task ??= Task.GetRandomTask(0);
        taskText.text = gashapon.task.Text;
        taskWatchText.text = gashapon.task.Text;

        taskPanelObject.SetActive(true);
        formulaPanelObject.SetActive(true);

        foreach (var text in variablesWatchText) {
            text.text = "";
            text.color = Color.black;
        }
        
        for (var i = 0; i < gashapon.task.InputValues.Length; i++) {
            variablesWatchText[i].text = gashapon.task.InputValues[i].ToString();
        }
        variablesWatchText.First().color = Color.white;

        for (var i = 0; i < formulaButtons.Length; i++) {
            formulaButtons[i].onClick.RemoveAllListeners();
            if (i == gashapon.task.FormulaId)
                formulaButtons[i].onClick.AddListener(() => TaskNextStep(gashapon));
            else
                formulaButtons[i].onClick.AddListener(() => DamageGashapon(gashapon));
        }

        SetVariablesButtons(gashapon, 0);
        
        formulaPanelObject.SetActive(false);
    }

    private static void SetVariablesButtons(Gashapon gashapon, int currentIndex) {
        var currentButtons = variables3ElemButtons;
        if (gashapon.task.NeededValues.Length == 1) 
            currentButtons = variables2ElemButtons;
        
        if (gashapon.task.InputValues.Length == currentIndex) {
            toyWinPanelObject.SetActive(true);
            toyWinPanelObject.GetComponentInChildren<Image>().sprite = 
                collectionToysImages.First(toy => toy.color == Color.black).sprite;

            toyWinPanelObject.GetComponentInChildren<Text>().text =
                collectionToysImages.First(toy => toy.color == Color.black).gameObject.name;
            
            variablesPanel2ElemObject.SetActive(false);
            variablesPanel3ElemObject.SetActive(false);
            gashapon.gameObject.SetActive(false);
            
            mainBottomInventoryPanel.SetActive(true);
            mainBottomTaskPanel.SetActive(false);
            variablesWatchPanel.SetActive(false);

            Box3Elements.SetActive(false);
            Box2Elements.SetActive(false);
            PoolGashapons.SetActive(true);
            HaveTaskOnSolve = false;
            collectionToysImages.First(toy => toy.color == Color.black).color = Color.white;
            return;
        }
        
        foreach (var button in currentButtons) 
            button.onClick.RemoveAllListeners();

        for (var i = 0; i < gashapon.task.InputValues.Length; i++) {
            variablesWatchText[i].color = i == currentIndex ? Color.white : Color.black;
        }
        
        if (gashapon.task.NeededValues.Contains(gashapon.task.InputValues[currentIndex]))
            currentButtons[currentButtons.Length - 1].onClick.AddListener(() => DamageGashapon(gashapon));
        else 
            currentButtons[currentButtons.Length - 1].onClick.AddListener(() => SetVariablesButtons(gashapon, currentIndex + 1));
        
        for (var i = 0; i < gashapon.task.NeededValues.Length; i++) {
            if (gashapon.task.InputValues[currentIndex] == gashapon.task.NeededValues[i])
                currentButtons[i].onClick.AddListener(() => SetVariablesButtons(gashapon, currentIndex + 1));
            else
                currentButtons[i].onClick.AddListener(() => DamageGashapon(gashapon));
        }
    }
    
    private static void TaskNextStep(Gashapon gashapon) {
        formulaPanelObject.SetActive(!formulaPanelObject.activeSelf);
        variablesWatchPanel.SetActive(true);
        PoolGashapons.SetActive(false);
        Box3Elements.SetActive(!Box3Elements.activeSelf);
        if (gashapon.task.NeededValues.Length == 2) {
            variablesPanel3ElemObject.SetActive(!variablesPanel3ElemObject.activeSelf);
            Box3Elements.SetActive(!Box3Elements.activeSelf);
        }
        else {
            variablesPanel2ElemObject.SetActive(!variablesPanel2ElemObject.activeSelf);
            Box2Elements.SetActive(!Box2Elements.activeSelf);
        }
    }
    
    private static void DamageGashapon(Gashapon gashapon) {
        gashapon.health--;
        if (gashapon.health > 0)
        {
            //gashapon.gameObject.GetComponent<RawImage>().texture = new Texture().
            damageGashaponObject.SetActive(true);
        }
        else
        {
            breakGashaponObject.SetActive(true);
            gashapon.gameObject.SetActive(false);
            formulaPanelObject.SetActive(false);
            variablesPanel2ElemObject.SetActive(false);
            variablesPanel3ElemObject.SetActive(false);
            box2Elem.SetActive(false);
            box3Elem.SetActive(false);
            Box2Elements.SetActive(false);
            Box3Elements.SetActive(false);
            PoolGashapons.SetActive(true);
            mainBottomInventoryPanel.SetActive(true);
            mainBottomTaskPanel.SetActive(false);
            variablesWatchPanel.SetActive(false);

            HaveTaskOnSolve = false;
            //    ShowGashapons();

            //  gashapon.gameObject.SetActive(false); TODO Починить, выключает весь префаб, при перезапуске нет шаров
        }
    }
    
    public void TurnOnBoolOnClickSolveButton()
    {
        HaveTaskOnSolve = true;
    }
}


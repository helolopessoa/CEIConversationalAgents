using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    public static bool gameStarted {get; private set; }
    public static float initialTimer  {get; private set; } = 60000f; //180f
    public static float timeRemaining  {get; private set; } = 60000f; //180f

    [Header("Data testing settings")]
    public static bool baselineTest = true;
    
    // public static bool timerIsRunning  {get; private set; } = false;
    
    [Header("UI")]
    [SerializeField] private GameObject MainMenuUI;    
    [SerializeField] private GameObject GameMenuUI;    
    // [SerializeField] private GameObject TimerUI;
    [SerializeField] private TMP_Text TimerTextUI;
    [SerializeField] private GameObject FormMenuUI;    
    [SerializeField] private GameObject EndGameMenuUI;    
    [SerializeField] private GameObject ChatUI;    
    [SerializeField] private TMP_InputField usernameInput;   
    private NPC[] npcs;
    private PlayerController2D player;
    [HideInInspector] public bool saveGame = false;
    [HideInInspector] public bool? successOnPeaceTreatyRangers;
    [HideInInspector] public bool? successOnPeaceTreatyDownsides;
    [HideInInspector] public bool successOnPeaceTreaty;
    private bool timerEnded;
    [SerializeField] private TMP_Text endgameMessageOutput;
    public string playerName = "";

    void Awake()
    {
        Debug.Log("GameManager Awake: " + this.GetInstanceID());
        endgameMessageOutput.text = "GAME FINISHED";
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        npcs = Object.FindObjectsByType<NPC>(FindObjectsSortMode.None);
        player = GameObject.Find("Player").GetComponent<PlayerController2D>();
        // FindObjectsByType<PlayerController2D>(FindObjectsSortMode.None);
        Debug.Log("[GameManager] Found " + npcs.Length + " NPCs in the scene.");
        gameStarted = false;
    }
    public void Start()
    {
        gameStarted = false;
        // FormMenuUI.SetActive(false);
        EndGameMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
        GameMenuUI.SetActive(false);
        ChatUI.SetActive(false);
        usernameInput.ActivateInputField();
        if (usernameInput != null && usernameInput.isFocused)
        {
            usernameInput.text = string.Empty;
            // usernameInput.onSubmit.AddListener(StartGame);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        // timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        if(!gameStarted) return;

        if (timeRemaining >= 0)
        {
            DisplayTime(timeRemaining);
            timeRemaining -= Time.deltaTime;
        }

        timerEnded = timeRemaining <= 0f;
        bool bothDecided = successOnPeaceTreatyDownsides.HasValue && successOnPeaceTreatyRangers.HasValue;

        // if (timerEnded || bothDecided)
        if (bothDecided)
        {
            EndGame();
        }
    }
    public void StartGame()
    {
        playerName = usernameInput.text;
        StartNPCs();
        MainMenuUI.SetActive(false);
        GameMenuUI.SetActive(true);
        ChatUI.SetActive(false);
        timeRemaining = initialTimer;
        gameStarted = true;
        player.ResumeMovement();
        Debug.Log("[GameManager] StartGame called. Variable gameStarted: " + gameStarted);
    }
    public void RestartGame()
    {
        saveGame = false;
        gameStarted = false;
        FormMenuUI.SetActive(false);
        EndGameMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
        ChatUI.SetActive(false);
        Debug.Log("[GameManager] RestartGame called. Variable gameStarted: " + gameStarted);
    }

    public void FormMenu()
    {
        FormMenuUI.SetActive(true);
        EndGameMenuUI.SetActive(false);
    }


    public void EndGame()
    {
        EndGameNPCs();
        EndGameMenuUI.SetActive(true);
        GameMenuUI.SetActive(false);
        ChatUI.SetActive(false);
        player.StopMovement();
        bool downsides = successOnPeaceTreatyDownsides ?? false;
        bool rangers   = successOnPeaceTreatyRangers ?? false;
        gameStarted = false;
        successOnPeaceTreaty = downsides && rangers;
        Debug.Log("timerEnded," + timerEnded);
        Debug.Log("successOnPeaceTreatyDownsides," + successOnPeaceTreatyDownsides);
        Debug.Log("successOnPeaceTreatyRangers," + successOnPeaceTreatyRangers);
        Debug.Log("rangers," + rangers);
        Debug.Log("downsides," + downsides);
        Debug.Log("successOnPeaceTreaty," + successOnPeaceTreaty);

        switch ((timerEnded, successOnPeaceTreaty, downsides, rangers))
        {
            case (false, true, true, true): // Ambos os líderes aceitaram o tratado antes do limite de tempo
                endgameMessageOutput.text = "Congratulations! You successfully negotiated a peace treaty between the Rangers and the Downsides. Your diplomatic skills have saved the day!";
                break;

            case (true, true, true, true): // Tempo acabou, mas ambos os líderes aceitaram o tratado
                endgameMessageOutput.text = "Your time is over, and both leaders have agreed to your conditions. Congratulations! You successfully negotiated a peace treaty between the Rangers and the Downsides. Your diplomatic skills have saved the day!";
                break;

            case (false, false, false, false): // Ambos os líderes negaram o tratado antes do limite de tempo
                endgameMessageOutput.text = "Unfortunately, the peace treaty negotiations were unsuccessful. The conflict between the Rangers and the Downsides continues. War devastates the area...";
                break;

            case (true, false, false, false): // Tempo acabou, ambos os líderes negaram o tratado
                endgameMessageOutput.text = "Your time is over. Unfortunately, the peace treaty negotiations were unsuccessful. The conflict between the Rangers and the Downsides continues. War devastates the area...";
                break;

            case (true, false, true, false): // Tempo acabou, o líder Downside aceitou o tratado, mas o líder Ranger não
                endgameMessageOutput.text = "Your time is over. Despite convincing the Downside leader, the Rangers were still against peace, and so, the conflict continues, and war devastates the area...";
                break;

            case (true, false, false, true): // Tempo acabou, o líder Ranger aceitou o tratado, mas o líder Downside não
                endgameMessageOutput.text = "Your time is over. Despite convincing the Ranger leader, the Downsides were still against peace, and so, the conflict continues, and war devastates the area...";
                break;                    

            case (false, false, false, true): // O líder Ranger aceitou o tratado, mas o líder Downside não
                endgameMessageOutput.text = "Unfortunately, the peace treaty negotiations were unsuccessful. Despite convincing the Ranger leader, the Downsides were still against peace, and so, the conflict continues, and war devastates the area...";
                break;

            case (false, false, true, false): // O líder Downside aceitou o tratado, mas o líder Ranger não
                endgameMessageOutput.text = "Unfortunately, the peace treaty negotiations were unsuccessful. Despite convincing the Downside leader, the Rangers were still against peace, and so, the conflict continues, and war devastates the area...";
                break;
            default:
                endgameMessageOutput.text = "The peace treaty negotiations have concluded with an unexpected outcome. The situation remains uncertain, and the future of the conflict between the Rangers and the Downsides is unclear.";
                break;                 
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGameData(SaveGameData data)
    {
        // Implement saving logic here (e.g., write to file, PlayerPrefs, etc.)
        Debug.Log("Game data saved for NPC: " + data.npcName);
        Debug.Log("Game data saved: " + data);
        data.peaceTreatySigned = successOnPeaceTreaty;
        SaveGameChat.SaveGameData(data, baselineTest);
    }

    public static void SetRangersPeaceTreatyResult(bool? result) => GameManager.Instance.successOnPeaceTreatyRangers = result; 
    public static void SetDownsidePeaceTreatyResult(bool? result) => GameManager.Instance.successOnPeaceTreatyDownsides = result; 
    

    void StartNPCs()
    {
        foreach (var npc in npcs)
        {
            npc.StartNPC();
        }
    }

    void EndGameNPCs()
    {
        foreach (var npc in npcs)
        {
            npc.EndGameNPC();
        }
    }


}
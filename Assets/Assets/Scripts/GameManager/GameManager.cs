using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    
    [Header("UI")]
    [SerializeField] private GameObject MainMenuUI;    
    [SerializeField] private GameObject GameMenuUI;    
    [SerializeField] private GameObject FormMenuUI;    
    [SerializeField] private GameObject ChatUI;    
    [SerializeField] private TMP_InputField usernameInput;   
    private NPC[] npcs;
    private PlayerController2D player;
    public bool saveGame = false;
    public bool gameStarted = false;
    public string playerName = "";

    void Awake()
    {
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
    }
    public void Start()
    {
        gameStarted = false;
        FormMenuUI.SetActive(false);
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
    public void StartGame()
    {
        playerName = usernameInput.text;
        FormMenuUI.SetActive(false);
        MainMenuUI.SetActive(false);
        GameMenuUI.SetActive(true);
        ChatUI.SetActive(false);
        gameStarted = true;
        StartNPCs();
        player.ResumeMovement();

    }
    public void RestartGame()
    {
        saveGame = false;
        gameStarted = false;
        FormMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
        ChatUI.SetActive(false);
    }
    public void EndGame()
    {
        EndGameNPCs();
        gameStarted = false;
        FormMenuUI.SetActive(true);
        GameMenuUI.SetActive(false);
        ChatUI.SetActive(false);
        player.StopMovement();
    }
    public void SaveGameData(SaveGameData data)
    {
        // Implement saving logic here (e.g., write to file, PlayerPrefs, etc.)
        Debug.Log("Game data saved for NPC: " + data.npcName);
        Debug.Log("Game data saved: " + data);
        SaveGameChat.SaveGameData(data);
    }

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
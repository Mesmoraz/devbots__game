                                                                                                                             using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("Bot controllers for each bot")]
    [SerializeField] BotController playerBotController;
    [SerializeField] BotController enemyBotController;
    [Header("Bot health bar for each bot")]
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Slider enemyHealthBar;
    
    private void Awake()
    {
        //Define which bot is which
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
        foreach (GameObject bot in bots) {
            if (bot.GetComponent<BotSensor>().IsPlayer()) {
                playerBotController = bot.GetComponent<BotController>();
            }
            else {
                enemyBotController = bot.GetComponent<BotController>();
            }
        }

        //Add listeners to the damage taken events attatched to both bots
        enemyBotController.DamageTakenEvent.AddListener(OnEnemyDamageTaken);
        playerBotController.DamageTakenEvent.AddListener(OnPlayerDamageTaken);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //set max value of sliders belonging to both bot health bars
        playerHealthBar.maxValue =1f;
        enemyHealthBar.maxValue = 1f;
    }

    //When a bot takes damage these functions are called 
    private void OnPlayerDamageTaken()
    {
        //When player takes damage the player HP slider value is set to the current health of the players bot
        playerHealthBar.value = playerBotController.GetHP;
    }
    private void OnEnemyDamageTaken()
    {
        //When enemy takes damage the enemy HP slider value is set to the current health of the enemy bot
        enemyHealthBar.value = enemyBotController.GetHP;
    }
}

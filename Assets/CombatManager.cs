using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public TMP_Text combatLog;
    public TMP_Text statsText; // Reference to the TextMeshPro text component for displaying character stats

    private bool isPlayerTurn = true;
    private int playerExp = 0;
    private int playerHealth = 100;
    private int playerDamage = 10;

    private int enemyHealth; // Store the enemy's health as a class member variable

    private bool isGameOver = false;

    private void Start()
    {
        combatLog.text = "Combat started. Player's turn.";
        UpdateStatsText();
        enemyHealth = Random.Range(10, 20); // Initialize the enemy's health at the start of combat
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reload the scene to restart the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPlayerTurn)
                PlayerAttack();
            else
                EnemyAttack();
        }
    }

    private void PlayerAttack()
    {
        enemyHealth -= playerDamage;
        combatLog.text = "Player attacks for " + playerDamage + " damage.";

        if (enemyHealth <= 0)
        {
            combatLog.text += "\nEnemy defeated! Combat over.";
            IncreaseExperience(1); // Increase player's experience by 1 when an enemy is defeated
        }
        else
        {
            combatLog.text += "\nEnemy health: " + enemyHealth;
            isPlayerTurn = false;
        }

        UpdateStatsText(); // Update character stats display

        if (playerHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void EnemyAttack()
    {
        int damage = Random.Range(3, 8);

        playerHealth -= damage;
        combatLog.text = "Enemy attacks for " + damage + " damage.";

        if (playerHealth <= 0)
        {
            combatLog.text += "\nPlayer defeated! Combat over.";
            PlayerDeath(); // Player is defeated
        }
        else
        {
            combatLog.text += "\nPlayer health: " + playerHealth;
            isPlayerTurn = true;
        }

        UpdateStatsText(); // Update character stats display
    }

    private void IncreaseExperience(int amount)
    {
        playerExp += amount;
        combatLog.text += "\nPlayer gained " + amount + " experience.";
        combatLog.text += "\nPlayer's experience: " + playerExp;

        if (playerExp >= 5)
        {
            PlayerWins();
        }
    }

    private void UpdateStatsText()
    {
        // Update the statsText with the current player stats
        statsText.text = "Health: " + playerHealth +
                         "\nDamage: " + playerDamage +
                         "\nExperience: " + playerExp;
    }

    private void PlayerDeath()
    {
        // Display a message when the player dies
        combatLog.text += "\nPlayer has died!";
        isGameOver = true;
        combatLog.text += "\nGame Over! Press 'R' to restart.";
        // Implement other game over logic or desired actions
    }

    private void PlayerWins()
    {
        // Display a message when the player wins
        combatLog.text += "\nPlayer wins!";
        isGameOver = true;
        combatLog.text += "\nGame Over! Press 'R' to restart.";
        // Implement other win logic or desired actions
    }
}

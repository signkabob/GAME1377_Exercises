/**
 * Exercise 02 - MonsterBrawl.cs
 * Name: Ka Bo Cheung
 * Date: 06/16/2026
 * Course: GAME-1377-001
 *
 *Script for the battle simulation between monsters
 */
using UnityEngine;

public class MonsterBrawl : MonoBehaviour
{
    void Start()
    {
        string[] monsterNames = { "Goblin", "Orc", "Troll", "Skeleton", "Ogre" };
        int[] attackStats = { 8, 20, 35, 12, 50 };
        int[] healthStats = { 30, 80, 200, 50, 250 };
        int[] speedStats = { 1, 2, 3, 1, 4 };

        // Display the monster roster
        for (int i = 0; i < monsterNames.Length; i++)
        {
            Debug.Log(monsterNames[i] + " | ATK: " + attackStats[i] + " | HP: " + healthStats[i] + " | SPD: " + speedStats[i]);
        }

        // Declare variables for the battle simulation
        string monster1Name;
        int monster1Health;
        string monster2Name;
        int monster2Health;
        int turn;

        // Monster #1 selection
        for (int monster1 = 0;  monster1 < monsterNames.Length; monster1++)
        {
            // Monster #2 selection; Skip used monster #1 for each iteration 
            for (int monster2 = monster1; monster2 < monsterNames.Length; monster2++)
            {
                // Monster shouldn't fight its own kind
                if (monster1 == monster2)
                {
                    continue;
                } 

                // Initialize variables for each unique battle
                monster1Name = monsterNames[monster1];
                monster1Health = healthStats[monster1];
                monster2Name = monsterNames[monster2];
                monster2Health = healthStats[monster2];
                turn = 0;

                // Battle simulation between monster #1 and monster #2 until either one is dead
                while (monster1Health > 0 && monster2Health > 0)
                {
                    turn++;
                    // Monster #1's turn to attack based on its speed
                    if (TimeToAttack(turn, speedStats[monster1]))
                    {
                        Attack(attackStats[monster1], ref monster2Health); 
                    }
                    // Monster #2's turn to attack based on its speed
                    if (TimeToAttack(turn, speedStats[monster2]))
                    {
                        Attack(attackStats[monster2], ref monster1Health);
                    }
                }

                // If both monsters die on the same turn...
                if (monster1Health <= 0 && monster2Health <= 0)
                {
                    DisplayResult(monster1Name, monster2Name, true, monster1Name, turn, monster1Health);
                }
                // If monster #1 defeats monster #2...
                else if (monster2Health <= 0)
                {
                    DisplayResult(monster1Name, monster2Name, false, monster1Name, turn, monster1Health);
                }
                // If monster #2 defeats monster #1...
                else
                {
                    DisplayResult(monster1Name, monster2Name, false, monster2Name, turn, monster2Health);
                }
            }
        }
    }

    /// <summary>
    /// Determines whether it is time for the monster to attack
    /// </summary>
    /// <param name="turn">Number of turn</param>
    /// <param name="speed">Monster speed</param>
    /// <returns>Checks if the turn number is a multiple of the monster speed</returns>
    private bool TimeToAttack(int turn, int speed)
    {
        if (turn % speed == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Applies damage to the opponent
    /// </summary>
    /// <param name="damage">Damage points dealt to the opponent</param>
    /// <param name="opponentHealth">Health points of the opponent</param>
    private void Attack(int damage, ref int opponentHealth)
    {
        opponentHealth -= damage;
    }

    /// <summary>
    /// Displays the battle result message
    /// </summary>
    /// <param name="monster1">Name of monster #1</param>
    /// <param name="monster2">Name of monster #2</param>
    /// <param name="tie">Is the battle a tie?</param>
    /// <param name="winner">Name of the winner</param>
    /// <param name="turn">Number of turn</param>
    /// <param name="remainingHP">Remaining health points of the winner</param>
    private void DisplayResult(string monster1, string monster2, bool tie, string winner, int turn, int remainingHP)
    {
        if (tie)
        {
            Debug.Log(monster1 + " vs " + monster2 + " | Draw | Turns: " + turn);
        }
        else
        {
            Debug.Log(monster1 + " vs " + monster2 + " | Winner: " + winner + " | Turns: " + turn + " | Remaining HP: " + remainingHP);

        }
    }
}


/**
 * Exercise 02 - MonsterBrawl.cs
 * Name: Ka Bo Cheung
 * Date: 06/15/2026
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
        int monster1Health;
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
                monster1Health = healthStats[monster1];
                monster2Health = healthStats[monster2];
                turn = 0;

                // Battle simulation between monster #1 and monster #2 until either one is dead
                while (monster1Health > 0 && monster2Health > 0)
                {
                    turn++;
                    // Monster #1's turn based on its speed
                    if (turn % speedStats[monster1] == 0)
                    {
                        monster2Health -= attackStats[monster1]; 
                    }
                    // Monster #2's turn based on its speed
                    if (turn % speedStats[monster2] == 0)
                    {
                        monster1Health -= attackStats[monster2];
                    }
                }

                // If both monsters die on the same turn...
                if (monster1Health <= 0 && monster2Health <= 0)
                {
                    Debug.Log(monsterNames[monster1] + " vs " + monsterNames[monster2] + " | Draw | Turns: " + turn);
                }
                // If monster #1 defeats monster #2...
                else if (monster2Health <= 0)
                {
                    Debug.Log(monsterNames[monster1] + " vs " + monsterNames[monster2] + " | Winner: " + monsterNames[monster1] 
                        + " | Turns: " + turn + " | Remaining HP: " + monster1Health);
                }
                // If monster #2 defeats monster #1...
                else
                {
                    Debug.Log(monsterNames[monster1] + " vs " + monsterNames[monster2] + " | Winner: " + monsterNames[monster2]
                        + " | Turns: " + turn + " | Remaining HP: " + monster2Health);
                }
            }
        }
    }
}


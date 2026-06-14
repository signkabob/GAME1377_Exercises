/**
 * Exercise 02 - MonsterBrawl.cs
 * Name: Ka Bo Cheung
 * Date: 06/13/2026
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
        int m1Health;
        int m2Health;
        int turn;

        // Monster #1 selection
        for (int m1 = 0;  m1 < monsterNames.Length; m1++)
        {
            // Monster #2 selection; Skip used monster #1 for each iteration 
            for (int m2 = m1; m2 < monsterNames.Length; m2++)
            {
                // Monster shouldn't fight its own kind
                if (m1 == m2)
                {
                    continue;
                } 

                // Initialize variables for each unique battle
                m1Health = healthStats[m1];
                m2Health = healthStats[m2];
                turn = 0;

                // Battle simulation between monster #1 and monster #2 until either one is dead
                while (m1Health > 0 && m2Health > 0)
                {
                    turn++;
                    // Monster #1's turn based on its speed
                    if (turn % speedStats[m1] == 0)
                    {
                        m2Health -= attackStats[m1]; 
                    }
                    // Monster #2's turn based on its speed
                    if (turn % speedStats[m2] == 0)
                    {
                        m1Health -= attackStats[m2];
                    }
                }

                // If both monsters die on the same turn...
                if (m1Health <= 0 && m2Health <= 0)
                {
                    Debug.Log(monsterNames[m1] + " vs " + monsterNames[m2] + " | Draw | Turns: " + turn);
                }
                // If monster #1 defeats monster #2...
                else if (m2Health <= 0)
                {
                    Debug.Log(monsterNames[m1] + " vs " + monsterNames[m2] + " | Winner: " + monsterNames[m1] 
                        + " | Turns: " + turn + " | Remaining HP: " + m1Health);
                }
                // If monster #2 defeats monster #1...
                else
                {
                    Debug.Log(monsterNames[m1] + " vs " + monsterNames[m2] + " | Winner: " + monsterNames[m2]
                        + " | Turns: " + turn + " | Remaining HP: " + m2Health);
                }
            }
        }
    }
}


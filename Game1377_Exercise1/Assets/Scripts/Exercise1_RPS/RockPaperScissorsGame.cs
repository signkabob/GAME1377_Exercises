/**
 * 
 * Exercise 01 - RockPaperScissorsGame.cs
 * Name: Ka Bo Cheung
 * Date: 06/12/2026
 * Course: GAME-1377-001
 *
 *Script for the RPS Manager to identify the hand choices and determine the winner of the game
 */
using UnityEngine;

public class RockPaperScissorsGame : MonoBehaviour
{
    public enum Choice
    {
        invalid,
        rock,
        paper,
        scissors,
        lizard,
        spock
    }

    private string[] choices = { "rock", "paper", "scissors", "lizard", "spock" };

    public void RockPaperScissors(int playerChoice)
    {
        // Generate a random integer from 1 to 5 for the computer choice 
        int computerChoice = Random.Range(1,6);

        // Display the choices from the player and the computer
        Debug.Log("You chose: " + (Choice) playerChoice);
        Debug.Log("Computer chose: " + (Choice) computerChoice);

        // A tie between the player and the computer
        if ((Choice) playerChoice == (Choice) computerChoice)
        {
            Debug.Log("It's a tie! Both chose " + (Choice) playerChoice);
        }
        // Rock beats Scissors and Lizard
        else if ((Choice) playerChoice == Choice.rock)
        {
            if ((Choice) computerChoice == Choice.scissors || (Choice) computerChoice == Choice.lizard)
            {
                Debug.Log("You win! " + (Choice) playerChoice + " beats " + (Choice) computerChoice);
            }
            else
            {
                Debug.Log("You lose! " + (Choice) computerChoice + " beats " + (Choice) playerChoice);
            }
        }
        // Scissors beats Paper and Lizard
        else if ((Choice)playerChoice == Choice.scissors)
        {
            if ((Choice)computerChoice == Choice.paper || (Choice)computerChoice == Choice.lizard)
            {
                Debug.Log("You win! " + (Choice)playerChoice + " beats " + (Choice)computerChoice);
            }
            else
            {
                Debug.Log("You lose! " + (Choice)computerChoice + " beats " + (Choice)playerChoice);
            }
        }
        // Paper beats Rock and Spock
        else if ((Choice)playerChoice == Choice.paper)
        {
            if ((Choice)computerChoice == Choice.rock || (Choice)computerChoice == Choice.spock)
            {
                Debug.Log("You win! " + (Choice)playerChoice + " beats " + (Choice)computerChoice);
            }
            else
            {
                Debug.Log("You lose! " + (Choice)computerChoice + " beats " + (Choice)playerChoice);
            }
        }
        // Lizard beats Paper and Spock
        else if ((Choice)playerChoice == Choice.lizard)
        {
            if ((Choice)computerChoice == Choice.paper || (Choice)computerChoice == Choice.spock)
            {
                Debug.Log("You win! " + (Choice)playerChoice + " beats " + (Choice)computerChoice);
            }
            else
            {
                Debug.Log("You lose! " + (Choice)computerChoice + " beats " + (Choice)playerChoice);
            }
        }
        // Spock beats Scissors and Rock
        else if ((Choice)playerChoice == Choice.spock)
        {
            if ((Choice)computerChoice == Choice.scissors || (Choice)computerChoice == Choice.rock)
            {
                Debug.Log("You win! " + (Choice)playerChoice + " beats " + (Choice)computerChoice);
            }
            else
            {
                Debug.Log("You lose! " + (Choice)computerChoice + " beats " + (Choice)playerChoice);
            }
        }
    }
}
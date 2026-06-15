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
        spock,
        length
    }

    public void RockPaperScissors(int playerChoice)
    {
        // Generate a random integer from 1 to 5 for the computer choice 
        int computerChoice = Random.Range(1,(int) Choice.length);
        // Set the computer as the default winner unless it loses
        bool isWinner = false;

        // Display the choices from the player and the computer
        Debug.Log("You chose: " + (Choice) playerChoice);
        Debug.Log("Computer chose: " + (Choice) computerChoice);

        // A tie between the player and the computer before exiting
        if ((Choice) playerChoice == (Choice) computerChoice)
        {
            Debug.Log("It's a tie! Both chose " + (Choice) playerChoice);
            return;
        }
        // Rock beats Scissors and Lizard
        else if ((Choice) playerChoice == Choice.rock)
        {
            if ((Choice) computerChoice == Choice.scissors || (Choice) computerChoice == Choice.lizard)
            {
                isWinner = true;
            }
        }
        // Scissors beats Paper and Lizard
        else if ((Choice)playerChoice == Choice.scissors)
        {
            if ((Choice)computerChoice == Choice.paper || (Choice)computerChoice == Choice.lizard)
            {
                isWinner = true;
            }
        }
        // Paper beats Rock and Spock
        else if ((Choice)playerChoice == Choice.paper)
        {
            if ((Choice)computerChoice == Choice.rock || (Choice)computerChoice == Choice.spock)
            {
                isWinner = true;
            }
        }
        // Lizard beats Paper and Spock
        else if ((Choice)playerChoice == Choice.lizard)
        {
            if ((Choice)computerChoice == Choice.paper || (Choice)computerChoice == Choice.spock)
            {
                isWinner = true;
            }
        }
        // Spock beats Scissors and Rock
        else if ((Choice)playerChoice == Choice.spock)
        {
            if ((Choice)computerChoice == Choice.scissors || (Choice)computerChoice == Choice.rock)
            {
                isWinner = true;
            }
        }

        // Display the result message
        if (isWinner)
        {
            Debug.Log("You win! " + (Choice)playerChoice + " beats " + (Choice)computerChoice);
        }
        else
        {
            Debug.Log("You lose! " + (Choice)computerChoice + " beats " + (Choice)playerChoice);
        }
    }
}
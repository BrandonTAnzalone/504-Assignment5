using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment5
{
    public class Puzzle
    {
        public int[,] PuzzleInitial;
        public int[,] PuzzleProgress;
        public int[,] PuzzleSolution;
        public int[,] TempSwap;
        public Difficulty PuzzleDifficulty;

        public Puzzle()
        {
            PuzzleInitial = new int[0, 0];
            PuzzleProgress = new int[0, 0];
            PuzzleSolution = new int[0, 0];
            PuzzleDifficulty = 0;
        }

        public Puzzle(int[,] newPuzzleInitial, int[,] newPuzzleProgress, int[,] newPuzzleSolution, Difficulty newPuzzleDifficulty)
        {
            PuzzleInitial = newPuzzleInitial;
            PuzzleProgress = newPuzzleProgress;
            PuzzleSolution = newPuzzleSolution;
            PuzzleDifficulty = newPuzzleDifficulty;
        }

        public bool IsValueInSolution(int value, int i, int j)
        {
            if (value == PuzzleSolution[i, j])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveState(int range)
        {
            //TempSwap = new int[5, 5];

                for (int i = 0; i < range; i++)
                {
                    for (int j = 0; j < range; j++)
                    {
                        PuzzleProgress[i, j] = PuzzleInitial[i, j];
                    }
                }
        }

        public void LoadState(int range)
        {
            
                for (int i = 0; i < range; i++)
                {
                    for (int j = 0; j < range; j++)
                    {
                        PuzzleInitial[i, j] = PuzzleProgress[i, j];
                    }
                }

     
        }

        public void Cheating()
        {

            Random Cheat = new Random();

            int choice1;
            int choice2;

            if (PuzzleDifficulty == 0)
            {
                choice1 = Cheat.Next(0, 3);
                choice2 = Cheat.Next(0, 3);

                while (true)
                {
                    if (PuzzleInitial[choice1, choice2] != PuzzleSolution[choice1, choice2])
                    {
                        PuzzleInitial[choice1, choice2] = PuzzleSolution[choice1, choice2];
                        break;
                    }
                    else
                    {
                        choice1 = Cheat.Next(0, 3);
                        choice2 = Cheat.Next(0, 3);
                    }
                }

            }
            else if (PuzzleDifficulty == (Difficulty)1)
            {

                choice1 = Cheat.Next(0, 5);
                choice2 = Cheat.Next(0, 5);

                MessageBox.Show(choice1 + " " + choice2);

                while (true)
                {
                    if (PuzzleInitial[choice1, choice2] != PuzzleSolution[choice1, choice2])
                    {
                        PuzzleInitial[choice1, choice2] = PuzzleSolution[choice1, choice2];
                        break;
                    }
                    else
                    {
                        choice1 = Cheat.Next(0, 5);
                        choice2 = Cheat.Next(0, 5);
                    }
                }
            }
            else if (PuzzleDifficulty == (Difficulty)2)
            {

                choice1 = Cheat.Next(0, 7);
                choice2 = Cheat.Next(0, 7);

                MessageBox.Show(choice1 + " " + choice2);

                while (true)
                {
                    if (PuzzleInitial[choice1, choice2] != PuzzleSolution[choice1, choice2])
                    {
                        PuzzleInitial[choice1, choice2] = PuzzleSolution[choice1, choice2];
                        break;
                    }
                    else
                    {
                        choice1 = Cheat.Next(0, 7);
                        choice2 = Cheat.Next(0, 7);
                    }
                }
            }

        }

        public void ResetPuzzle(int puzzleSize)
        {
       
                for (int i = 0; i < puzzleSize; i++)
                {
                    for (int j = 0; j < puzzleSize; j++)
                    {
                        if (PuzzleInitial[i, j] != PuzzleProgress[i, j])
                        {
                            PuzzleInitial[i, j] = 0;
                        }
                    }
                }
        }

        public bool IsCorrect()
        {
            if (PuzzleDifficulty == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (PuzzleInitial[i, j] != PuzzleSolution[i, j])
                            return false;
                    }
                }
            }
            else if (PuzzleDifficulty == (Difficulty)1)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (PuzzleInitial[i, j] != PuzzleSolution[i, j])
                            return false;
                    }
                }
            }
            else if (PuzzleDifficulty == (Difficulty)2)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (PuzzleInitial[i, j] != PuzzleSolution[i, j])
                            return false;
                    }
                }
            }

            return true;
        }
    }
}

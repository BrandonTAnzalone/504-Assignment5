using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace Assignment5
{
    public enum Difficulty { Easy, Medium, Hard };

    public partial class Form1 : Form
    {
        private static List<Puzzle> Puzzles;
        private static TextBox[,] GameMode;

        private Stopwatch stopwatch;
        private TimeSpan ElapsedTime = new TimeSpan();
        private static string DifficultySetting;
        private static int index;
        private static int sum;
        private static bool StartCheck = false;
        private static bool CheatCheck = false;

        // Scoreboard Check
        private TimeSpan Finshed = new TimeSpan();
        private static bool Solved = false;


        public Form1()
        {
            InitializeComponent();

            Puzzles = new List<Puzzle>();
            GameMode = new TextBox[9, 9];
       


            DiffOption.DataSource = Enum.GetValues(typeof(Difficulty));
            stopwatch = new Stopwatch();
            index = 0;

            BuildMode();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = PuzzleTitle;
        }

        public void ReadPuzzle(string difficulty)
        {

            int puzzleSize = PuzzleSize(difficulty);
            int maxRange = puzzleSize + 2;
            int number = 0;

            int[,] TempArray = new int[maxRange, maxRange];
            int[,] TempArrayProgress = new int[maxRange, maxRange];
            int[,] TempArraySolution = new int[maxRange, maxRange];

            for(int f = 1; f <= 3; f++)
            {

                TempArray = new int[maxRange, maxRange];
                TempArrayProgress = new int[maxRange, maxRange];
                TempArraySolution = new int[maxRange, maxRange];

                string fileName = string.Format("../../{0}/{1}{2}.txt", difficulty.ToLower(), difficulty.ToLower()[0], f);
                using (StreamReader PuzzleFile = new StreamReader(@fileName))
                {
                    string input = PuzzleFile.ReadLine();
                    int revDiag = puzzleSize - 1;

                    for (int i = 0; i < puzzleSize; i++)
                    {
                        int sumInitial = 0;
                        int sumProgress = 0; // Reset for new rows

                        for (int j = 0; j < puzzleSize; j++)
                        {
                            number = (int)Char.GetNumericValue(input[j]);
                            TempArray[i, j] = number; // TEST input[j]
                            sumInitial += number;
                            TempArrayProgress[i, j] = number; // TEST input[j]
                            sumProgress += number;

                            TempArray[maxRange - 2, j] += number;

                            if (i == j)
                                TempArray[maxRange - 2, maxRange - 2] += number;

                            if (revDiag == j)
                            {
                                TempArray[maxRange - 2, maxRange - 1] += number;
                                revDiag--;
                            }


                        }

                        TempArray[i, puzzleSize] = sumInitial; // Set the 3rd column sums
                        TempArrayProgress[i, puzzleSize] = sumProgress; // Set the 3rd column sums

                        input = PuzzleFile.ReadLine();

                    }

                    input = PuzzleFile.ReadLine();


                    revDiag = puzzleSize - 1;

                    for (int i = 0; i < puzzleSize; i++)
                    {
                        int sumSolution = 0;
                   

                        for (int j = 0; j < puzzleSize; j++)
                        {
                            number = (int)Char.GetNumericValue(input[j]);
                            TempArraySolution[i, j] = number; // TEST input[j]

                            sumSolution += number;
                            TempArray[maxRange - 1, j] += number;

                            if (i == j)
                                TempArray[maxRange - 1, maxRange - 2] += number;

                            if (revDiag == j)
                            {
                                TempArray[maxRange - 1, maxRange - 1] += number;
                                revDiag--;
                            }
                        }


                        TempArray[i, puzzleSize + 1] = sumSolution; // Set the 3rd column sums
                        input = PuzzleFile.ReadLine();

                    }

                    Puzzle TempPuzzle = new Puzzle(TempArray, TempArrayProgress, TempArraySolution, (Difficulty)Enum.Parse(typeof(Difficulty),difficulty));
                    Puzzles.Add(TempPuzzle);
                }
            }
        }

  
    public int PuzzleSize(string difficulty)
        {
            int puzzleSize = 0;
            switch (difficulty)
            {
                case "Easy":
                    puzzleSize = 3;
                    break;
                case "Medium":
                    puzzleSize = 5;
                    break;
                case "Hard":
                    puzzleSize = 7;
                    break;
            }
            return puzzleSize;
        }
    
     

        public void BuildMode()
        {
            GameMode[0, 0] = a1; GameMode[0, 1] = a2; GameMode[0, 2] = a3; GameMode[0, 3] = a4; GameMode[0, 4] = a5; GameMode[0, 5] = a6; GameMode[0, 6] = a7; GameMode[0, 7] = a8; GameMode[0, 8] = a9;
            GameMode[1, 0] = b1; GameMode[1, 1] = b2; GameMode[1, 2] = b3; GameMode[1, 3] = b4; GameMode[1, 4] = b5; GameMode[1, 5] = b6; GameMode[1, 6] = b7; GameMode[1, 7] = b8; GameMode[1, 8] = b9;
            GameMode[2, 0] = c1; GameMode[2, 1] = c2; GameMode[2, 2] = c3; GameMode[2, 3] = c4; GameMode[2, 4] = c5; GameMode[2, 5] = c6; GameMode[2, 6] = c7; GameMode[2, 7] = c8; GameMode[2, 8] = c9;
            GameMode[3, 0] = d1; GameMode[3, 1] = d2; GameMode[3, 2] = d3; GameMode[3, 3] = d4; GameMode[3, 4] = d5; GameMode[3, 5] = d6; GameMode[3, 6] = d7; GameMode[3, 7] = d8; GameMode[3, 8] = d9;
            GameMode[4, 0] = e1; GameMode[4, 1] = e2; GameMode[4, 2] = e3; GameMode[4, 3] = e4; GameMode[4, 4] = e5; GameMode[4, 5] = e6; GameMode[4, 6] = e7; GameMode[4, 7] = e8; GameMode[4, 8] = e9;
            GameMode[5, 0] = f1; GameMode[5, 1] = f2; GameMode[5, 2] = f3; GameMode[5, 3] = f4; GameMode[5, 4] = f5; GameMode[5, 5] = f6; GameMode[5, 6] = f7; GameMode[5, 7] = f8; GameMode[5, 8] = f9;
            GameMode[6, 0] = g1; GameMode[6, 1] = g2; GameMode[6, 2] = g3; GameMode[6, 3] = g4; GameMode[6, 4] = g5; GameMode[6, 5] = g6; GameMode[6, 6] = g7; GameMode[6, 7] = g8; GameMode[6, 8] = g9;
            GameMode[7, 0] = h1; GameMode[7, 1] = h2; GameMode[7, 2] = h3; GameMode[7, 3] = h4; GameMode[7, 4] = h5; GameMode[7, 5] = h6; GameMode[7, 6] = h7; GameMode[7, 7] = h8; GameMode[7, 8] = h9;
            GameMode[8, 0] = i1; GameMode[8, 1] = i2; GameMode[8, 2] = i3; GameMode[8, 3] = i4; GameMode[8, 4] = i5; GameMode[8, 5] = i6; GameMode[8, 6] = i7; GameMode[8, 7] = i8; GameMode[8, 8] = i9;
        }

        private void GeneratePuzzle(object sender, EventArgs e)
        {
            StartButton.Enabled = true;
            SaveButton.Enabled = true;
            CheatButton.Enabled = true;

            foreach (TextBox SingleItem in GameMode)
            {
                if (SingleItem.BackColor == Color.Green && SingleItem.Text == "")
                {
                    SingleItem.BackColor = Color.White;
                }
            }

            index = (int)PuzzleNum.Value - 1;
            DifficultySetting = DiffOption.SelectedItem.ToString();

            ReadPuzzle(DifficultySetting);
            InitPuzzle(DifficultySetting);
  

            CreatePuzzle(index, DifficultySetting);


            if (!StartCheck) 
                ManageTextBoxes(DifficultySetting, false);
            else
                ManageTextBoxes(DifficultySetting, true);

        }
        public void CreatePuzzle(int index2, string setting)
        {
            int rangeMax = PuzzleSize(setting);

            for (int y = 0; y < rangeMax; y++)
            {
                for (int z = 0; z < rangeMax; z++)
                {
                    GameMode[y, z].Visible = true;
                    if (Puzzles[index2].PuzzleInitial[y, z] != 0)
                    {
                        GameMode[y, z].Text = Puzzles[index2].PuzzleInitial[y, z].ToString();
                    }
                    else
                    {
                        GameMode[y, z].Text = "";
                    }

                }
            }
        }
        public void UpdateTotals(int index2, string setting)
        {
            int puzzleSize = PuzzleSize(setting);

            for (int y = 0; y < puzzleSize; y++)
            {
                for (int j = puzzleSize; j < puzzleSize + 2; j++)
                {
                    GameMode[y, j].Text = Puzzles[index2].PuzzleInitial[y, j].ToString();
                }
            }
            for (int i = puzzleSize; i < puzzleSize + 2; i++)
            {
                for(int c = 0; c < puzzleSize + 2; c++)
                {
                    GameMode[i, c].Text = Puzzles[index2].PuzzleInitial[i, c].ToString();
                }
            }
        }

        public void ManageTextBoxes(string setting, bool enable)
        {
            int puzzleSize = PuzzleSize(setting);

            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    TextBox currentTextBox = GameMode[i, j];
                    if (enable == true)
                    {
                        if (Puzzles[index].PuzzleProgress[i, j] == 0)
                        {
                            
                            currentTextBox.Enabled = enable;
                        }
                    }
                    else
                        currentTextBox.Enabled = enable;
                }
            }
        }

        public void InitPuzzle(string setting)
        {
            int puzzleSize = PuzzleSize(setting);

            for(int i = 0; i < puzzleSize; i++ )
            {
                for(int j = 0; j < puzzleSize; j++)
                {
                    TextBox currentTextBox = GameMode[i, j];
                    currentTextBox.TextChanged += new System.EventHandler(this.FieldTextChange);

                    if (currentTextBox.Text != "")
                    {
                        currentTextBox.BackColor = Color.Green;
                        currentTextBox.Enabled = false;
                    }
                    else
                    {
                        currentTextBox.BackColor = Color.White;
                    }
                }

            }
        }

        private void FieldTextChange(object sender, EventArgs e)
        {
            TextBox SingleItem = sender as TextBox;

            if (SingleItem.Text != "" && DifficultySetting == "Easy")
            {
                CalculateEasySums(SingleItem, index);
            }
            if (SingleItem.Text != "" && DifficultySetting == "Medium")
            {
                CalculateEasySums(SingleItem, index);
            }
            if (SingleItem.Text != "" && DifficultySetting == "Hard")
            {
                CalculateEasySums(SingleItem, index);
            }
        }

        private void FieldKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox SingleItem = sender as TextBox;
            SingleItem.MaxLength = 1;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void hasFocus(object sender, EventArgs e)
        {
            TextBox SingleItem = sender as TextBox;
            SingleItem.BackColor = Color.Cyan;
        }

        private void lostFocus(object sender, EventArgs e)
        {
            TextBox SingleItem = sender as TextBox;
            if (SingleItem.BackColor != Color.Green && SingleItem.Text != "")
            {
                SingleItem.BackColor = Color.Red;
            }
            if (SingleItem.Text == "")
                SingleItem.BackColor = Color.White;
        }

        public void CalculateEasySums(TextBox SingleItem, int index2)
        {
           int puzzleSize = PuzzleSize(DifficultySetting);
            // Change the puzzle in progress array based on user input
            for (int i = 0; i < puzzleSize; i++)
            {

                for (int j = 0; j < puzzleSize; j++)
                {
                    if (GameMode[i, j].Name == SingleItem.Name)
                    {
                        Puzzles[index2].PuzzleInitial[i, j] = (int)Char.GetNumericValue(SingleItem.Text[0]);

                        if (Puzzles[index2].IsValueInSolution((int)Char.GetNumericValue(SingleItem.Text[0]), i, j))
                        {
                            SingleItem.BackColor = Color.Green;
                            SingleItem.Enabled = false;
                        }
                        else
                        {
                            SingleItem.BackColor = Color.Red;
                        }

                        // Check if user has won
                        if (Puzzles[index2].IsCorrect() && !CheatCheck)
                        {
                            MessageBox.Show("You've completed the puzzle!\n");
                            UpdateScoreboard();

                        }
                        else if (Puzzles[index2].IsCorrect() && CheatCheck)
                        {
                            MessageBox.Show("You've completed the puzzle!... with cheats\n");
                            CheatButton.Enabled = false;
                        }
                    }

                }

            }

            // Calculate new sums for the rows
            for (int j = 0; j < puzzleSize; j++)
            {
                sum = 0;

                for (int i = 0; i < puzzleSize; i++)
                {
                    sum += Puzzles[index2].PuzzleInitial[j, i];
                }
                Puzzles[index2].PuzzleInitial[j, puzzleSize] = sum;
            }

            //Calculates new sums for the columns
            for (int j = 0; j < puzzleSize; j++)
            {
                sum = 0;

                for (int i = 0; i < puzzleSize; i++)
                {
                    sum += Puzzles[index2].PuzzleInitial[i, j];
                }
                Puzzles[index2].PuzzleInitial[puzzleSize, j] = sum;

            }


            Puzzles[index2].PuzzleInitial[puzzleSize, puzzleSize] = 0;
            Puzzles[index2].PuzzleInitial[puzzleSize, puzzleSize + 1] = 0;

            for (int i = 0; i < puzzleSize; i++) //Get new diagonal sum
            {
                Puzzles[index2].PuzzleInitial[puzzleSize, puzzleSize] += Puzzles[index2].PuzzleInitial[i, i];
            }

            int x = 0;
            for (int i = puzzleSize - 1; i >= 0; i--) // Get other diagonal sum
            {
                Puzzles[index2].PuzzleInitial[puzzleSize, puzzleSize + 1] += Puzzles[index2].PuzzleInitial[i, x];
                x++;
            }

            UpdateTotals(index2, DifficultySetting);
        
        }


        public void UpdateScoreboard()
        {
            ElapsedTime = stopwatch.Elapsed;
            PuzzleTimer.Stop();
            stopwatch.Stop();

            // Easy Puzzle
            if (DifficultySetting == "Easy")
            {
                if ((Finshed < ElapsedTime) && Solved)
                {
                    ScoreboardEasy.Clear();
                    ScoreboardEasy.Text += String.Format("{0:00}:{1:00}.{2:000} - {3}", Finshed.Minutes, Finshed.Seconds, Finshed.Milliseconds, DifficultySetting);
                }
                else
                {
                    ScoreboardEasy.Clear();
                    ScoreboardEasy.Text += String.Format("{0:00}:{1:00}.{2:000} - {3}", ElapsedTime.Minutes, ElapsedTime.Seconds, ElapsedTime.Milliseconds, DifficultySetting);
                }

                Finshed = ElapsedTime;
                Solved = true;
            }
            // Medium Puzzle
            if (DifficultySetting == "Medium")
            {
                if ((Finshed < ElapsedTime) && Solved)
                {
                    ScoreboardEasy.Clear();
                    ScoreboardEasy.Text += String.Format("{0:00}:{1:00}.{2:000} - {3}", Finshed.Minutes, Finshed.Seconds, Finshed.Milliseconds, DifficultySetting);
                }
                else
                {
                    ScoreboardEasy.Clear();
                    ScoreboardEasy.Text += String.Format("{0:00}:{1:00}.{2:000} - {3}", ElapsedTime.Minutes, ElapsedTime.Seconds, ElapsedTime.Milliseconds, DifficultySetting);
                }

                Finshed = ElapsedTime;
                Solved = true;
            }
            // Hard Puzzle
            if (DifficultySetting == "Hard")
            {
                if ((Finshed < ElapsedTime) && Solved)
                {
                    ScoreboardEasy.Clear();
                    ScoreboardEasy.Text += String.Format("{0:00}:{1:00}.{2:000} - {3}", Finshed.Minutes, Finshed.Seconds, Finshed.Milliseconds, DifficultySetting);
                }
                else
                {
                    ScoreboardEasy.Clear();
                    ScoreboardEasy.Text += String.Format("{0:00}:{1:00}.{2:000} - {3}", ElapsedTime.Minutes, ElapsedTime.Seconds, ElapsedTime.Milliseconds, DifficultySetting);
                }

                Finshed = ElapsedTime;
                Solved = true;
            }
        }

        private void CheatButtonClick(object sender, EventArgs e)
        {

            DialogResult Confirm = MessageBox.Show("Cheating will invalidate your time! Continue?", "Cheat Confirmation", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information);

            if (Confirm == DialogResult.Yes)
            {
                CheatCheck = true;
                PuzzleTimer.Stop();
                stopwatch.Reset();

                if (DifficultySetting == "Easy")
                    Puzzles[index].Cheating();
                if (DifficultySetting == "Medium")
                    Puzzles[index].Cheating();
                if (DifficultySetting == "Hard")
                    Puzzles[index].Cheating();

                GeneratePuzzle(sender, e);
            }
        }

        private void showTiles()
        {
            DifficultySetting = DiffOption.SelectedItem.ToString();

            int puzzleSize = PuzzleSize(DifficultySetting);

            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = puzzleSize ; j < puzzleSize + 2; j++)
                {
                    GameMode[i, j].Visible = true;
                    GameMode[i,j].Enabled = false;
                    GameMode[i, j].Text = Puzzles[index].PuzzleInitial[i, j].ToString();
                }
            }

            for (int i = puzzleSize; i < puzzleSize + 2; i++)
            {
                for (int j = 0; j < puzzleSize + 2; j++)
                {
                    GameMode[i, j].Visible = true;
                    GameMode[i, j].Enabled = false;
                    GameMode[i, j].Text = Puzzles[index].PuzzleInitial[i, j].ToString();
                }
            }
        }

        private void StartPuzzleClick(object sender, EventArgs e)
        {

            if (StartButton.Text == "Start Puzzle")
            {
                // Disable generation options
                GenPuzzle.Enabled = false;
                DiffOption.Enabled = false;
                PuzzleNum.Enabled = false;
                ResetButton.Enabled = false;

                showTiles();

                StartCheck = true;
                GeneratePuzzle(sender, e);

                PuzzleTimer.Start();
                stopwatch.Start();

                StartButton.Text = "Stop Puzzle";
            }
            else if (StartButton.Text == "Stop Puzzle")
            {
                StartCheck = false;
                GeneratePuzzle(sender, e);
                PuzzleTimer.Stop();
                stopwatch.Stop();
                ResetButton.Enabled = true;

                StartButton.Text = "Start Puzzle";

            }

        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (!CheatCheck)
            {
                ElapsedTime = stopwatch.Elapsed;
                TimerBox.Text = String.Format("{0:00}:{1:00}.{2:000}", ElapsedTime.Minutes, ElapsedTime.Seconds, ElapsedTime.Milliseconds);
            }
            else
            {
                TimerBox.Text = "Invalidated";
            }
        }

        public void ResetPuzzleClick(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Any Saved Puzzles will be lost! Continue?", "Reset Confirmation", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information);

            if (Confirm == DialogResult.Yes)
            {
                GenPuzzle.Enabled = true;
                DiffOption.Enabled = true;
                PuzzleNum.Enabled = true;
                StartButton.Enabled = false;
                CheatCheck = false;

                if (DifficultySetting == "Easy")
                    Puzzles[index].ResetPuzzle(3);
                if (DifficultySetting == "Medium")
                    Puzzles[index].ResetPuzzle(5);
                if (DifficultySetting == "Hard")
                    Puzzles[index].ResetPuzzle(7);
                stopwatch.Reset();
                GeneratePuzzle(sender, e);
            }       
        }

        public void SavePuzzleClick(object sender, EventArgs e)
        {
            int range = 0;

            if (SaveButton.Text == "Load Puzzle")
            {
                switch(DifficultySetting)
                {
                    case "Easy":
                        range = 5;
                        break;
                    case "Medium":
                        range = 7;
                        break;
                    case "Hard":
                        range = 9;
                        break;
                }
                Puzzles[index].LoadState(range);

                GeneratePuzzle(sender, e);
            }

            if (SaveButton.Text == "Save Puzzle")
            {
                switch (DifficultySetting)
                {
                    case "Easy":
                        range = 5;
                        break;
                    case "Medium":
                        range = 7;
                        break;
                    case "Hard":
                        range = 9;
                        break;
                }
                Puzzles[index].SaveState(range);

                GeneratePuzzle(sender, e);
                SaveButton.Text = "Load Puzzle";

            }
        }

    }
}

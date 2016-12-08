using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Windows.Controls;

namespace MinesweeperVisualGit
{
    /*Delegate calls back to view to update based on cell actions*/
    interface GameDelegate
    {
        void gameEnded(bool won);
        void gameStarted();
    }

    enum FlipReturnState
    {
        Lost,
        Normal,
        Failed, 
        Won
    }
    internal class GameController: CellDelegate
    {
        public Cell[] cells;
        public int width, height, mines, flipped, flagged;
        public Difficulty difficulty;
        private TextBlock minesLeftLabel;
        private GameDelegate gDelegate;
        public bool gameInPlay;

        public GameController(Difficulty difficulty)
        {
            this.difficulty = difficulty;
            int m = numberOfMinesForDifficulty(difficulty);
            int w = widthForDifficulty(difficulty);
            int h = heightForDifficulty(difficulty);
            setupGame(m, w, h);
        }

        public void setDelegate(GameDelegate gDelegate)
        {
            this.gDelegate = gDelegate;
        }

        public void setMinesLeftLabel(TextBlock label)
        {
            minesLeftLabel = label;
            updateFlagCount();
        }

        private int numberOfMinesForDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Hard:
                    return 80;
                case Difficulty.Medium:
                    return 30;
                case Difficulty.Easy:
                    return 10;
                default:
                    return 1;
            }
        }

        private int widthForDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Hard:
                    return 30;
                case Difficulty.Medium:
                    return 15;
                case Difficulty.Easy:
                    return 10;
                default:
                    return 1;
            }
        }

        private int heightForDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Hard:
                    return 15;
                case Difficulty.Medium:
                    return 15;
                case Difficulty.Easy:
                    return 10;
                default:
                    return 1;
            }
        }

        public void setupGame(int nMines, int w, int h)
        {
            Random rand = new Random();
            gameInPlay = false;
            flipped = 0;
            flagged = 0;
            width = w;
            height = h;
            int numCells = w*h;
            mines = nMines;
            cells = new Cell[numCells];
            HashSet<int> mineLocations = new HashSet<int>();
            while(mineLocations.Count < mines)
            {
                int randomInt = rand.Next(0, numCells);
                mineLocations.Add(randomInt);
            }
            for(int index = 0; index < cells.Length; index++)
            {
                cells[index] = new Cell(index);
                cells[index].setDelegate(this);
                cells[index].nearbyMines = 0;
                if (mineLocations.Contains(index))
                {
                    cells[index].isMine = true;
                }
            }
            /* Calculates the neighbors for each cell*/
            for (int index = 0; index < cells.Length; index++)
            {
                Cell cell = cells[index];
                Cell[] potenN = new Cell[8];
                bool hasBelow = index < ((height - 1) * width);
                bool hasAbove = index >= width;
                int currentIndex = 0;
                int mineCount = 0;
                if(index % width < width-1)
                {
                    if (hasAbove)
                    {
                        Cell oCell1 = cells[index - width + 1];
                        if (oCell1.isMine) mineCount++;
                        potenN[currentIndex] = oCell1;
                        currentIndex++;
                    }
                    if (hasBelow)
                    {
                        Cell oCell1 = cells[index + width + 1];
                        if (oCell1.isMine) mineCount++;
                        potenN[currentIndex] = oCell1;
                        currentIndex++;
                    }
                    Cell oCell = cells[index + 1];
                    if (oCell.isMine) mineCount++;
                    potenN[currentIndex] = oCell;
                    currentIndex++;
                }
                if(index % width != 0)
                {
                    if (hasAbove)
                    {
                        Cell oCell1 = cells[index - width - 1];
                        if (oCell1.isMine) mineCount++;
                        potenN[currentIndex] = oCell1;
                        currentIndex++;
                    }
                    if (hasBelow)
                    {
                        Cell oCell1 = cells[index + width - 1];
                        if (oCell1.isMine) mineCount++;
                        potenN[currentIndex] = oCell1;
                        currentIndex++;
                    }
                    Cell oCell = cells[index - 1];
                    if (oCell.isMine) mineCount++;
                    potenN[currentIndex] = oCell;
                    currentIndex++;
                }
                if (hasAbove) {
                    Cell oCell = cells[index - width];
                    if (oCell.isMine) mineCount++;
                    potenN[currentIndex] = oCell;
                    currentIndex++;
                }
                if (hasBelow) {
                    Cell oCell = cells[index + width];
                    if (oCell.isMine) mineCount++;
                    potenN[currentIndex] = oCell;
                    currentIndex++;
                }
                cell.neighbors = new Cell[currentIndex];
                for(int i = 0; i < currentIndex; i++)
                {
                    cell.neighbors[i] = potenN[i];
                }
                cell.nearbyMines = mineCount;
            }

        }

        public void setCellButton(int i, int j, Button button, Image image)
        {
            cells[getIndex(i, j)].setButton(button, image);
        }

        public void printBoard()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = getIndex(x, y);
                    if (cells[index].flipped)
                    {
                        if (cells[index].isMine)
                        {
                            System.Console.Write(" X ");
                        }
                        else
                        {
                            System.Console.Write(" {0} ", cells[index].nearbyMines);
                        }
                    }
                    else
                    {
                        System.Console.Write(" * ");
                    }
                }
                System.Console.WriteLine();
            }
        }

        public int calculate3BVOfBoard()
        {
            int clicks = 0;
            int nextOpenIndex = getNextOpenIndex();
            while(nextOpenIndex >= 0)
            {
                clicks++;
                addAllConnectedOpenCells(cells[nextOpenIndex]);
                nextOpenIndex = getNextOpenIndex();
            }
            for (int i = 0; i < width*height; i++)
            {
                if (!cells[i].isMine && !cells[i].flipped)
                    clicks++;
            }
            return clicks;
        }

        private void addAllConnectedOpenCells(Cell startCell)
        {
            if (startCell.setFlipped()) flipped++;
            foreach (Cell neighbor in startCell.neighbors)
            {
                if (!neighbor.isMine && !neighbor.flipped)
                {
                    if (neighbor.setFlipped()) flipped++;
                    if (neighbor.nearbyMines == 0)
                    {
                        addAllConnectedOpenCells(neighbor);
                    }
                }
            }
        }

        
        private int getNextOpenIndex()
        {
            int nextOpen = -1;
            for (int i = 0; i < width * height; i++)
            {
                if (cells[i].nearbyMines == 0 && !cells[i].flipped && !cells[i].isMine)
                {
                    nextOpen = i;
                    break;
                }
            }
            return nextOpen;
        }

        public int getIndex(int x, int y)
        {
            return y * width + x;
        }

        private void updateFlagCount()
        {
            if (minesLeftLabel != null)
            {
                int minesLeft = mines - flagged;
                minesLeftLabel.Text = minesLeft.ToString();
            }
        }

        private void checkIfWon()
        {
            Console.WriteLine("Cells:{0} Flipped:{1} Mines:{2}", cells.Length, flipped, mines);
            if (cells.Length - flipped == mines)
            {
                gameInPlay = false;
                gDelegate.gameEnded(true);
                Console.WriteLine("Player Won");
            }
        }

        public void leftClicked(int index)
        {
            if (!gameInPlay)
            {
                gDelegate.gameStarted();
                gameInPlay = true;
            }
            flipped++;
            if(cells[index].nearbyMines == 0)
            {
                addAllConnectedOpenCells(cells[index]);
            }
            checkIfWon();
        }

        public void rightClicked(int index, bool wasFlagged)
        {
            if (!gameInPlay)
            {
                gDelegate.gameStarted();
                gameInPlay = true;
            }
            if (wasFlagged)
            {
                flagged += 1;
                cells[index].setFlagged(true);
            }else if(!wasFlagged)
            {
                flagged -= 1;
                cells[index].setFlagged(false);
            }
            updateFlagCount();
        }
        public void onLose(int index)
        {
            gameInPlay = false;
            gDelegate.gameEnded(false);
            Console.WriteLine("Player Lost");
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace MinesweeperVisualGit
{
    /*Delegate calls back to Game Controller*/
    interface CellDelegate
    {
        void leftClicked(int index);
        void rightClicked(int index, bool flagged);
        void onLose(int index);
    }

    internal class Cell
    {
        public bool flipped;
        public bool isMine;
        public bool flagged;
        public int nearbyMines;
        public int index;
        public Cell[] neighbors;
        public Button button;
        private Image bImage;
        private CellDelegate mDelegate;

        public Cell(int index)
        {
            this.index = index;
            flipped = false;
            isMine = false;
            flagged = false;
            nearbyMines = 0;
        }

        public void setButton(Button button, Image image)
        {
            this.button = button;
            this.button.PreviewMouseLeftButtonUp += onClickLeft;
            this.button.PreviewMouseRightButtonUp += onClickRight;
            this.bImage = image;
        }

        public void setDelegate(CellDelegate mDelegate)
        {
            this.mDelegate = mDelegate;
        }

        public bool setFlipped()
        {
            if (flipped)
                return false;
            button.PreviewMouseRightButtonUp -= onClickRight;
            button.PreviewMouseLeftButtonUp -= onClickLeft;
            this.flipped = true;
            button.Foreground = getColorForValue(this.nearbyMines);
            if (nearbyMines > 0)
            {
                button.Content = this.nearbyMines.ToString();
            }else
            {
                button.Content = "";
            }
            return true;
        }

        public void setFlagged(bool flagged)
        {
            this.flagged = flagged;
            if (flagged)
            {
                bImage.Source = new BitmapImage(new Uri(@"images/cell_flagged.png", UriKind.Relative));
            }
            else
            {
                bImage.Source = new BitmapImage(new Uri(@"images/cellnormal.png", UriKind.Relative));
            }
        }

        private void onClickRight(object sender, MouseEventArgs e)
        {
            mDelegate.rightClicked(index, !flagged);
        }

        private SolidColorBrush getColorForValue(int value)
        {
            Color newColor;
            switch (value)
            {
                case 1:
                    newColor = Colors.Blue;
                    break;
                case 2:
                    newColor = Colors.Green;
                    break;
                case 3:
                    newColor = Colors.Red;
                    break;
                case 4:
                    newColor = Colors.Purple;
                    break;
                case 5:
                    newColor = Colors.BlueViolet;
                    break;
                case 6:
                    newColor = Colors.Olive;
                    break;
                case 7:
                    newColor = Colors.MintCream;
                    break;
                case 8:
                    newColor = Colors.OrangeRed;
                    break;
                default:
                    newColor = Colors.Black;
                    break;
            }
            return new SolidColorBrush(newColor);
        }

        private void onClickLeft(object sender, MouseEventArgs e)
        {
            if (!flagged)
            {
                button.PreviewMouseRightButtonUp -= onClickRight;
                button.PreviewMouseLeftButtonUp -= onClickLeft;
                if (isMine)
                {
                    bImage.Source = new BitmapImage(new Uri(@"images/mine.png", UriKind.Relative));
                    mDelegate.onLose(index);
                }
                else
                {
                    flipped = true;
                    button.Foreground = getColorForValue(this.nearbyMines);
                    if (nearbyMines > 0)
                    {
                        button.Content = this.nearbyMines.ToString();
                    }
                    else
                    {
                        button.Content = "";
                    }
                    mDelegate.leftClicked(index);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Lottery_App.Services
{
    internal class MegaItem
    {
        public int Multiplier { set; get; }
        public int BallOne { set; get; }
        public int BallTwo { set; get; }
        public int BallThree { set; get; }
        public int BallFour { set; get; }
        public int BallFive { set; get; }
        public int BallSix { set; get; }
        public int MegaBall { set; get; }
        public DateTime DrawDate { set; get; }

        private List<MegaItem> megaList;

        public MegaItem()
        {
        }

        public MegaItem(DateTime drawDate, string winningNumbers, int multiplier, int megaBall)
        {
            string[] winningArr = winningNumbers.Split(' ');
            Multiplier = multiplier;
            DrawDate = drawDate;
            BallOne = Int32.Parse(winningArr[0]);
            BallTwo = Int32.Parse(winningArr[1]);
            BallThree = Int32.Parse(winningArr[2]);
            BallFour = Int32.Parse(winningArr[3]);
            BallFive = Int32.Parse(winningArr[4]);
            BallSix = Int32.Parse(winningArr[5]);
            MegaBall = megaBall;
        }

        public List<MegaItem> GetPowerItems()
        {
            megaList = new List<MegaItem>()
            {
            };
            return megaList;
        }
    }
}
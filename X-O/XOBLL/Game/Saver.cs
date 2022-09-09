using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_O.XOBLL.Enums;
using X_O.XOBLL.Interfaces;

namespace X_O.XOBLL.Game
{
    public class Saver : ISaveAndLoadFile
    {
        private FileInfo _file = new FileInfo("result.txt");
        public void Save(GameResult result, GameMode mode)
        {
            

            using (StreamWriter sw = _file.AppendText())
            {
                switch (result)
                {
                    case GameResult.Win: sw.WriteLine("1;0;0;" + mode); break;
                    case GameResult.Lose: sw.WriteLine("0;1;0;" + mode); break;
                    case GameResult.Draw: sw.WriteLine("0;0;1;" + mode); break;
                }
            }
        }

        public int[] Load(GameMode mode)
        {
            int winEasy = 0, loseEasy = 0, drawEasy = 0;
            int winHard = 0, loseHard = 0, drawHard = 0;
            
            try
            {
                using (StreamReader sr = _file.OpenText())
                {
                    while (!sr.EndOfStream)
                    {
                        string[] temp = sr.ReadLine().Split(';');
                        if (temp[3] == "Easy")
                        {
                            winEasy += int.Parse(temp[0]);
                            loseEasy += int.Parse(temp[1]);
                            drawEasy += int.Parse(temp[2]);
                        }
                        else
                        {
                            winHard += int.Parse(temp[0]);
                            loseHard += int.Parse(temp[1]);
                            drawHard += int.Parse(temp[2]);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return new int[] { 0, 0, 0 };
            }
            if (mode == GameMode.Easy)
                return new int[] { winEasy, loseEasy, drawEasy };
            else if (mode == GameMode.Hard)
                return new int[] { winHard, loseHard, drawHard };
            else
                return new int[] { winEasy + winHard, loseEasy + loseHard, drawEasy + drawHard };

        }

        public void Clear()
        {
            _file.Delete();
        }


    }
}

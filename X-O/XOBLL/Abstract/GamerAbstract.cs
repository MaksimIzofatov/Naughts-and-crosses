using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_O.XOBLL.Game;

namespace X_O.XOBLL.Abstract
{
    public abstract class GamerAbstract
    {
        private bool _newGame;
        protected static char[] _symbols = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public event Action<char, int[]>? Win;
        protected int[][] _winCombination = new int[][]
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 },
        };
        public int Queue { get; private set; }

       

        protected GamerAbstract(int queue)
        {
            Queue = queue;
        }

        public bool NewGameFlag => _newGame;

        public char ClickButton(int index)
        {
            if (Queue == 1)
            {
                _symbols[index] = 'x';
                return 'x';
            }
            else
            {
                _symbols[index] = '0';
                return '0';
            }
        }


        public void Winner()
        {
            _newGame = false;
            int count = 0;
            int arrayCount = 0;
            int arrayIndex = 0;

            for (int i = 0; i < _winCombination.Length; i++)
            {
                if (_symbols[_winCombination[i][0]] == _symbols[_winCombination[i][1]] &&
                    _symbols[_winCombination[i][1]] == _symbols[_winCombination[i][2]])
                {

                    arrayCount++;
                    arrayIndex = i;

                }
            }
            if (arrayCount == 1)
            {
                Win?.Invoke(_symbols[_winCombination[arrayIndex][0]], _winCombination[arrayIndex]);
            }
            else
            {
                for (int i = 0; i < _winCombination.Length; i++)
                {
                    if (_symbols[_winCombination[i][0]] == _symbols[_winCombination[i][1]] &&
                        _symbols[_winCombination[i][1]] == _symbols[_winCombination[i][2]] &&
                        _symbols[_winCombination[i][0]] == 'x')
                    {


                        Win?.Invoke(_symbols[_winCombination[i][0]], _winCombination[i]);

                    }
                }
            }

            for (int i = 0; i < _symbols.Length; i++)
            {
                if (_symbols[i] == 'x' || _symbols[i] == '0')
                    count++;
            }
            if (count == 9)
                Win?.Invoke('n', new int[1]);

        }

        public void NewGame()
        {
            int c = 49;
            for (int i = 0; i < _symbols.Length; i++)
            {
                _symbols[i] = Convert.ToChar(c++);
            }
            _newGame = true;
        }
    }
}

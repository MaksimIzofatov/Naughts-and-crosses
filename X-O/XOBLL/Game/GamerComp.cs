using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using X_O.XOBLL.Abstract;
using X_O.XOBLL.Enums;

namespace X_O.XOBLL.Game
{
    public class GamerComp : GamerAbstract
    {
        private int[] _stepsWin = new int[5] { 0, 2, 4, 6, 8 };
        private int[] _steps = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        private int[] _stepsDefense = { 1, 3, 5, 7 };
        private GameMode _mode;
        private int nextStep = 0;
        private Random _r = new Random();
        public GamerComp(int queue, GameMode mode) : base(queue)
        {
            _mode = mode;
        }

        public (char?, int) StepComp()
        {
            if (_mode == GameMode.Easy)
                return ClickEasy();
            else
                return ClickHard();
        }

        private (char?, int) ClickHard()
        {
            int index = _r.Next(0, _stepsWin.Length);
            if (Queue == 1)
            {

                if (nextStep == 0 || nextStep == 1)
                {
                    nextStep++;
                    return Variable(index, _stepsWin);
                }
                else
                {
                    nextStep++;
                    var win = CheckWinStep('x');

                    

                    if (win != null)
                    {
                        var res = Variable(0, win);
                        if (res.Item1 != null)
                            return res;
                    }

                    var result = StepsDefense();
                    if (result.Item1 != null)
                        return result;
                    index = _r.Next(0, _stepsWin.Length);
                    var res1 = Variable(index, _stepsWin);
                    if (res1.Item1 != null)
                        return res1;
                    else
                        return Variable(index, _steps);


                    
                }


            }
            else
            {
                if (nextStep == 0)
                {
                    nextStep++;
                    int stepGamerOne = 0;
                    int count = 0;
                    for (int i = 0; i < _symbols.Length; i++)
                    {
                        if (_symbols[i] == 'x')
                        {
                            stepGamerOne = i;
                            break;
                        }
                    }
                    for (int i = 0; i < _stepsWin.Length; i++)
                    {
                        if (_stepsWin[i] == stepGamerOne)
                            break;
                        else
                            count++;
                    }

                    if (count == 5)
                    {
                        bool isWork = true;
                        while (isWork)
                        {
                            index = _r.Next(0, _stepsWin.Length);
                            if (index != 2)
                                isWork = false;
                        }
                        
                        return Variable(index, _stepsWin);  
                        
                    }
                    else
                    {
                        if (_symbols[4] != 'x')
                            return (ClickButton(4), 4);
                        return Variable(index, _stepsWin);
                    }
                }
                else if (nextStep == 1)
                {
                    nextStep++;
                    if ((_symbols[0] == 'x' && _symbols[8] == 'x') || (_symbols[2] == 'x' && _symbols[6] == 'x'))
                    {
                        index = _r.Next(0, _stepsDefense.Length);
                        return Variable(index, _stepsDefense);
                    }

                    var res = StepsDefense();
                    if (res.Item1 != null)
                        return res;
                    else
                    {
                        if (_symbols[4] != 'x' && _symbols[4] != '0')
                            return (ClickButton(4), 4);
                        return Variable(index, _stepsWin);
                    }

                }
                else
                {
                    nextStep++;

                    int[]? temp = CheckWinStep('0');
                    if (temp != null)
                    {
                        var res = Variable(0, temp);
                        if (res.Item1 != null)
                            return res;
                    }

                    var result = StepsDefense();
                    if (result.Item1 != null)
                        return result;
                    index = _r.Next(0, _stepsWin.Length);
                    var res1 = Variable(index, _stepsWin);
                    if (res1.Item1 != null)
                        return res1;
                    else
                        Variable(index, _steps);

                }
            }
            return (_symbols[0], 0);
        }

        private int[]? CheckWinStep(char c)
        {
            char antiC;
            if (c == 'x')
                antiC = '0';
            else
                antiC = 'x';
            for (int i = 0; i < _winCombination.Length; i++)
            {
                if (_symbols[_winCombination[i][0]] == _symbols[_winCombination[i][1]]
                    || _symbols[_winCombination[i][1]] == _symbols[_winCombination[i][2]]
                    || _symbols[_winCombination[i][0]] == _symbols[_winCombination[i][2]])
                {
                    if (_symbols[_winCombination[i][0]] == c || _symbols[_winCombination[i][1]] == c)
                    {
                        for (int j = 0; j < _winCombination[i].Length; j++)
                        {
                            if (_winCombination[i][j] != antiC)
                                return _winCombination[i];
                        }
                    }
                }
            }



            return null;
        }

        private (char?, int) StepsDefense()
        {
            for (int i = 0; i < _winCombination.Length; i++)
            {


                if (_symbols[_winCombination[i][0]] == _symbols[_winCombination[i][1]]
                    || _symbols[_winCombination[i][1]] == _symbols[_winCombination[i][2]]
                    || _symbols[_winCombination[i][0]] == _symbols[_winCombination[i][2]])
                {
                    var result = Variable(0, _winCombination[i]);

                    if (result.Item1 == null)
                    {
                        continue;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            return (null, 1);
        }

        private (char?, int) ClickEasy()
        {
            int index = _r.Next(0, 9);
            return Variable(index, _steps);
        }

        private (char?, int) Variable(int index, params int[] cells)
        {
            bool variable = true;
            int count = 0;
            while (variable)
            {
                if (_symbols[cells[index]] != 'x' && _symbols[cells[index]] != '0')
                {
                    return (ClickButton(cells[index]), cells[index]);
                }
                else
                    index = _r.Next(0, cells.Length);
                count++;
                if (count == 50)
                    variable = false;
            }
            return (null, 1);
        }
    }
}

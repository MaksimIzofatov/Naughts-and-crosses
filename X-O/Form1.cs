using X_O.XOBLL.Abstract;
using X_O.XOBLL.Enums;
using X_O.XOBLL.Game;
using X_O.XOBLL.Interfaces;

namespace X_O
{
    public partial class Form1 : Form
    {
        private Random _r = new Random();
        private GamerAbstract _gamer, _gamerComp;
        private Button[] _buttons;
        private ISaveAndLoadFile _saver;
        private GameMode _gameMode = GameMode.Hard;
        private Load _load = new Load();
        public Form1()
        {
            
            InitializeComponent();

            _buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (var item in _buttons)
            {
                item.Text = "";
            }
            _saver = new Saver();
            NewGame();
        }

        private void OnWinner(char obj, int[] combination)
        {
            if (obj == 'n')
            {
                MessageBox.Show("�����!");
                _saver.Save(GameResult.Draw, _gameMode);
                
            }
            else
            {
                PaintButton(combination);
                if ((obj == 'x' && _gamer.Queue == 1) || (obj == '0' && _gamer.Queue == 2))
                {
                    MessageBox.Show("�� ��������!");
                    _saver.Save(GameResult.Win, _gameMode);
                }
                else
                {
                    MessageBox.Show("�� ���������!");
                    _saver.Save(GameResult.Lose, _gameMode);
                }
                
            }

            NewGame();
        }

        private void StepComp()
        {
            var result = (_gamerComp as GamerComp).StepComp();
            
            _buttons[result.Item2].Text = result.Item1.ToString();
        }

        private void NewGame()
        {
            Clear();
            int queue = _r.Next(1, 3);
            _gamer = new Gamer(queue);
            _gamerComp = new GamerComp(3 - queue, _gameMode);
            _gamer.NewGame();
            _gamer.Win += OnWinner;
            if (_gamer.Queue == 1)
                label1.Text = "�� ������ ������! ��� 'x' ";
            else
            {
                label1.Text = "��������� ����� ������! � ������ 'x' ";
                StepComp();
            }
        }

        private void Clear()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Text = "";
                _buttons[i].BackColor = Color.White;
            }
        }

        private void PaintButton(int[] array)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == array[j])
                        _buttons[i].BackColor = Color.Green;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "")
            {
                button1.Text = _gamer.ClickButton(0).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }

            }
                


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "")
            {
                button2.Text = _gamer.ClickButton(1).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }

            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "")
            {
                button3.Text = _gamer.ClickButton(2).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "")
            {
                button4.Text = _gamer.ClickButton(3).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "")
            {
                button5.Text = _gamer.ClickButton(4).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "")
            {
                button6.Text = _gamer.ClickButton(5).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }


            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "")
            {
                button7.Text = _gamer.ClickButton(6).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "")
            {
                button8.Text = _gamer.ClickButton(7).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }
                
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "")
            {
                button9.Text = _gamer.ClickButton(8).ToString();
                _gamer.Winner();
                if (!_gamer.NewGameFlag)
                {

                    StepComp();
                    _gamer.Winner();
                }

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void EasyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _gameMode = GameMode.Easy;
            NewGame();
        }

        private void GeneralStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = _saver.Load(GameMode.General);
            ShowResult(result, GameMode.General);
            
        }

        private void EasyStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = _saver.Load(GameMode.Easy);
            ShowResult(result, GameMode.Easy);
        }

        private void HardStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = _saver.Load(GameMode.Hard);
            ShowResult(result, GameMode.Hard);
        }

        private void HardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _gameMode = GameMode.Hard;
            NewGame();
        }

        private void ShowResult(int[] result, GameMode mode)
        {
            string res = $"�������: {result[0]}; �����: {result[2]}; ��������: {result[1]}";
            _load.ShowResult(res, mode);
            if (_load.ShowDialog() == DialogResult.Abort)
                (_saver as Saver).Clear();
        }

        
    }
}
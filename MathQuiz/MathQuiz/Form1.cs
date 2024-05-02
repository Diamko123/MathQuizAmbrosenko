using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Создаёт случайный объект под названием randomizer
        // для генерации случайных чисел
        Random randomizer = new Random();
        // Эти целочисленные переменные хранят числа
        // для задачи сложения.
        int addend1;
        int addend2;

        /// <summary>
        /// Начните тест с заполнения всех заданий
        /// и запускаю таймер.
        /// </summary>

        // 
        // для задачи вычитания. 
        int minuend;
        int subtrahend;

        // Эти целочисленные переменные хранят числа  
        // для задачи умножения. 
        int multiplicand;
        int multiplier;

        // Эти целочисленные переменные хранят числа  
        // для задачи деления.
        int dividend;
        int divisor;

        // Эта целочисленная переменная отслеживает
        // оставшееся время.
        int timeLeft;

        /// <summary>
        /// Проверьте ответы, чтобы убедиться, что пользователь все понял правильно.
        /// </summary>
        /// <returns> Истинно, если ответ правильный, ложно в противном случае. </returns>
        
        
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        public void StartTheQuiz()
        {
            // Заполните задачу на сложение.
            // Сгенерируйте два случайных числа для сложения.
            // Сохраните значения в переменных 'addend1' и 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Преобразуйте два случайно сгенерированных числа
            // в строки, чтобы их можно было отображать
            // в элементах управления label.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' - это название элемента управления NumericUpDown.
            // На этом шаге проверяется, что его значение равно нулю, прежде чем
            // добавлять к нему какие-либо значения.
            sum.Value = 0;

            // Заполните задачу на вычитание.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Заполните задачу на умножение.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Заполните задачу на деление.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Запуск таймера.
            timeLeft = 30;
            timeLabel.Text = "30 секунд";
            timer1.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft <6)
            {
                // Если на таймере осталось 5 секунд
                // цвет элемента управления timeLabel становится красным.
                timeLabel.BackColor = Color.Red;
            }
            if (CheckTheAnswer())
            {
                // Если функция Check The Answer() возвращает значение true, то пользователь
                //вы ответили правильно. Остановите таймер 
                // и покажите окно с сообщениями.
                // Изменяет цвет таймера на белый (если игрок ответил при значении таймера меньше 5 секунд)
                timer1.Stop();
                MessageBox.Show("У тебя правильные ответы на все вопросы!",
                                "Поздравляю!");
                startButton.Enabled = true;
                timeLabel.BackColor = Color.White;
            }
            else if (timeLeft > 0)
            {
                // Если функция Check Answer() возвращает значение false, продолжайте подсчет
                // дальше. Уменьшите оставшееся время на одну секунду и
                // отобразите новое оставшееся время, обновив
                // окно оставшегося времени.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " секунд";
            }
            else
            {
                // Если у пользователя закончилось время, остановите таймер, покажите
                // откройте окно для сообщений и заполните ответы.
                timer1.Stop();
                timeLabel.Text = "Время вышло!";
                MessageBox.Show("Ты не успел закончить вовремя.", "Прости!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = Color.White;

            }




        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Выберите полный ответ в элементе управления NumericUpDown.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}

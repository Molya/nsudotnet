using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game
{
    public class GameManager
    {
        private static int? currentRow;
        private static int? currentColumn;

        private static int counter;

        private static GameObject[][] gameField = new GameObject[9][];
        public static GameObject[][] GameField
        {
            get { return gameField; }
        }

        private static GameObject[][] commonGameField = new GameObject[3][];
        public static GameObject[][] CommonGameField
        {
            get { return commonGameField; }
        }

        static GameManager()
        {
            for (int i = 0; i < 9; i++)
            {
                gameField[i] = new GameObject[9];
                for (int j = 0; j < 9; j++)
                {
                    gameField[i][j] = new GameObject();
                }
            }

            for (int i = 0; i < 3; i++)
            {
                commonGameField[i] = new GameObject[3];
                for (int j = 0; j < 3; j++)
                {
                    commonGameField[i][j] = new GameObject();
                }
            }
        }

        public static bool SetValueAndCheckWinner(GameObject gameObject)
        {
            int row;
            int column;
            FindObject(gameObject, out row, out column);

            //Проверка первого хода и пустого места в поле
            if (currentRow.HasValue && currentColumn.HasValue && CheckEmptyField())
                //Проверка, что походили в нужное поле
                if (!(row >= currentRow.Value * 3 && row <= currentRow.Value * 3 + 2
                    && column >= currentColumn.Value * 3 && column <= currentColumn.Value * 3 + 2))
                {
                    throw new Exception("Сходи в нужное поле!");
                }

            //Проверка на занятость поля
            if (gameField[row][column].IsBusy)
                throw new Exception("Данное поле уже занято");

            gameField[row][column].Value = GetCurrentChar();

            //Проверка победителя в локальном поле
            char? charOfLocalWinner = CheckWinner(true);
            if (charOfLocalWinner.HasValue && !commonGameField[currentRow.Value][currentColumn.Value].IsBusy)
            {
                commonGameField[currentRow.Value][currentColumn.Value].Value = charOfLocalWinner.Value;

                if (CheckWinner(false).HasValue)
                    return true;
            }

            counter++;

            currentRow = row % 3;
            currentColumn = column % 3;

            return false;
        }

        private static void FindObject(GameObject gameObject, out int row, out int column)
        {
            row = -1;
            column = -1;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (gameObject == gameField[i][j])
                    {
                        row = i;
                        column = j;
                        return;
                    }       
                }
            }
        }

        public static char GetCurrentChar()
        {
            return counter % 2 == 0 ? 'X' : 'O';
        }

        private static bool CheckEmptyField()
        {
            for (int i = currentRow.Value * 3; i < currentRow.Value * 3 + 2; i++)
            {
                for (int j = currentColumn.Value * 3; j < currentColumn.Value * 3 + 2; j++)
                {
                    if (!gameField[i][j].IsBusy)
                        return true;
                } 
            }

            return false;
        }

        private static char? CheckWinner(bool isLocal)
        {
            if (!currentRow.HasValue || !currentColumn.HasValue)
                return null;


            int row = 0;
            int column = 0;
            var field = commonGameField;

            if (isLocal)
            {
                row = currentRow.Value * 3;
                column = currentColumn.Value * 3;
                field = gameField;
            }

            //проверка по строкам
            for (int i = row; i < row + 3; i++)
            {
                if (field[i][column].Value != ' ' &&
                    field[i][column].Value == field[i][column + 1].Value &&
                    field[i][column].Value == field[i][column + 2].Value)
                    return gameField[i][column].Value;
            }

            //проверка по столбцам
            for (int i = column; i < column + 3; i++)
            {
                if (field[row][i].Value != ' ' &&
                    field[row][i].Value == field[row + 1][i].Value &&
                    field[row][i].Value == field[row + 2][i].Value)
                    return field[row][i].Value;
            }

            //проверка главной диагонали
            if (field[row][column].Value != ' ' &&
                field[row][column].Value == field[row + 1][column + 1].Value &&
                field[row][column].Value == field[row + 2][column + 2].Value)
                return field[row][column].Value;

            //проверка побочной диагонали
            if (field[row][column + 2].Value != ' ' &&
                field[row][column + 2].Value == field[row + 1][column + 1].Value &&
                field[row][column + 2].Value == field[row + 2][column].Value)
                return field[row][column + 2].Value;

            return null;
        }
    }
}

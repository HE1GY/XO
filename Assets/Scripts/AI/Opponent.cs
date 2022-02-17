using System;
using GameLogic;


namespace AI
{
    public class Opponent 
    {
        private const int MatrixColums = 3;
        private Mark _aiMark;

        public Mark AIMark => _aiMark;

        public Opponent(Mark aiMark)
        {
            _aiMark = aiMark;
        }

        public int MakeAMove(Mark[] marksLayout )
        {
            Mark[,] marksMatrix = ArrayToMatrix(marksLayout);
            return  ChooseCell(marksMatrix);
        }

        private int ChooseCell(Mark[,]matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j]==Mark.None)
                    {
                        int score=Int32.MinValue;
                        GetAiScore(matrix,i,j,ref score);
                    }
                }
            }
        }

        private int  GetAiScore(Mark[,]matrix,int i ,int j,ref int score)
        {
            Mark[,] scoreMatrix = matrix;
            scoreMatrix[i, j] = AIMark;
            if (AiWin(scoreMatrix))
            {
                score += 10;
                return score;
            }
            else if(HumanWin(scoreMatrix))
            {
                score -= 10;
                return score;
            }
            else if(CheckDraw(scoreMatrix))
            {
                score += 0;
                return score;
            }
            else
            {
                return GetAiScore(scoreMatrix,)
            }
            
        }

        private bool AiWin(Mark[,] markMatrix)
        {
            return CheckIfMarkMatch(markMatrix, AIMark);
        }

        private bool HumanWin(Mark[,] markMatrix)
        {
            Mark humanMark;
            if (AIMark == Mark.X)
            {
                humanMark = Mark.O;
            }
            else
            {
                humanMark = Mark.X;
            }
            return CheckIfMarkMatch(markMatrix, humanMark);
        }

        private bool CheckDraw(Mark[,] markMatrix)
        {
            for (int i = 0; i < markMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < markMatrix.GetLength(1); j++)
                {
                    if (markMatrix[i, j] == Mark.None)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        
        
        private bool CheckIfMarkMatch(Mark[,] markMatrix,Mark mark)
        {
            return markMatrix[0, 0] == markMatrix[0, 1] && markMatrix[0, 1] == markMatrix[0, 2] &&
                   markMatrix[0, 2] == mark ||
                   markMatrix[1, 0] == markMatrix[1, 1] && markMatrix[1, 1] == markMatrix[1, 2] &&
                   markMatrix[1, 2] == mark ||
                   markMatrix[2, 0] == markMatrix[2, 1] && markMatrix[2, 1] == markMatrix[2, 2] &&
                   markMatrix[2, 2] == mark ||

                   markMatrix[0, 0] == markMatrix[1, 0] && markMatrix[1, 0] == markMatrix[2, 0] &&
                   markMatrix[2, 0] == mark ||
                   markMatrix[0, 1] == markMatrix[1, 1] && markMatrix[1, 1] == markMatrix[2, 1] &&
                   markMatrix[2, 1] == mark ||
                   markMatrix[0, 2] == markMatrix[1, 2] && markMatrix[1, 2] == markMatrix[2, 2] &&
                   markMatrix[2, 2] == mark ||
                   
                   markMatrix[0, 0] == markMatrix[1, 1] && markMatrix[1, 1] == markMatrix[2, 2] &&
                   markMatrix[2, 2] == mark ||
                   markMatrix[2, 0] == markMatrix[1, 1] && markMatrix[1, 1] == markMatrix[0, 2] &&
                   markMatrix[0, 2] == mark;

        }
        
        


        private Mark[,] ArrayToMatrix(Mark[] marks)
        {
            Mark[,] matrix= new Mark[(int) Math.Sqrt(marks.Length), (int) Math.Sqrt(marks.Length)];
            int indexForArray = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = marks[indexForArray];
                    indexForArray++;
                }
            }
            return matrix;
        }
        
        
        private int IntoArrrayMetrics(int i,int j)
        {
            return i*MatrixColums + j;
        }
    }
}
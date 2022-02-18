using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameLogic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI
{
    public class AIBrain
    {
        
        public int GetAiMoveIndex(Mark[] marks)
        {
            return MinMax(marks,Mark.O,0);
        }

        private int MinMax(Mark[] marks,Mark playerMark,int layer)
        {
            if (CheckWin(marks,Mark.O))
            {
                return 10 + layer;
            }
            if (CheckWin(marks,Mark.X))
            {
                return -10 - layer;
            }

            if (CheckDraw(marks))
            {
                return 0;
            }

            if (playerMark == Mark.O)//Max
            {
                int bestScore=Int32.MinValue;
                int bestIndex = -1;
                List<int> emptyCellIndexs = GetEmptyCelIndex(marks);
                for (int i = 0; i < emptyCellIndexs.Count; i++)
                {
                    Mark[] copyMarks = marks.ToArray();
                    copyMarks[emptyCellIndexs[i]] = Mark.O;

                    int branchScore = MinMax(copyMarks, Mark.X, layer - 1);

                    if (branchScore > bestScore)
                    {
                        bestIndex = emptyCellIndexs[i];
                        bestScore = branchScore;
                    }
                }

                if (layer == 0)
                {
                    return bestIndex;
                }

                return bestScore;
            }
            else
            {
                int worseScore=Int32.MaxValue;
                List<int> emptyCellIndexs = GetEmptyCelIndex(marks);
                for (int i = 0; i < emptyCellIndexs.Count; i++)
                {
                    Mark[] copyMarks = marks.ToArray();
                    copyMarks[emptyCellIndexs[i]] = Mark.X;

                    int branchScore = MinMax(copyMarks, Mark.O, layer - 1);

                    if (branchScore < worseScore)
                    {
                        worseScore = branchScore;
                    }
                }
                return worseScore;
            }
        }

        private bool CheckWin(Mark[] marks,Mark playerMark)
        {
            return CheckMarksMatch(0, 1, 2, playerMark, marks) || CheckMarksMatch(3, 4, 5, playerMark, marks) || CheckMarksMatch(6, 7, 8, playerMark, marks) ||
                   CheckMarksMatch(0, 3, 6, playerMark, marks) || CheckMarksMatch(1, 4, 7, playerMark, marks) || CheckMarksMatch(2, 5, 8, playerMark, marks) ||
                   CheckMarksMatch(0, 4, 8, playerMark, marks) || CheckMarksMatch(2, 4, 6, playerMark, marks);

        }

        private bool CheckMarksMatch(int i ,int i1,int i2,Mark sameMark,Mark[] marksField)
        {
            return marksField[i] == marksField[i1] && marksField[i2] == marksField[i1] && marksField[i2] == sameMark;
        }

        private bool CheckDraw(Mark[] marks)
        {
            return marks.All(m => m != Mark.None);
        }

        private List<int> GetEmptyCelIndex(Mark[] marksField)
        {
            List<int> emptyCellIndex=new List<int>();
            for (int i = 0; i < marksField.Length; i++)
            {
                if (marksField[i] == Mark.None)
                {
                    emptyCellIndex.Add(i);
                }
            }

            return emptyCellIndex;
        } 
    }
}

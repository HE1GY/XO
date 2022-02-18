using System;
using System.Linq;
using AI;
using UnityEngine;
using Utilities.Events;


namespace GameLogic
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private PlaceHolder[] _xoGrid;

        private Mark[] _marks;
        
        private Mark _currentMark=Mark.X;
        private AIBrain _opponent;


        private void OnEnable()
        {
            foreach (var xo in _xoGrid)
            {
                xo.MarkChanged += OnUpdateGameField;
            }
            EventsControllerXo.AddListener(EventsTypeXo.SpawnItem,OnAiMove);
        }

        private void OnDisable()
        {
            foreach (var xo in _xoGrid)
            {
                xo.MarkChanged -= OnUpdateGameField;
            }
            EventsControllerXo.AddListener(EventsTypeXo.SpawnItem,OnAiMove);
        }

        private void Start()
        {
            _marks = new Mark[9];
            _opponent = new AIBrain();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnAiMove();
            }
        }


        private void OnUpdateGameField()
        {
            CopyPlaceHoldersMarksToMarksArray();
            if (CheckIfWin())
            {
                EventsControllerXo.Broadcast<Mark>(EventsTypeXo.Win, _currentMark);
            }

            if (CheckIfDraw())
            {
                EventsControllerXo.Broadcast<Mark>(EventsTypeXo.Draw, Mark.None);
            }
            
            ChangeCurrentMover();
        }

        private bool CheckIfDraw()
        {
            return _marks.All(mark => mark != Mark.None);
        }

        private void CopyPlaceHoldersMarksToMarksArray()
        {
            _marks = _xoGrid.Select(placeHolder => placeHolder.Mark).ToArray();
        }

        private  bool CheckIfWin()
        {
            return CheckIfItemMatch(0, 1, 2) || CheckIfItemMatch(3, 4, 5) || CheckIfItemMatch(6, 7, 8) ||
                   CheckIfItemMatch(0, 3, 6) || CheckIfItemMatch(1, 4, 7) || CheckIfItemMatch(2, 5, 8) ||
                   CheckIfItemMatch(0, 4, 8) || CheckIfItemMatch(2, 4, 6);
        }

        private bool CheckIfItemMatch(int i, int i1, int i2)
        {
            return _marks[i] == _marks[i1] && _marks[i1] == _marks[i2] && _marks[i] != Mark.None;
        }

        private void ChangeCurrentMover()
        {
            if (_currentMark == Mark.None)
            {
                _currentMark = Mark.X;
            }
            else if (_currentMark == Mark.X)
            {
                _currentMark = Mark.O;
            }
            else if(_currentMark == Mark.O)
            {
                _currentMark = Mark.X;
            }
        }

        private void OnAiMove()
        {
            int aiChoice = _opponent.GetAiMoveIndex(_marks);
            Debug.Log(aiChoice);
            _xoGrid[aiChoice].SetMark(Mark.O);
            OnUpdateGameField();
        }
        
    }
}
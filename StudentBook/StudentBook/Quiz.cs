using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLibrary.Models;

namespace StudentBook
{
    class Quiz
    {
        private int _currentPosition;

        public List<QuestionsToView> Questions;
        public List<QuestionsToView> CorrectQuestions;
        public List<QuestionsToView> UncorrectQuestions;
        public int CurrentPosition 
        {
            get => _currentPosition;
            set
            {
                if (value >= Questions.Count)
                    _currentPosition = -1;
                else
                    _currentPosition = value;
            }
        }
        public Quiz()
        {
            Questions = new List<QuestionsToView>();
            CorrectQuestions = new List<QuestionsToView>();
            UncorrectQuestions = new List<QuestionsToView>();
            _currentPosition = 0;
        }
        public Quiz(int count)
        {
            Questions = new List<QuestionsToView>(count);
            CorrectQuestions = new List<QuestionsToView>();
            UncorrectQuestions = new List<QuestionsToView>();
            _currentPosition = 0;
        }
        public Quiz(List<QuestionsToView> questions)
        {
            Questions = new List<QuestionsToView>(questions);
            CorrectQuestions = new List<QuestionsToView>();
            UncorrectQuestions = new List<QuestionsToView>();
            _currentPosition = 0;
        }
        public void Answer(bool isCorrect)
        {
            if (isCorrect)
                CorrectQuestions.Add(Questions[_currentPosition]);
            else
                UncorrectQuestions.Add(Questions[_currentPosition]);
            CurrentPosition = _currentPosition + 1;
        }
    }
}

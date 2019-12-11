using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLibrary.Models;

namespace StudentBook
{
    class Quiz
    {
        public List<QuestionsToView> Questions;
        public List<QuestionsToView> CorrectQuestions;
        public List<QuestionsToView> UncorrectQuestions;

        public Quiz()
        {
            Questions = new List<QuestionsToView>();
            CorrectQuestions = new List<QuestionsToView>();
            UncorrectQuestions = new List<QuestionsToView>();
        }
        public Quiz(int count)
        {
            Questions = new List<QuestionsToView>(count);
            CorrectQuestions = new List<QuestionsToView>();
            UncorrectQuestions = new List<QuestionsToView>();
        }
        public Quiz(List<QuestionsToView> questions)
        {
            Questions = new List<QuestionsToView>(questions);
            CorrectQuestions = new List<QuestionsToView>();
            UncorrectQuestions = new List<QuestionsToView>();
        }
    }
}

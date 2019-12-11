using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DatabaseLibrary.Models
{
    public class QuestionsToView
    {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string Task { get; set; }
        public int[] CorrectAnswer { get; set; }
        public string[] Answers { get; set; }

        public QuestionsToView(Questions questions)
        {
            ID = questions.ID;
            TopicID = questions.TopicID;
            Task = questions.Task;
            CorrectAnswer = JsonConvert.DeserializeObject<int[]>(questions.CorrectAnswer);
            Answers = JsonConvert.DeserializeObject<string[]>(questions.Answers);
            Array.Sort(CorrectAnswer);
        }
        public QuestionsToView(Questions questions, QuestionTranslate translate)
        {
            ID = questions.ID;
            TopicID = questions.TopicID;
            Task = translate.Task;
            CorrectAnswer = JsonConvert.DeserializeObject<int[]>(questions.CorrectAnswer);
            Answers = JsonConvert.DeserializeObject<string[]>(translate.Answers);
            Array.Sort(CorrectAnswer);
        }
        public bool CheckAnswers(int[] answers)
        {
            Array.Sort(answers);
            if (answers.Length != CorrectAnswer.Length)
                return false;
            for(var i=0;i< answers.Length; i++)
            {
                if(answers[i]!= CorrectAnswer[i])
                    return false;
            }
            return true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for checking whether an answer is correct in the quiz
public class Answers : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public void Answer()
    {
        if(isCorrect)
        {
            quizManager.CorrectAnswer();
        }
        else
        {
            quizManager.WrongAnswer();
        }
    }
}

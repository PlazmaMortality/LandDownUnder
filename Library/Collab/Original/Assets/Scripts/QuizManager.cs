using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<Questions> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject quizBox;
    public GameObject sureBox;
    public GameObject statsBox;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI animalsFoundText;

    int totalQuestions;
    public int score;
    public bool inQuiz;
    int numberOfQuestions;

    void Start()
    {
        numberOfQuestions = 3;
        inQuiz = false;
        score = 0;
        totalQuestions = numberOfQuestions;
        GenerateQuestion();
    }

    public void BeginQuiz()
    {
        inQuiz = true;
        Pause();
        sureBox.SetActive(false);
        quizBox.SetActive(true);
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].answers[i];
        
            if(QnA[currentQuestion].correctAnswer == i)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }

    void GenerateQuestion()
    {
        if(numberOfQuestions > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            questionText.text = QnA[currentQuestion].question;
            SetAnswer();
            numberOfQuestions--;
        }
        else
        {
            QuizEnd();
        }
    }

    public void QuizEnd()
    {
        float t = Time.timeSinceLevelLoad;
        int seconds = (int)(t % 60);
        int minutes = (int)(t / 60);
        minutes = minutes % 60;

        quizBox.SetActive(false);
        statsBox.SetActive(true);
        scoreText.text = "Quiz Score - " + score + "/" + totalQuestions;
        if (seconds < 10)
        {
            timeText.text = "Time - " + minutes + ":0" + seconds;
        }
        else
        {
            timeText.text = "Time - " + minutes + ":" + seconds;
        }
        int found = FindObjectOfType<PestManager>().pestsFound;
        animalsFoundText.text = "Pests Identified - " + found;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void CorrectAnswer()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    public void WrongAnswer()
    {
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

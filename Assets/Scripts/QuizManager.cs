using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

// Manages the quiz interactions and results

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

    // Sets the number of questions, score and total questions
    void Start()
    {
        numberOfQuestions = 5;
        inQuiz = false;
        score = 0;
        totalQuestions = numberOfQuestions;
        GenerateQuestion();
    }

    //Displays the quiz, and stops other menu interactions while still in the quiz
    public void BeginQuiz()
    {
        inQuiz = true;
        Pause();
        sureBox.SetActive(false);
        quizBox.SetActive(true);
    }

    // Sets the answers of the question to the various options, as well as setting the correct answer where specified
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

    // Presents a random question from the pool of total questions
    void GenerateQuestion()
    {
        // Continues until the number of questions in the quiz reaches 0
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

    // Displays statistics of the game on a final screen whenthe quiz is completed
    public void QuizEnd()
    {
        // Stores time in the current scene and calculates minutes/seconds
        float t = Time.timeSinceLevelLoad;
        int seconds = (int)(t % 60);
        int minutes = (int)(t / 60);
        minutes = minutes % 60;

        quizBox.SetActive(false);
        statsBox.SetActive(true);
        // Displays the score of the quiz
        scoreText.text = "Quiz Score - " + score + "/" + totalQuestions;
        // Adds a 0 if seconds is less than 10. An exmaple change - :4 to :04
        if (seconds < 10)
        {
            timeText.text = "Time - " + minutes + ":0" + seconds;
        }
        else
        {
            timeText.text = "Time - " + minutes + ":" + seconds;
        }
        // Displays the number of pests the player found within the game
        int found = FindObjectOfType<PestManager>().pestsFound;
        animalsFoundText.text = "Pests Identified - " + found;

        //Creates file with player data
        CreateFile(minutes, seconds, found);
    }

    //Creates a file storing the player data
    void CreateFile(int m, int s, int f)
    {
        // Path of file
        string path = Application.dataPath + "/Stats.txt";

        //Creates file if one doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Land Down Under Player Statistics\n\n");
        }

        //Stores the date, score, time taken and pests found
        string info = "Date - " + System.DateTime.Now + "\n" + 
            "Quiz Score - " + score + "/" + totalQuestions + "\n" +
            "Time Taken - " + m + ":" + s + "\n" +
            "Pests Identified - " + f + "\n\n";

        //Appends info to file
        File.AppendAllText(path, info);
    }

    //Loads the main menu by switching scenes
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Increases the score and removes the question in order to not repeat when another is generated.
    public void CorrectAnswer()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    //Removes the question in order to not repeat when another is generated. No score change
    public void WrongAnswer()
    {
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    //Pauses time and enables cursor
    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Reloads the current scene
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Exits the game
    public void Quit()
    {
        Application.Quit();
    }
}

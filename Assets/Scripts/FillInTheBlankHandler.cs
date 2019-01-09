using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class FillInTheBlankHandler : MonoBehaviour
{

    #region Global Variables
    TextMeshPro question;
    TextMeshPro questionType;
    TextMeshPro awnserA;
    TextMeshPro awnserB;
    TextMeshPro awnserC;
    TextMeshPro awnserD;

    public enum QuestionType { FillInTheBlank, abcd, VoiceRec };
    public QuestionType TypeOfQuestion;
    public string QuestionText;
    public string AText = "Dog";
    public string BText = "Cat";
    public string CText = "Fish";
    public string DText = "Donkey";

    public enum CorrectAwnserEvent { NextQuestion, ChangeLevel, Extra };
    public CorrectAwnserEvent NextQuestion;

    public int sceneToLoad = 0;

    #endregion

    /// <summary>
    /// called once when first created
    /// initializes varialbles
    /// </summary>
    void Start()
    {
        #region init vars
        questionType = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        question = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        awnserA = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        awnserB = transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
        awnserC = transform.GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshPro>();
        awnserD = transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshPro>();
        #endregion

        //if the current question is equal to the 3rd question asked then set the level to be changed when answers.
        if (StaticVars.NumOfQuestions == 3)
        {
            NextQuestion = CorrectAwnserEvent.ChangeLevel;
        }

        //ask the first question
        AskQuestion();
    }

    /// <summary>
    /// set the questions and answers
    /// </summary>
    private void AskQuestion()
    {
        StaticVars.NumOfQuestions++;

        switch (TypeOfQuestion)
        {
            case QuestionType.FillInTheBlank:
                questionType.text = "Fill in the blank!";
                question.text = "My " + "____" + " likes to go for walks!";
                break;
            case QuestionType.abcd:
                questionType.text = "Multiple choice!";
                question.text = "which animal likes to go for walks!";
                break;
            case QuestionType.VoiceRec:
                questionType.text = "Say the awnser!";
                question.text = "My " + "____" + " likes to go for walks!";
                break;
        }

        question.text = QuestionText;
        awnserA.text = AText;
        awnserB.text = BText;
        awnserC.text = CText;
        awnserD.text = DText;
    }

    /// <summary>
    /// event to call when Answer is pressed.
    /// </summary>
    public void AnswerEvent()
    {

        switch (NextQuestion)
        {
            case CorrectAwnserEvent.NextQuestion:
                AskQuestion();
                break;
            case CorrectAwnserEvent.ChangeLevel:
                ChangeLevel(sceneToLoad);
                break;
            case CorrectAwnserEvent.Extra:

                break;
        }

    }

    /// <summary>
    /// change the level
    /// </summary>
    /// <param name="i"></param>
    private void ChangeLevel(int i)
    {
        SceneManager.LoadScene(i);
    }


}

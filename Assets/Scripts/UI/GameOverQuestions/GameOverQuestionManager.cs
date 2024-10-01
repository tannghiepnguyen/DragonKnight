using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverQuestionManager : MonoBehaviour
{

    public GameObject questionUIObject;
    public UIManager uIManager;
    public TextMeshProUGUI mainQuestion;
    public GameOverQuestionLib questionLibrary;
    public GameObject questionSlotPrefab;
    public Transform answerContainer;

    private GameOverQuestionData selectedQuestion;

    

    // Start is called before the first frame update
    void Start()
    {
        selectedQuestion = questionLibrary.questionDatas[Random.Range(0, questionLibrary.questionDatas.Length)];
        mainQuestion.SetText(selectedQuestion.question);

        foreach (var answer in selectedQuestion.answers){
            GameObject instance = Instantiate(questionSlotPrefab);
            
            int i = 0;
            instance.GetComponent<GameOverQuestionSlot>().text.SetText(answer);
            if (i == selectedQuestion.correctAnswer){
                instance.GetComponent<GameOverQuestionSlot>().isCorrect = true;
            }
            instance.GetComponent<Button>().onClick.AddListener(() => OnAnswerSelected(instance));

            instance.transform.parent = answerContainer;
        }

    }

    void OnAnswerSelected(GameObject answer){
        bool isCorrect = answer.GetComponent<GameOverQuestionSlot>().isCorrect;
        if (!isCorrect){
            questionUIObject.SetActive(false);
            uIManager.GameOver();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

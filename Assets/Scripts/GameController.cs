using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public GameObject[] hangMan;
    public Text wordToFindField;
    public GameObject winText;
    public GameObject loseText;
    public GameObject replayButton;

    private float time;
    private string[] wordsLocal = { "SERGIO AGUERO", "DAVID SILVA","COLIN BELL","VINCENT KOMPANY","YAYA TOURE","KEVIN DE BRUYNE", "ANH DUY" };
    //private string[] words = File.ReadAllLines(@"Assets/Words.txt");
    private string chosenWord;
    private string hiddenWord;
    private int fails;
    private bool gameEnd;
    // Start is called before the first frame update
    void Start()
    {
        
        chosenWord = wordsLocal[Random.Range(0, wordsLocal.Length)];
        for (int i =0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += " "; 
            }
            else
            {
                hiddenWord += "-";
            }
            wordToFindField.text = hiddenWord ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd == false)
        {
            time += Time.deltaTime;
            timeField.text = time.ToString();
        }

    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 )
        {
            string pressedLetter = e.keyCode.ToString();
            Debug.Log(e.keyCode.ToString());

            if (chosenWord.Contains(pressedLetter)) // press true
            {
                int i = chosenWord.IndexOf(pressedLetter);
                while (i != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, i) + pressedLetter + hiddenWord.Substring(i + 1);
                    Debug.Log(hiddenWord);
                    chosenWord = chosenWord.Substring(0, i) + "-" + chosenWord.Substring(i + 1);
                    Debug.Log(chosenWord);
                    i = chosenWord.IndexOf(pressedLetter);
                }
                wordToFindField.text = hiddenWord;
            }
            else // add a hang man body part
            {
                hangMan[fails].SetActive(true);
                fails++;
            }
            if (fails == hangMan.Length) // case lost
            {
                loseText.SetActive(true);
                gameEnd = true;
                replayButton.SetActive(true);
            }

            if (!hiddenWord.Contains("-")) // case win
            {
                winText.SetActive(true);
                gameEnd = true;
                replayButton.SetActive(true);
            }

        }
    }
}
 
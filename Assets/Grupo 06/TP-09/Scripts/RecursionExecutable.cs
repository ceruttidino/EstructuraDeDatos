using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecursionExecutable : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI outputText;

    private bool IsPalindrome(string text, int i, int j)
    {
        if(i >= j)
        {
            return true;
        }
        if (text[i] != text[j]) 
        {
            return false;
        }
        return IsPalindrome(text, i + 1, j - 1);
    }


    public void Palindrome()
    {
        string word = inputField.text.ToLower().Replace(" ", "");
        bool isPlaindrome = IsPalindrome(word, 0, word.Length - 1);

        if (isPlaindrome)
        {
            outputText.text = "Es Palindromo";
        }
        else
        {
            outputText.text = "No Es Palindromo";
        }
    }

    private int RecursionFact(int n)
    {
        if(n <= 1)
        {
            return 1;
        }
        return n * RecursionFact(n - 1);
    }

    public void Factorial()
    {
        int n = int.Parse(inputField.text);
        outputText.text = "Factorial de " + n + " = " + RecursionFact(n); 
    }
}

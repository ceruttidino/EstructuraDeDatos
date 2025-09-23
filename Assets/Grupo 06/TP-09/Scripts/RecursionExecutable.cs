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


    private int RecursionFib(int n)
    {
        if (n <= 1) return n;
        return RecursionFib(n - 1) + RecursionFib(n - 2);

    }

    public void Fibonacci()
    {
        int n = int.Parse(inputField.text);
        string result = "";
        for (int i = 0; i < n; i++) 
        {
            result += RecursionFib(i) + " ";
        }
        outputText.text = "Fibonacci ("+ n +"): " + result;
    }

    private int RecursionSum(int n)
    {
        if(n <= 1) return 0;
        return n + RecursionSum(n - 1);
    }

    public void Sumatoria()
    {
        int n = int.Parse(inputField.text);
        outputText.text = "Suma de 1 a " + n + " = " + RecursionSum(n);
    }

    private string RecursionPyramid(int current, int max)
    {
        if (current > max) return "";

        int xs = current * 2 - 1;
        int spaces = max - current;

        string line = new string(' ', spaces) + new string('x', xs) + "\n";

        return line + RecursionPyramid(current + 1, max);
    }


    public void Pyramid()
    {
        int n = int.Parse (inputField.text);
        outputText.text = RecursionPyramid(1, n);
    }
}

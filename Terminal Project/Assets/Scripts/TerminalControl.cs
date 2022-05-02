using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerminalControl : MonoBehaviour
{
    private string pass;
    enum Screen { MainMenu, Password, Win };

    private Screen currentScreen = Screen.MainMenu;
    private const string menuHint = "Напишите 'Меню', чтобы вернуться назад";
    private int level;
    private string password;
    private string[] Level1Passwords = {"ключ", "книга", "шкаф", "ручка", "текст"};
    private string[] Level2Passwords = {"дубинка", "полицейский", "начальник", "рапорт", "арест"};
    private string[] Level3Passwords = {"спутник", "комета", "космос", "галактика", "звезда"};
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu("Олег");
    }

    


    void ShowMainMenu(string playerName)
    {
        currentScreen = Screen.MainMenu;
        level = 0;
        Terminal.ClearScreen();
        Terminal.WriteLine("Привет, "+playerName+ "!");
        Terminal.WriteLine("Какой терминал ты хочешь взломать?");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Введите 1 - " + "Библиотека");
        Terminal.WriteLine("Введите 2 - полицейский участок");
        Terminal.WriteLine("Введите 3 - космический корабль");
        Terminal.WriteLine("Ваш выбор:");
    }

   
    void OnUserInput(string input)
    {
        if (input == "Меню")
        {
            ShowMainMenu("рад видеть тебя снова");
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            ShowWinScreen();
        }
        else
        {
            Terminal.WriteLine("Попробуйте еще раз...");
            GameStart();
        }
    }

    void ShowWinScreen()
    {
        Terminal.ClearScreen();
        Reward();
    }

    void Reward()
    {
        currentScreen = Screen.Win;
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Пароль верный! Держите вашу книгу:");
                Terminal.WriteLine(
                    @"
    _______
   /      /,
  /      //
 /______//
(______(/
                    ");
                break;
        }
        switch (level)
        {
            case 2:
                Terminal.WriteLine("Пароль верный! Держите ваш пистолет:");
                Terminal.WriteLine(
                    @"
__,___________
/ __.==-----/
/(-'
`-'
                    ");
                break;
        }
        switch (level)
        {
            case 3:
                Terminal.WriteLine("Пароль верный! Держите вашего гуманоида:");
                Terminal.WriteLine(
                    @"
  _________
 /___   ___\
//@@@\ /@@@\\
\\@@@/ \@@@//
 \___ э ___/ 
    | - |
     \_/
                    ");
                break;
        }
        Terminal.WriteLine(menuHint);
    }
    
    void RunMainMenu(string input)
    { 
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            GameStart();
        }
       
        else if (input == "007")
        {
            Terminal.WriteLine("Hello Mr Bond");
        }
        else
        {
            Terminal.WriteLine("Введите правильно значение");
        }
    }

    void GameStart()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        switch (level)
        {
            case 1:
                password = Level1Passwords[Random.Range(0, Level1Passwords.Length)];
                Terminal.WriteLine("Вы в городской библиотеке.");
                break;
            case 2:
                password = Level2Passwords[Random.Range(0, Level2Passwords.Length)];
                Terminal.WriteLine("Вы в полицейском участке");
                break;
            case 3:
                password = Level3Passwords[Random.Range(0, Level3Passwords.Length)];
                Terminal.WriteLine("Вы на космическом корабле");
                break;
            default:
                Debug.LogError("Такого уровня не существует");
                break;
        }
        Terminal.WriteLine("Подсказка:" + password.Anagram());
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("Введите пароль: ");
    }
}

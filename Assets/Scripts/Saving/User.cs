using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string uid = "";
    public string name = "";
    public int score = 0;

    public User(string userid, string userName, int userScore)
    {
        uid = userid;
        name = userName;
        score = userScore;
    }

    public User()
    {
        uid = "";
        name = "";
        score = 0;
    }
}

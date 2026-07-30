using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : View
{
    [SerializeField] private List<LeaderboardUser> leaderboardUsers = new List<LeaderboardUser>();

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void GetTopPlayers(List<UserData> users)
    {
        leaderboardUsers.ForEach(user => user.Clear());

        for (int i = 0; i < users.Count; i++)
        {
            leaderboardUsers[i].SetData(users[i].Nickname, users[i].Record);
        }
    }
}

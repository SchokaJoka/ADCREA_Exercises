using System;
using UnityEngine;
using System.Collections.Generic;


public class SocialNetwork : MonoBehaviour
{
    private Dictionary<string, Person> mySocialNetwork;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySocialNetwork = new Dictionary<string, Person>();
        
        // Create social network from list of names
        string[] names = {"Alice", "Bob", "Charlie", "Diana", "Eve"};
        foreach (string alias in names)
        {
            mySocialNetwork[alias] = new Person(alias);
        }
        
        // Add friends from list of name pairs
        List<(string, string)> friendShips = new List<(string, string)>
        {
            ("Alice", "Bob"), ("Alice", "Charlie"), ("Bob", "Diana"),
            ("Charlie", "Eve"), ("Bob", "Charlie"), ("Diana", "Charlie")
        };

        foreach (var (name1, name2) in friendShips)
        {
            mySocialNetwork[name1].AddFriend(mySocialNetwork[name2]);
            // Ensure bidirectional friendship
            mySocialNetwork[name2].AddFriend(mySocialNetwork[name1]);
        }
    }

    void OnDrawGizmos()
    {
        if (mySocialNetwork == null) return;

        float rot = 0.0f;
        float rotIncrement = 2f * (float)Math.PI / mySocialNetwork.Count;
        Dictionary<string, Vector3> positions = new Dictionary<string, Vector3>();
        
        // Drawing members in a circle
        Gizmos.color = Color.cyan;
        foreach (var person in mySocialNetwork.Values)
        {
            Vector3 pos = new Vector3(Mathf.Sin(rot) * 10, 0 , Mathf.Cos(rot) * 10);
            Gizmos.DrawSphere(pos, 0.3f);
            positions[person.name] = pos;
            UnityEditor.Handles.Label(positions[person.name] + Vector3.up * 0.4f, person.name);
            rot += rotIncrement;
        }
        
        // Drawing friendship lines between members
        Gizmos.color = Color.yellow;
        foreach (var person in mySocialNetwork.Values)
        {
            foreach (var friend in person.friends)
            {
                if(String.CompareOrdinal(person.name, friend.name) < 0) // Avoid duplicates
                {
                    Gizmos.DrawLine(positions[person.name], positions[friend.name]);
                }
            }
        }
    }

    // Gibt den Namen der Person mit den meisten Freunden zurück
    public string GetPersonWithMostFriends()
    {
        string maxPerson = null;
        int maxFriends = -1;
        foreach (var person in mySocialNetwork.Values)
        {
            if (person.friends.Count > maxFriends)
            {
                maxFriends = person.friends.Count;
                maxPerson = person.name;
            }
        }
        return maxPerson;
    }

    // Gibt eine Liste der Personen ohne Freunde zurück
    public List<string> GetPeopleWithNoFriends()
    {
        List<string> noFriends = new List<string>();
        foreach (var person in mySocialNetwork.Values)
        {
            if (person.friends.Count == 0)
            {
                noFriends.Add(person.name);
            }
        }
        return noFriends;
    }

    // Gibt die durchschnittliche Anzahl an Freunden pro Person zurück
    public float GetAverageNumberOfFriends()
    {
        if (mySocialNetwork.Count == 0) return 0f;
        int totalFriends = 0;
        foreach (var person in mySocialNetwork.Values)
        {
            totalFriends += person.friends.Count;
        }
        return (float)totalFriends / mySocialNetwork.Count;
    }
}

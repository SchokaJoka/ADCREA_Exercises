using UnityEngine;
using System.Collections.Generic;

public class Person
{
    public string name { get; set; }
    public List<Person> friends { get; private set; }

    public Person(string newName)
    {
        name = newName;
        friends = new List<Person>();
    }

    public void AddFriend(Person person)
    {
        if (!friends.Contains(person))
        {
            friends.Add(person);
        }
    }
}

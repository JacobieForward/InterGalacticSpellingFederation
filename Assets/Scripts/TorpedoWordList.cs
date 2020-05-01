using System;
using System.Collections.Generic;

public class TorpedoWordList {
    // TODO: Store word lists elsewhere
    private List<String> tier1Words = new List<String>();
    private List<String> tier2Words = new List<String>();
    private List<String> tier3Words = new List<String>();
    private List<String> tier4Words = new List<String>();

    private List<List<String>> listOfWordLists = new List<List<String>>();

    private void Awake() {
        initWordLists();
    }

    public String GetRandomWordForLevel(int level) {
        List<String> wordList = listOfWordLists[level];
        int randomNum = (int)UnityEngine.Random.Range(0, wordList.Count);
        return wordList[randomNum];
    }

    public void initWordLists() {
        tier1Words.Add("Jam");
        tier1Words.Add("Lit");
        tier1Words.Add("Fit");
        tier1Words.Add("Hit");
        tier1Words.Add("How");
        tier1Words.Add("Cow");
        tier1Words.Add("Sow");
        tier1Words.Add("Can");
        tier1Words.Add("Pow");
        tier1Words.Add("Yes");
        tier1Words.Add("Sum");
        tier1Words.Add("Sun");
        tier1Words.Add("Son");
        tier1Words.Add("Yea");
        tier1Words.Add("Pea");

        tier2Words.Add("Spit");
        tier2Words.Add("Rick");
        tier2Words.Add("Mitt");
        tier2Words.Add("Slit");
        tier2Words.Add("Yard");
        tier2Words.Add("Hard");
        tier2Words.Add("Fire");
        tier2Words.Add("Nice");
        tier2Words.Add("Dice");
        tier2Words.Add("Rice");
        tier2Words.Add("Mice");
        tier2Words.Add("Ripe");
        tier2Words.Add("Nice");
        tier2Words.Add("Yeah");
        tier2Words.Add("Glee");
        tier2Words.Add("Read");
        tier2Words.Add("Flee");
        tier2Words.Add("Home");
        tier2Words.Add("Beer");
        tier2Words.Add("Bear");
        tier2Words.Add("Asia");

        tier3Words.Add("Flick");
        tier3Words.Add("Frizz");
        tier3Words.Add("Huzza");
        tier3Words.Add("Pizza");
        tier3Words.Add("Shoot");
        tier3Words.Add("Shirt");
        tier3Words.Add("Drive");
        tier3Words.Add("Plant");
        tier3Words.Add("Fires");
        tier3Words.Add("Beers");
        tier3Words.Add("Bears");
        tier3Words.Add("Steer");
        tier3Words.Add("Could");
        tier3Words.Add("Would");
        tier3Words.Add("Flood");
        tier3Words.Add("Flame");
        tier3Words.Add("Blame");
        tier3Words.Add("Table");
        tier3Words.Add("China");
        tier3Words.Add("Blood");
        tier3Words.Add("Sword");

        tier4Words.Add("Muzzle");
        tier4Words.Add("Puzzle");
        tier4Words.Add("Guzzle");
        tier4Words.Add("Little");
        tier4Words.Add("Should");
        tier4Words.Add("Destroy");
        tier4Words.Add("Health");
        tier4Words.Add("Germany");
        tier4Words.Add("Measure");
        tier4Words.Add("Knight");
        tier4Words.Add("Chipper");
        tier4Words.Add("Maximum");
        tier4Words.Add("Flight");
        tier4Words.Add("Welcome");
        tier4Words.Add("Memeber");
        tier4Words.Add("December");
        tier4Words.Add("November");
        tier4Words.Add("Monday");
        tier4Words.Add("Sunday");

        listOfWordLists.Add(tier1Words);
        listOfWordLists.Add(tier2Words);
        listOfWordLists.Add(tier3Words);
        listOfWordLists.Add(tier4Words);
    }
}

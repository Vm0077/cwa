using System;
using System.Linq;
using UnityEditor;

public class GameData {
    public int coin;
    public int life;
    public int star;
    public float time;
    public long lastUpdated;
    // TO-DO: save level
    //public Dictionary<String, bool> level = new level;
    // New Game
    public GameData() {
        this.coin = 0;
        this.life = 3;
        this.star = 0;
        this.time = 0;
    }
}

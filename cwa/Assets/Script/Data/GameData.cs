using System;
using UnityEditor;

public class GameData {
    int deathCount;
    int coin;
    int life;
    int star;
    public long lastUpdated;
    public GameData() {
        this.deathCount = 0;
        this.coin = 0;
        this.life = 0;
        this.star = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    private static int globalCoins = 0;

    private static float globalTimer = 120;

    public static void SetTimer(float f)
    {
        globalTimer = f;
    }

    public static float GetTimer()
    {
        return globalTimer;
    }

    public static void SetCoins(int i)
    {
        globalCoins = i;
    }

    public static int GetCoins()
    {
        return globalCoins;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    private static int globalCoins = 0;

    public static void SetCoins(int i)
    {
        globalCoins = i;
    }

    public static int GetCoins()
    {
        return globalCoins;
    }
}

using UnityEngine;
using System.Linq;

public static class ExtensionMethods
{
    public static void ShuffleArray<T>(this T[] arrayToShuffle) {
        System.Random rnd= new System.Random();
        arrayToShuffle = arrayToShuffle.OrderBy(x => rnd.Next()).ToArray(); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Utils
{
    public static class Extentions
    {
        public static bool HasAValue(this string _string)
        {
            return (!string.IsNullOrEmpty(_string) && !string.IsNullOrWhiteSpace(_string));
        }

        public static bool HasComponent<T>(this GameObject _gameObjet, out T _reference)
        {
            _reference = _gameObjet.GetComponent<T>();
            return (_reference != null);
        }
    }
}

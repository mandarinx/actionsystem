using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace AptGames {

    public static class ArrayUtils {

        public static int IndexOfType<T>(IReadOnlyList<T> arr, Type type) {
            for (int i = 0; i < arr.Count; ++i) {
                if (arr[i].GetType() == type) {
                    return i;
                }
            }
            return -1;
        }
        
        public static bool ArrayContainsType<T>(IReadOnlyList<T> arr, Type contains) {
            for (int i = 0; i < arr.Count; ++i) {
                if (arr[i].GetType() == contains) {
                    return true;
                }
            }
            return false;
        }

        public static void Shuffle(int[] list) {
            // Shuffle using Fisher-Yates algorithm
            for (int i = list.Length; i > 1; --i) {
                int j = Random.Range(0, (i - 1));
                int tmp = list[j];
                list[j] = list[i - 1];
                list[i - 1] = tmp;
            }
        }

        /// <summary>
        /// Fills an array with a linearly increasing value, starting at 0
        /// </summary>
        /// <param name="list">An array of integers</param>
        public static void FillLinear(int[] list) {
            for (int i = 0; i < list.Length; ++i) {
                list[i] = i;
            }
        }

        public static void SelectionSortAsc(int[] arr, int from, int to) {
            int len = to - from;
            for (int j = from; j < len - 1; j++) {
                int min_key = j;
 
                for (int k = j + 1; k < len; k++) {
                    if (arr[k] < arr[min_key]) {
                        min_key = k;
                    }
                }
 
                int tmp = arr[min_key];
                arr[min_key] = arr[j];
                arr[j]       = tmp;
            }
        }
        
        public static void SelectionSortDesc(int[] arr, int from, int to) {
            int len = to - from;
            for (int j = from; j < len - 1; j++) {
                int max_key = j;

                for (int k = j + 1; k < len; k++) {
                    if (arr[k] > arr[max_key]) {
                        max_key = k;
                    }
                }

                int tmp = arr[max_key];
                arr[max_key] = arr[j];
                arr[j]       = tmp;
            }
        }        
    }
}

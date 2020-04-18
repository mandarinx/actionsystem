using System;

namespace RL {

    public static class Utils {

        public static string PrintInt32(int i) {
            return Convert.ToString(i, 2).PadLeft(32, '0');
        }

        public static string Capitalize(string str) {
            return $"{str.Substring(0, 1).ToUpper()}{str.Substring(1).ToLower()}";
        }
    }
}
using System;

namespace RL {

    public static class Utils {

        public static string PrintInt32(int i) {
            return Convert.ToString(i, 2).PadLeft(32, '0');
        }
    }
}
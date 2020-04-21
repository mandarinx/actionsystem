
namespace AptGames {

    public static class M {

        /// <summary>
        /// Returns the average between two integers, in one CPU operation.
        /// </summary>
        /// <param name="a">Any integer</param>
        /// <param name="b">Any integer</param>
        /// <returns>The average of the two provided integers</returns>
        public static int Avgerage(int a, int b) {
            return (a >> 1) + (b >> 1) + (a & b & 1);
        }

        public static int Map(int value, int aMin, int aMax, int bMin, int bMax) {
            return (value - aMin) / (aMax - aMin) * (bMax - bMin) + bMin;
        }

        public static float Map(float value, float aMin, float aMax, float bMin, float bMax) {
            return (value - aMin) / (aMax - aMin) * (bMax - bMin) + bMin;
        }
    }
}
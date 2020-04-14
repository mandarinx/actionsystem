namespace RL {

    public static class ArrayExtensions {

        public static T Find<T>(this IAction[] list) where T : class {
            for (int i = 0; i < list.Length; ++i) {
                if (list[i] is T elm) {
                    return elm;
                }
            }
            return default;
        }
    }
}
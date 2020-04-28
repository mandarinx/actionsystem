namespace RL.Systems.Map {

    public static class TilingMethods {

        public static int Autotile(Map map, int index, Group group) {
            int tile = 0;
            if (map.HasTile(index + map.Width, group)) {
                tile |= Map.MASK_N;
            }
            if (map.HasTile(index + 1, group)) {
                tile |= Map.MASK_E;
            }
            if (map.HasTile(index - map.Width, group)) {
                tile |= Map.MASK_S;
            }
            if (map.HasTile(index - 1, group)) {
                tile |= Map.MASK_W;
            }
            return tile;
        }
    }
}
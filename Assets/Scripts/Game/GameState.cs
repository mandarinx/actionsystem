namespace RL {

    public enum GameState {
        UNDEFINED             = 0,
        WARMUP                = 1,
        AWAITING_PLAYER_INPUT = 2,
        RESOLVE_ACTIONS       = 3,
        PLAY_ANIMATIONS       = 4,
        GAME_OVER             = 1000,
    }
}
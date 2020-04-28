namespace RL {

    public enum GameState {
        UNDEFINED              = 0,
        AWAITING_PLAYER_INPUT  = 2,
        RESOLVE_PLAYER_ACTIONS = 3,
        RESOLVE_NPC_ACTIONS    = 4,
        WILL_UPDATE_SYSTEMS         = 5,
        UPDATE_SYSTEMS         = 6,
        GAME_OVER              = 1000,
    }
}
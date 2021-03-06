﻿
using RL.Systems.Items;

namespace RL {

    public interface IActionSystem {
        void Resolve(Item source, IAction action, Item target);
    }
}
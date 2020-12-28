using PvPModifier.Variables;
using System;

namespace PvPModifier.Network.Packets {

    public class PlayerDeathArgs : EventArgs {
        public PvPPlayer Dead;

        public bool ExtractData(PvPPlayer dead, out PlayerDeathArgs arg) {
            arg = new PlayerDeathArgs {
                Dead = dead
            };

            return true;
        }
    }
}
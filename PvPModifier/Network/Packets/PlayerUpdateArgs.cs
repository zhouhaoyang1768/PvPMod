﻿using PvPModifier.Variables;
using System;
using System.IO;

namespace PvPModifier.Network.Packets {

    public class PlayerUpdateArgs : EventArgs {
        public PvPPlayer Player { get; set; }

        public int PlayerAction { get; set; }
        public int Pulley { get; set; }
        public int Potion { get; set; }
        public int Rotation { get; set; }
        public int SelectedSlot { get; set; }

        public bool ExtractData(MemoryStream data, PvPPlayer player, out PlayerUpdateArgs arg) {
            data.ReadByte();

            arg = new PlayerUpdateArgs {
                Player = player,
                PlayerAction = data.ReadByte(),
                Pulley = data.ReadByte(),
                Potion = data.ReadByte(),
                Rotation = data.ReadByte(),
                SelectedSlot = data.ReadByte()
            };

            return true;
        }
    }
}
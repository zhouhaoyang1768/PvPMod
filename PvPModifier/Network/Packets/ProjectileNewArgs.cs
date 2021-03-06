﻿using Microsoft.Xna.Framework;
using PvPModifier.Utilities;
using PvPModifier.Variables;
using System;
using System.IO;
using System.IO.Streams;
using Terraria;
using TerrariaApi.Server;

namespace PvPModifier.Network.Packets {

    public class ProjectileNewArgs : EventArgs {
        public GetDataEventArgs Args;
        public PvPPlayer Attacker;
        public PvPItem Weapon;
        public PvPProjectile Proj;

        public int Identity;
        public Vector2 Position;
        public Vector2 Velocity;
        public Single Knockback;
        public int Damage;
        public int Owner;
        public int Type;
        public BitsByte AiFlags;
        public float Ai0;
        public float Ai1;
        public float[] Ai;

        public bool ExtractData(GetDataEventArgs args, MemoryStream data, PvPPlayer attacker, out ProjectileNewArgs arg) {
            arg = null;
            if (PresetData.ProjectileDummy.Contains(Type)) return false;

            try {
                arg = new ProjectileNewArgs {
                    Args = args,
                    Attacker = attacker,

                    Identity = data.ReadInt16(),
                    Position = new Vector2(data.ReadSingle(), data.ReadSingle()),
                    Velocity = new Vector2(data.ReadSingle(), data.ReadSingle()),
                    Owner = data.ReadByte(),
                    Type = data.ReadInt16(),
                    AiFlags = (BitsByte)data.ReadByte(),
                    Ai0 = AiFlags[0] ? data.ReadSingle() : 0,
                    Ai1 = AiFlags[1] ? data.ReadSingle() : 0,

                    Damage = data.ReadInt16(),
                    Knockback = data.ReadByte(),

                    Ai = new float[Projectile.maxAI],

                    Proj = new PvPProjectile(Type, Identity),
                    Weapon = ProjectileUtils.GetProjectileWeapon(attacker, Type)
                };
            } catch (EndOfStreamException) {
                return false;
            }

            return true;
        }
    }
}
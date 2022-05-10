using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [Serializable]
    public class Attributes
    {
        public string name;
        public Category category;
        public int level;
        public float health;
        public float attack;
        public float defense;
        public float dextery;
        public float speed;
        public float cooldown;

        public Attributes(Attributes attributes)
        {
            level = attributes.level;
            health = attributes.health;
            attack = attributes.attack;
            defense = attributes.defense;
            dextery = attributes.dextery;
            speed = attributes.speed;
            cooldown = attributes.cooldown;
        }
    }
    public enum Category
    {
        Human,
        Cop,
        Robot,
        Vehicle,
        Weapon
    }

}
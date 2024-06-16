using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstantNumberKoto
{
    public static class ConstantNumberKoto
    {
        public enum EVENT_TYPE
        {
            BOSS_YOKAI01,
            BOSS_YOKAI02,
        }
        public static EVENT_TYPE eventType;
        public static int EVENT_TYPE_COUNT = 2;
    }
}
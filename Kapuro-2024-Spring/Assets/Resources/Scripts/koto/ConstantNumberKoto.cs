using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstantNumberKoto
{
    public static class ConstantNumberKoto
    {
        public enum EVENT_TYPE
        {
            KAWARAYOKAI,
            SHISHIGAWARA,
            SHISHIGAWARA_EVENT,
            KAWARA_BOUZU,
            KAWARA_BOUZU_EVENT,
        }
        public static EVENT_TYPE eventType;
        public static int EVENT_TYPE_COUNT = 2;
    }
}
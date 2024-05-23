using System;
using UnityEngine;

namespace ColorGame.Scripts.Colors
{
    [Serializable]
    public class ColorPalette
    {
        public string name;
        public Color colorA;
        public Color colorB;
        public Color colorC;
        public Color colorD;
        
        public Color this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return colorA;
                    case 1:
                        return colorB;
                    case 2:
                        return colorC;
                    case 3:
                        return colorD;
                    default:
                        throw new IndexOutOfRangeException("Invalid index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        colorA = value;
                        break;
                    case 1:
                        colorB = value;
                        break;
                    case 2:
                        colorC = value;
                        break;
                    case 3:
                        colorD = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid index");
                }
            }
        }
        public int Count => 4;
    }
}

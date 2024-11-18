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
                return index switch
                {
                    0 => colorA,
                    1 => colorB,
                    2 => colorC,
                    3 => colorD,
                    _ => throw new IndexOutOfRangeException("Invalid index")
                };
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

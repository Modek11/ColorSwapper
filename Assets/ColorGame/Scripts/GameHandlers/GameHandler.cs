using ColorGame.Scripts.Colors;
using ColorGame.Scripts.Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField] private ColorPalettesHolder colorPalettesHolder;

        public ColorPalette CurrentActiveColorPalette { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            //TODO: check color palette from save file
            var randomNumber = Random.Range(0, colorPalettesHolder.colorPalettes.Count);
            CurrentActiveColorPalette = colorPalettesHolder.colorPalettes[randomNumber];
        }
    }
}

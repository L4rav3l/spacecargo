using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace SpaceCargo;

public class EndFuel : IScene
{
    private GraphicsDevice _graphics;
    private SceneManager _sceneManager;
    private ContentManager _contentManager;

    private SpriteFont _pixelfont;
    private Texture2D _paper;
    private Song _fuel;

    private bool _end = false;

    public EndFuel(GraphicsDevice _graphics, SceneManager _sceneManager, ContentManager _contentManager)
    {
        this._graphics = _graphics;
        this._sceneManager = _sceneManager;
        this._contentManager = _contentManager;
    }

    public void LoadContent()
    {
        _pixelfont = _contentManager.Load<SpriteFont>("pixelfont");
        _fuel = _contentManager.Load<Song>("empty-fuel");

        _paper = new Texture2D(_graphics, 1, 1);
        _paper.SetData(new [] {Color.White});
    }

    public void Update(GameTime GameTime)
    {
        KeyboardState state = Keyboard.GetState();

        if(state.IsKeyDown(Keys.E))
        {
            GameData.InSpace = false;
            _sceneManager.ChangeScene("menu");
        }

        if(!_end)
        {
            MediaPlayer.Play(_fuel);
            _end = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int Width = _graphics.Viewport.Width;
        int Height = _graphics.Viewport.Height;

        _graphics.Clear(Color.Black);

        spriteBatch.Draw(_paper, new Rectangle((Width / 2) - 300, (Height / 2) - 350, 600, 700), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

        Vector2 Reason = new Vector2((Width / 2) - 250, (Height / 2) - 300);

        spriteBatch.DrawString(_pixelfont, "You didn't have enough fuel,\nand when they cameto help\nyou, you died from radiation.\nEnd 2", Reason, Color.Black, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
    
        Vector2 NextM = _pixelfont.MeasureString("Press E button") * 0.75f;
        spriteBatch.DrawString(_pixelfont, "Press E button", new Vector2((Width / 2) - (NextM.X / 2), ((Height / 4) * 3) - (NextM.Y / 2)), Color.Black, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
    }
}
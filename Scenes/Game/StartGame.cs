using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceCargo;

public class StartGame : IScene
{
    private GraphicsDevice _graphics;
    private SceneManager _sceneManager;
    private ContentManager _contentManager;

    private SpriteFont _pixelfont;
    private Texture2D _paper;

    public StartGame(GraphicsDevice _graphics, SceneManager _sceneManager, ContentManager _contentManager)
    {
        this._graphics = _graphics;
        this._sceneManager = _sceneManager;
        this._contentManager = _contentManager;
            
        _paper = new Texture2D(_graphics, 1, 1);
        _paper.SetData(new [] {Color.White});
    }

    public void LoadContent()
    {
        _pixelfont = _contentManager.Load<SpriteFont>("pixelfont");
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        if(state.IsKeyDown(Keys.E))
        {
            _sceneManager.AddScene(new Space(_graphics, _sceneManager, _contentManager), "space");
            _sceneManager.AddScene(new Station(_graphics, _sceneManager, _contentManager), "station");
            _sceneManager.AddScene(new PostStation(_graphics, _sceneManager, _contentManager), "poststation");
            _sceneManager.AddScene(new EndFuel(_graphics, _sceneManager, _contentManager), "end-fuel");
            _sceneManager.AddScene(new EndMaffia(_graphics, _sceneManager, _contentManager), "end-maffia");
            _sceneManager.ChangeScene("end-fuel");
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int Width = _graphics.Viewport.Width;
        int Height = _graphics.Viewport.Height;

        _graphics.Clear(Color.Black);

        spriteBatch.Draw(_paper, new Rectangle((Width / 2) - 300, (Height / 2) - 350, 600, 700), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

        spriteBatch.DrawString(_pixelfont, "Hello Michael,\nWe accepted your work\napplication Your first\nworkday is Monday at 8:00 AM.\n\nDon't be late.\n\nDonald Mayfield\nSpaceCargo - HR", new Vector2(Width / 2 - 250, (Height / 2) - 300), Color.Black, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
        
        Vector2 NextM = _pixelfont.MeasureString("Press E button") * 0.75f;
        spriteBatch.DrawString(_pixelfont, "Press E button", new Vector2((Width / 2) - (NextM.X / 2), ((Height / 4) * 3) - (NextM.Y / 2)), Color.Black, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
    }
}
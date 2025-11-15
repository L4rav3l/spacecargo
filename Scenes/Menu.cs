using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SpaceCargo;

public class Menu : IScene
{
    private GraphicsDevice _graphics;
    private SceneManager _sceneManager;
    private ContentManager _contentManager;

    private int _selected = 0;
    private bool _blinking = true;
    private double _blinkingCooldown = 0;

    private KeyboardState _previous;

    private SpriteFont _pixelfont;

    public Menu(GraphicsDevice _graphics, SceneManager _sceneManager, ContentManager _contentManager)
    {
        this._graphics = _graphics;
        this._sceneManager = _sceneManager;
        this._contentManager = _contentManager;
    }

    public void LoadContent()
    {
        _pixelfont = _contentManager.Load<SpriteFont>("pixelfont");
    }

    public void Update(GameTime gameTime)
    {
        double elapsed = gameTime.ElapsedGameTime.TotalSeconds * 1000;

        KeyboardState state = Keyboard.GetState();

        if(_blinkingCooldown > 0)
        {
            _blinkingCooldown -= elapsed;
        } else {
            _blinking = !_blinking;
            _blinkingCooldown = 300;
        }

        if(state.IsKeyDown(Keys.Enter) && !_previous.IsKeyDown(Keys.Enter))
        {
            if(_selected == 1)
            {
                GameData.Quit = true;
            }
        }

        if((state.IsKeyDown(Keys.Up) && !_previous.IsKeyDown(Keys.Up)) || (state.IsKeyDown(Keys.Down) && !_previous.IsKeyDown(Keys.Down)))
        {
            if(_selected == 0)
            {
                _selected = 1;
            } else {
                _selected = 0;
            }
        }

        _previous = state;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int Width = _graphics.Viewport.Width;
        int Height = _graphics.Viewport.Height;

        string playText = "";
        string quitText = "";

        if(_selected == 0)
        {
            if(_blinking == true)
            {
                playText = "Play";
            } else {
                playText = "> Play <";
            }

            quitText = "Quit";

        } else if(_selected == 1)
        {
            if(_blinking == true)
            {
                quitText = "Quit";
            } else {
                quitText = "> Quit <";
            }

            playText = "Play";
        }

        _graphics.Clear(Color.Gray);

        Vector2 MenuM = _pixelfont.MeasureString("Space Cargo");
        Vector2 Menu = new Vector2((Width / 2) - (MenuM.X / 2), (Height / 4) - (MenuM.Y / 2));

        spriteBatch.DrawString(_pixelfont, "Space Cargo", Menu, Color.White);

        Vector2 PlayM = _pixelfont.MeasureString(playText);
        Vector2 Play = new Vector2((Width / 2) - (PlayM.X / 2), (Height / 4) - (PlayM.Y / 2) + 100);

        spriteBatch.DrawString(_pixelfont, playText, Play, Color.White);

        Vector2 QuitM = _pixelfont.MeasureString(quitText);
        Vector2 Quit = new Vector2((Width / 2) - (QuitM.X / 2), (Height / 4) - (QuitM.Y / 2) + 150);

        spriteBatch.DrawString(_pixelfont, quitText, Quit, Color.White);

    }
}
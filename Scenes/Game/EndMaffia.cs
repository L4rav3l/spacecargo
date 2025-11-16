using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SpaceCargo;

public class EndMaffia : IScene
{
    private GraphicsDevice _graphics;
    private SceneManager _sceneManager;
    private ContentManager _contentManager;

    private SpriteFont _pixelfont;
    private Texture2D _paper;
    private Song _bomb;

    private bool _end = false;

    public EndMaffia(GraphicsDevice _graphics, SceneManager _sceneManager, ContentManager _contentManager)
    {
        this._graphics = _graphics;
        this._sceneManager = _sceneManager;
        this._contentManager = _contentManager;
    }

    public void LoadContent()
    {
        _pixelfont = _contentManager.Load<SpriteFont>("pixelfont");
        _bomb = _contentManager.Load<Song>("explosion");

        _paper = new Texture2D(_graphics, 1, 1);
        _paper.SetData(new [] {Color.White});
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        if(state.IsKeyDown(Keys.E))
        {
            _sceneManager.ChangeScene("menu");
        }

        if(!_end)
        {
            _end = true;
            MediaPlayer.Play(_bomb);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int Width = _graphics.Viewport.Width;
        int Height = _graphics.Viewport.Height;

        _graphics.Clear(Color.Black);

        spriteBatch.Draw(_paper, new Rectangle((Width / 2) - 300, (Height / 2) - 350, 600, 700), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        spriteBatch.DrawString(_pixelfont, "The Laky's Mafia sent this\npackage, which contained\na pipe bomb. The pipe bomb\nexploded instantly when you\ndelivered it. You are dead.\nEnd 1.", new Vector2((Width / 2) - 250, (Height / 2) - 300), Color.Black, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
    
        Vector2 NextM = _pixelfont.MeasureString("Press E button") * 0.75f;
        spriteBatch.DrawString(_pixelfont, "Press E button", new Vector2((Width / 2) - (NextM.X / 2), ((Height / 4) * 3) - (NextM.Y / 2)), Color.Black, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
    }
}
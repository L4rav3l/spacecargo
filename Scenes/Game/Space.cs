using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using TiledSharp;
using System;

namespace SpaceCargo;

public class Space : IScene
{
    private GraphicsDevice _graphics;
    private SceneManager _sceneManager;
    private ContentManager _contentManager;

    private Camera2D camera;
    private Player player;
    
    private Texture2D TileSet;
    private Texture2D playerTexture;
    private Texture2D _keypanel;
    private SpriteFont _pixelfont;

    private bool _panel = false;
    private int _code = 0;
    private double _cooldown;

    private TmxMap map;
    private List<Rectangle> solidTiles;

    public List<Rectangle> LoadCollisionObjects(string mapFilePath)
    {
        var map = new TmxMap(mapFilePath);
        var solidTiles = new List<Rectangle>();

        foreach (var objectGroup in map.ObjectGroups)
        {
            if(objectGroup.Name == "Collision")
            {
                foreach(var obj in objectGroup.Objects)
                {
                    if(obj.Width > 0 && obj.Height > 0)
                    {
                        var rect = new Rectangle(
                            (int)obj.X,
                            (int)obj.Y,
                            (int)obj.Width,
                            (int)obj.Height
                        );

                        solidTiles.Add(rect);
                    }
                }
            }
        }

        return solidTiles;
    }

    public Space(GraphicsDevice _graphics, SceneManager _sceneManager, ContentManager _contentManager)
    {
        this._graphics = _graphics;
        this._sceneManager = _sceneManager;
        this._contentManager = _contentManager;
    }

    public void LoadContent()
    {
        player = new Player(new Vector2(2270, 2140));
        camera = new Camera2D(_graphics.Viewport);

        map = new TmxMap("Content/space.tmx");

        TileSet = _contentManager.Load<Texture2D>("tilesmap");
        playerTexture = _contentManager.Load<Texture2D>("playertexture");

        solidTiles = LoadCollisionObjects("Content/space.tmx");
        _pixelfont = _contentManager.Load<SpriteFont>("pixelfont");

        _keypanel = new Texture2D(_graphics, 1, 1);
        _keypanel.SetData(new [] {Color.White});
    }

    public void Update(GameTime gameTime)
    {   
        player.Update(gameTime, solidTiles, camera);
        camera.Follow(player.Position, new Vector2(map.Width * 64, map.Height * 64));

        double elapsed = gameTime.ElapsedGameTime.TotalSeconds * 1000;

        if(_cooldown >= 0)
        {
            _cooldown -= elapsed;
        }

        KeyboardState state = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();

        if(_panel == true)
        {
            if(_code.ToString().Length < 4 && _cooldown <= 0)
            {
                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(840, 439)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 1;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(954, 439)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 2;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(1064, 439)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 3;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(840, 546)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 4;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(954, 546)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 5;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(1064, 546)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 6;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(840, 653)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 7;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(954, 653)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 8;
                    _cooldown = 350;
                }

                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(1064, 653)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _code += 9;
                    _cooldown = 350;
                }
                
                if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(954, 765)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    _code *= 10;
                    _cooldown = 350;
                }
            }
            
            if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(840, 765)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
            {
                _code /= 10;
                _cooldown = 350;
            }

            if(Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), new Vector2(1064, 765)) < 50 && mouseState.LeftButton == ButtonState.Pressed)
            {
                if(GameData.Station == "A")
                {
                    if(GameData.Code[0] == _code)
                    {
                        _sceneManager.ChangeScene("station");
                        GameData.Move = true;
                    }

                } else if(GameData.Station == "B")
                {
                    if(GameData.Code[1] == _code)
                    {
                        _sceneManager.ChangeScene("station");
                        GameData.Move = true;
                    }

                } else if(GameData.Station == "C")
                {
                    if(GameData.Code[2] == _code)
                    {
                        _sceneManager.ChangeScene("station");
                        GameData.Move = true;
                    }

                } else if(GameData.Station == "D")
                {
                    if(GameData.Code[3] == _code)
                    {
                        _sceneManager.ChangeScene("station");
                        GameData.Move = true;
                    }
                }

                _cooldown = 350;
            }
        }

        if(state.IsKeyDown(Keys.E))
        {
            if(_panel == false)
            {
                if(Vector2.Distance(player.Position, new Vector2(542, 3484)) <= 64)
                {
                    GameData.Station = "A";
                    GameData.Move = false;
                    _panel = true;
                }

                if(Vector2.Distance(player.Position, new Vector2(734, 1309)) <= 64)
                {
                    GameData.Station = "B";
                    GameData.Move = false;
                    _panel = true;
                }

                if(Vector2.Distance(player.Position, new Vector2(3550, 733)) <= 64)
                {
                    GameData.Station = "C";
                    GameData.Move = false;
                    _panel = true;
                }

                if(Vector2.Distance(player.Position, new Vector2(3230, 3933)) <= 64)
                {
                    GameData.Station = "D";
                    GameData.Move = false;
                    _panel = true;
                }

                if(Vector2.Distance(player.Position, new Vector2(2270, 2141)) <= 64)
                {
                    GameData.BackStation = true;
                    GameData.InSpace = false;
                    _sceneManager.ChangeScene("poststation");
                }
            }
        }

        if(state.IsKeyDown(Keys.Escape) && _panel == true)
        {
            _panel = false;
            GameData.Move = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphics.Clear(Color.Black);

        int Width = _graphics.Viewport.Width;
        int Height = _graphics.Viewport.Height;
        
        int mapWidth = map.Width;
        int mapHeight = map.Height;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                int i = y * mapWidth + x;
                if (i >= map.Layers[0].Tiles.Count) continue;

                var tile = map.Layers[0].Tiles[i];
                if (tile.Gid == 0) continue;

                int tilesPerRow = TileSet.Width / map.TileWidth;
                int tileIndex = tile.Gid - 1;

                int tileIndexX = tileIndex % tilesPerRow;
                int tileIndexY = tileIndex / tilesPerRow;

                Rectangle source = new Rectangle(
                    tileIndexX * map.TileWidth,
                    tileIndexY * map.TileHeight,
                    map.TileWidth,
                    map.TileHeight
                );

                Vector2 worldPosition = new Vector2(x * map.TileWidth, y * map.TileHeight);
                Vector2 screenPosition = camera.WorldToScreen(worldPosition);

                spriteBatch.Draw(TileSet, screenPosition, source, Color.White);
            }
        }

        player.Draw(spriteBatch, playerTexture, camera);

        if(GameData.Package == true)
        {
            Vector2 ObjectM = _pixelfont.MeasureString($"{GameData.Base} Station, {GameData.Door} Door") * 0.75f;

            spriteBatch.DrawString(_pixelfont, $"{GameData.Base} Station, {GameData.Door} Door", new Vector2((Width / 2) - (ObjectM.X / 2), (ObjectM.Y / 2) + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
        }

        spriteBatch.DrawString(_pixelfont, $"{Math.Floor(GameData.Fuel)}% Fuel", new Vector2(100, 50), Color.White);

        if(_panel == true)
        {
            spriteBatch.Draw(_keypanel, new Rectangle((Width / 2) - 200, (Height / 2) - 300, 400, 600), null, Color.Gray, 0f, Vector2.Zero, SpriteEffects.None, 0.3f);
            spriteBatch.Draw(_keypanel, new Rectangle((Width / 2) - 160, (Height / 2) - 275, 320, 100), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);
            spriteBatch.DrawString(_pixelfont, _code.ToString(), new Vector2((Width / 2) - 40, (Height / 2) - 235), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

            int num = 0;

            for(int y = 0; y < 4; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    spriteBatch.Draw(_keypanel, new Rectangle(((Width / 2 - 160) + (110 * x)), ((Height / 2 - 150) + (110 * y)), 100, 100), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
                    
                    num++;

                    if(num == 12)
                    {
                        spriteBatch.DrawString(_pixelfont, "OK", new Vector2((Width / 2 - 160) + (110 * x) + 17, (Height / 2 - 150) + (110 * y) + 25), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    } else if(num == 10)
                    {
                        spriteBatch.DrawString(_pixelfont, "DEL", new Vector2((Width / 2 - 160) + (110 * x) + 9, (Height / 2 - 150) + (110 * y) + 25), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    } else if(num == 11)
                    {
                        spriteBatch.DrawString(_pixelfont, "0", new Vector2((Width / 2 - 160) + (110 * x) + 35, (Height / 2 - 150) + (110 * y) + 25), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    } else {
                        spriteBatch.DrawString(_pixelfont, num.ToString(), new Vector2((Width / 2 - 160) + (110 * x) + 35, (Height / 2 - 150) + (110 * y) + 25), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    }

                }
            }
        }
    }
}
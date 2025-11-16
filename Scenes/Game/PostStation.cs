using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using TiledSharp;
using System.Collections.Generic;
using System;

namespace SpaceCargo;

public class PostStation : IScene
{
    private GraphicsDevice _graphics;
    private SceneManager _sceneManager;
    private ContentManager _contentManager;

    private Camera2D camera;
    private Player player;

    private Texture2D TileSet;
    private Texture2D playertexture2;
    private SpriteFont _pixelfont;

    private TmxMap map;
    private List<Rectangle> solidTiles;

    private Random random;

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

    public PostStation(GraphicsDevice _graphics, SceneManager _sceneManager, ContentManager _contentManager)
    {
        this._graphics = _graphics;
        this._sceneManager = _sceneManager;
        this._contentManager = _contentManager;
    }

    public void LoadContent()
    {
        player = new Player(new Vector2(1372, 1499));
        camera = new Camera2D(_graphics.Viewport);

        map = new TmxMap("Content/poststation.tmx");

        TileSet = _contentManager.Load<Texture2D>("tilesmap");
        playertexture2 = _contentManager.Load<Texture2D>("playertexture2");
        _pixelfont = _contentManager.Load<SpriteFont>("pixelfont");

        solidTiles = LoadCollisionObjects("Content/poststation.tmx");

        random = new Random();
    }

    public void Update(GameTime gameTime)
    {
        player.Update(gameTime, solidTiles, camera);
        camera.Follow(player.Position, new Vector2(map.Width * 64, map.Height * 64));

        if(GameData.BackStation == true)
        {
            player.Position = new Vector2(1372, 1499);
            GameData.BackStation = false;
        }

        KeyboardState state = Keyboard.GetState();
        
        if(Vector2.Distance(player.Position, new Vector2(1631, 1316)) < 64 && state.IsKeyDown(Keys.E) && GameData.Package == false)
        {
            int station = random.Next(1000,4999) / 1000;
            int door = random.Next(6000,9999) / 1000;

            if(station == 0)
            {
                GameData.Base = "A";
            } else if(station == 1)
            {
                GameData.Base = "B";
            } else if(station == 2)
            {
                GameData.Base = "C";
            } else {
                GameData.Base = "D";
            }
            
            if(door == 6)
            {
                GameData.Door = "A";
            } else if(door == 7)
            {
                GameData.Door = "B";
            } else if(door == 8)
            {
                GameData.Door = "C";
            } else {
                GameData.Door = "D";
            }

            GameData.Package = true;
        }

        if(Vector2.Distance(player.Position, new Vector2(1248, 1501)) < 64)
        {
            GameData.InSpace = true;
            GameData.Fuel = 100;
            _sceneManager.ChangeScene("space");
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int mapWidth = map.Width;
        int mapHeight = map.Height;

        int Width = _graphics.Viewport.Width;
        int Height = _graphics.Viewport.Height;

        _graphics.Clear(Color.Black);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
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

        if(GameData.Package == true)
        {
            Vector2 ObjectM = _pixelfont.MeasureString($"{GameData.Base} Station, {GameData.Door} Door") * 0.75f;

            spriteBatch.DrawString(_pixelfont, $"{GameData.Base} Station, {GameData.Door} Door", new Vector2((Width / 2) - (ObjectM.X / 2), (ObjectM.Y / 2) + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.2f);
        }

        player.Draw(spriteBatch, playertexture2, camera);
    }
}
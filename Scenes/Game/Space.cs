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
    private SpriteFont _pixelfont;

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
    }

    public void Update(GameTime gameTime)
    {   
        player.Update(gameTime, solidTiles, camera);
        camera.Follow(player.Position, new Vector2(map.Width * 64, map.Height * 64));

        KeyboardState state = Keyboard.GetState();

        if(state.IsKeyDown(Keys.E))
        {
            if(Vector2.Distance(player.Position, new Vector2(542, 3484)) <= 64)
            {
                GameData.Station = "A";
                GameData.BackStation = true;
                GameData.InSpace = false;
                _sceneManager.ChangeScene("station");
            }

            if(Vector2.Distance(player.Position, new Vector2(734, 1309)) <= 64)
            {
                GameData.Station = "B";
                GameData.BackStation = true;
                GameData.InSpace = false;
                _sceneManager.ChangeScene("station");
            }

            if(Vector2.Distance(player.Position, new Vector2(3550, 733)) <= 64)
            {
                GameData.Station = "C";
                GameData.BackStation = true;
                GameData.InSpace = false;
                _sceneManager.ChangeScene("station");
            }

            if(Vector2.Distance(player.Position, new Vector2(3230, 3933)) <= 64)
            {
                GameData.Station = "D";
                GameData.BackStation = true;
                GameData.InSpace = false;
                _sceneManager.ChangeScene("station");
            }

            if(Vector2.Distance(player.Position, new Vector2(2270, 2141)) <= 64)
            {
                GameData.BackStation = true;
                GameData.InSpace = false;
                _sceneManager.ChangeScene("poststation");
            }
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

    }
}
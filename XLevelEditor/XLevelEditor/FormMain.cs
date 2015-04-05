using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GDIBitmap = System.Drawing.Bitmap;
using GDIColor = System.Drawing.Color;
using GDIImage = System.Drawing.Image;
using GDIGraphics = System.Drawing.Graphics;
using GDIGraphicsUnit = System.Drawing.GraphicsUnit;
using GDIRectangle = System.Drawing.Rectangle;

using RpgLibrary.WorldClasses;

using XRpgLibrary.TileEngine;

namespace XLevelEditor
{
    public partial class FormMain : Form
    {
        #region Field Region
        SpriteBatch spriteBatch;
        LevelData levelData;
        TileMap map;

        List<Tileset> tileSets = new List<Tileset>();
        List<MapLayer> layers = new List<MapLayer>();
        List<GDIImage> tileSetImages = new List<GDIImage>();
        List<TilesetData> tileSetData = new List<TilesetData>();

        Camera camera;
        Engine engine;

        Point mouse = new Point();
        bool isMouseDown = false;
        bool trackMouse = false;

        Texture2D cursor;
        Texture2D grid;
        #endregion

        #region Property Region
        public GraphicsDevice GraphicsDevice
        {
            get { return mapDisplay.GraphicsDevice; }
        }
        #endregion

        #region Constructor Region
        public FormMain()
        {
            InitializeComponent();

            this.Load += new EventHandler(FormMain_Load);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);

            tilesetToolStripMenuItem.Enabled = false;
            mapLayerToolStripMenuItem.Enabled = false;
            charactersToolStripMenuItem.Enabled = false;
            chestsToolStripMenuItem.Enabled = false;
            keysToolStripMenuItem.Enabled = false;

            newLevelToolStripMenuItem.Click += new EventHandler(newLevelToolStripMenuItem_Click);
            newTilesetToolStripMenuItem.Click += new EventHandler(newTilesetToolStripMenuItem_Click);
            newLayerToolStripMenuItem.Click += new EventHandler(newLayerToolStripMenuItem_Click);
            
            saveLevelToolStripMenuItem.Click += new EventHandler(saveLevelToolStripMenuItem_Click);
            openLevelToolStripMenuItem.Click += new EventHandler(openLevelToolStripMenuItem_Click);

            mapDisplay.OnInitialize += new EventHandler(mapDisplay_OnInitialize);
            mapDisplay.OnDraw += new EventHandler(mapDisplay_OnDraw);
        }
        #endregion

        #region Save Menu Item Event Handler Region
        void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "Level Files (*.xml)|*.xml";
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            DialogResult result = ofDialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            string path = Path.GetDirectoryName(ofDialog.FileName);
            LevelData newLevel = null;
            MapData mapData = null;

            try
            {
                newLevel = XnaSerializer.Deserialize<LevelData>(ofDialog.FileName);
                mapData = XnaSerializer.Deserialize<MapData>(path + @"\Maps\" + newLevel.MapName +
                ".xml");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error reading level");
                return;
            }

            tileSetImages.Clear();
            tileSetData.Clear();
            tileSets.Clear();
            layers.Clear();
            lbTileset.Items.Clear();
            clbLayers.Items.Clear();

            foreach (TilesetData data in mapData.Tilesets)
            {
                Texture2D texture = null;
                tileSetData.Add(data);
                lbTileset.Items.Add(data.TilesetName);
                GDIImage image = (GDIImage)GDIBitmap.FromFile(data.TilesetImageName);
                tileSetImages.Add(image);
                using (Stream stream = new FileStream(data.TilesetImageName, FileMode.Open,
                FileAccess.Read))
                {
                    texture = Texture2D.FromStream(GraphicsDevice, stream);
                    tileSets.Add(
                        new Tileset(
                            texture,
                            data.TilesWide,
                            data.TilesHigh,
                            data.TileWidthInPixels,
                            data.TileHeightInPixels));
                }
            }

            foreach (MapLayerData data in mapData.Layers)
            {
                clbLayers.Items.Add(data.MapLayerName, true);
                layers.Add(MapLayer.FromMapLayerData(data));
            }

            lbTileset.SelectedIndex = 0;
            clbLayers.SelectedIndex = 0;
            nudCurrentTile.Value = 0;

            map = new TileMap(tileSets, layers);
            tilesetToolStripMenuItem.Enabled = true;
            mapLayerToolStripMenuItem.Enabled = true;
            charactersToolStripMenuItem.Enabled = true;
            chestsToolStripMenuItem.Enabled = true;
            keysToolStripMenuItem.Enabled = true;
        }

        void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (map == null)
                return;

            List<MapLayerData> mapLayerData = new List<MapLayerData>();
            for (int i = 0; i < clbLayers.Items.Count; i++)
            {
                MapLayerData data = new MapLayerData(
                    clbLayers.Items[i].ToString(),
                    layers[i].Width,
                    layers[i].Height);
                for (int y = 0; y < layers[i].Height; y++)
                    for (int x = 0; x < layers[i].Width; x++)
                        data.SetTile(
                            x,
                            y,
                            layers[i].GetTile(x, y).TileIndex,
                            layers[i].GetTile(x, y).Tileset);
                mapLayerData.Add(data);
            }

            MapData mapData = new MapData(levelData.MapName, tileSetData, mapLayerData);
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.Description = "Select Game Folder";
            fbDialog.SelectedPath = Application.StartupPath;

            DialogResult result = fbDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!File.Exists(fbDialog.SelectedPath + @"\Game.xml"))
                {
                    MessageBox.Show("Game not found", "Error");
                    return;
                }

                string LevelPath = Path.Combine(fbDialog.SelectedPath, @"Levels\");
                string MapPath = Path.Combine(LevelPath, @"Maps\");

                if (!Directory.Exists(LevelPath))
                    Directory.CreateDirectory(LevelPath);
                if (!Directory.Exists(MapPath))
                    Directory.CreateDirectory(MapPath);

                XnaSerializer.Serialize<LevelData>(LevelPath + levelData.LevelName + ".xml", levelData);
                XnaSerializer.Serialize<MapData>(MapPath + mapData.MapName + ".xml", mapData);
            }
        }
        #endregion

        #region Form Event Handler Region
        void FormMain_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
            lbTileset.SelectedIndexChanged += new EventHandler(lbTileset_SelectedIndexChanged);
            nudCurrentTile.ValueChanged += new EventHandler(nudCurrentTile_ValueChanged);

            Rectangle viewPort = new Rectangle(0, 0, mapDisplay.Width, mapDisplay.Height);
            camera = new Camera(viewPort);
            engine = new Engine(32, 32);

            controlTimer.Tick += new EventHandler(controlTimer_Tick);
            controlTimer.Enabled = true;
            controlTimer.Interval = 200;

            tbMapLocation.TextAlign = HorizontalAlignment.Center;
            pbTilesetPreview.MouseDown += new MouseEventHandler(pbTilesetPreview_MouseDown);

            mapDisplay.SizeChanged += new EventHandler(mapDisplay_SizeChanged);
        }

        void controlTimer_Tick(object sender, EventArgs e)
        {
            mapDisplay.Invalidate();
            Logic();
        }

        void mapDisplay_SizeChanged(object sender, EventArgs e)
        {
            Rectangle viewPort = new Rectangle(0, 0, mapDisplay.Width, mapDisplay.Height);
            Vector2 cameraPosition = camera.Position;
            camera = new Camera(viewPort, cameraPosition);
            camera.LockCamera();
            mapDisplay.Invalidate();
        }

        void pbTilesetPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbTileset.Items.Count == 0)
                return;

            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            int index = lbTileset.SelectedIndex;
            float xScale = (float)tileSetImages[index].Width / pbTilesetPreview.Width;

            float yScale = (float)tileSetImages[index].Height / pbTilesetPreview.Height;

            Point previewPoint = new Point(e.X, e.Y);
            Point tilesetPoint = new Point(
                (int)(previewPoint.X * xScale),
                (int)(previewPoint.Y * yScale));
            Point tile = new Point(
                tilesetPoint.X / tileSets[index].TileWidth,
                tilesetPoint.Y / tileSets[index].TileHeight);
            nudCurrentTile.Value = tile.Y * tileSets[index].TilesWide + tile.X;
        }

        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        void Application_Idle(object sender, EventArgs e)
        {
            mapDisplay.Invalidate();
        }
        #endregion

        #region Tile Tab Event Handler Region
        void lbTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTileset.SelectedItem != null)
            {
                nudCurrentTile.Value = 0;
                nudCurrentTile.Maximum = tileSets[lbTileset.SelectedIndex].SourceRectangles.Length - 1;
                FillPreviews();
            }
        }

        private void FillPreviews()
        {
            int selected = lbTileset.SelectedIndex;
            int tile = (int)nudCurrentTile.Value;
            GDIImage preview = (GDIImage)new GDIBitmap(pbTilePreview.Width, pbTilePreview.Height);
            GDIRectangle dest = new GDIRectangle(0, 0, preview.Width, preview.Height);
            GDIRectangle source = new GDIRectangle(
                tileSets[selected].SourceRectangles[tile].X,
                tileSets[selected].SourceRectangles[tile].Y,
                tileSets[selected].SourceRectangles[tile].Width,
                tileSets[selected].SourceRectangles[tile].Height);
            GDIGraphics g = GDIGraphics.FromImage(preview);
            g.DrawImage(tileSetImages[selected], dest, source, GDIGraphicsUnit.Pixel);
            pbTilesetPreview.Image = tileSetImages[selected];
            pbTilePreview.Image = preview;
        }

        void nudCurrentTile_ValueChanged(object sender, EventArgs e)
        {
            if (lbTileset.SelectedItem != null)
            {
                FillPreviews();
            }
        }
        #endregion

        #region New Menu Item Event Handler Region
        void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormNewLevel frmNewLevel = new FormNewLevel())
            {
                frmNewLevel.ShowDialog();
                if (frmNewLevel.OKPressed)
                {
                    levelData = frmNewLevel.LevelData;
                    tilesetToolStripMenuItem.Enabled = true;
                }
            }
        }

        void newTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormNewTileset frmNewTileset = new FormNewTileset())
            {
                frmNewTileset.ShowDialog();
                if (frmNewTileset.OKPressed)
                {
                    TilesetData data = frmNewTileset.TilesetData;
                    try
                    {
                        GDIImage image = (GDIImage)GDIBitmap.FromFile(data.TilesetImageName);
                        tileSetImages.Add(image);
                        Stream stream = new FileStream(data.TilesetImageName, FileMode.Open, FileAccess.Read);
                        Texture2D texture = Texture2D.FromStream(GraphicsDevice, stream);
                        Tileset tileset = new Tileset(
                            texture,
                            data.TilesWide,
                            data.TilesHigh,
                            data.TileWidthInPixels,
                            data.TileHeightInPixels);
                        tileSets.Add(tileset);
                        tileSetData.Add(data);

                        if (map != null)
                            map.AddTileset(tileset);

                        stream.Close();
                        stream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading file.\n" + ex.Message, "Error reading image");
                        return;
                    }

                    lbTileset.Items.Add(data.TilesetName);
                    if (lbTileset.SelectedItem == null)
                        lbTileset.SelectedIndex = 0;
                    mapLayerToolStripMenuItem.Enabled = true;
                }
            }
        }

        void newLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormNewLayer frmNewLayer = new FormNewLayer(levelData.MapWidth, levelData.MapHeight))
            {
                frmNewLayer.ShowDialog();
                if (frmNewLayer.OKPressed)
                {
                    MapLayerData data = frmNewLayer.MapLayerData;
                    if (clbLayers.Items.Contains(data.MapLayerName))
                    {
                        MessageBox.Show("Layer with name " + data.MapLayerName + " exists.", "Existing layer");
                        return;
                    }

                    MapLayer layer = MapLayer.FromMapLayerData(data);
                    clbLayers.Items.Add(data.MapLayerName, true);
                    clbLayers.SelectedIndex = clbLayers.Items.Count - 1;
                    layers.Add(layer);

                    if (map == null)
                        map = new TileMap(tileSets, layers);
                    charactersToolStripMenuItem.Enabled = true;
                    chestsToolStripMenuItem.Enabled = true;
                    keysToolStripMenuItem.Enabled = true;
                }
            }
        }
        #endregion

        #region Map Display Event Handler Region
        void mapDisplay_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapDisplay.MouseEnter += new EventHandler(mapDisplay_MouseEnter);
            mapDisplay.MouseLeave += new EventHandler(mapDisplay_MouseLeave);
            mapDisplay.MouseMove += new MouseEventHandler(mapDisplay_MouseMove);
            mapDisplay.MouseDown += new MouseEventHandler(mapDisplay_MouseDown);
            mapDisplay.MouseUp += new MouseEventHandler(mapDisplay_MouseUp);

            try
            {
                using (Stream stream = new FileStream(@"Content\grid.png", FileMode.Open, FileAccess.Read))
                {
                    grid = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }

                using (Stream stream = new FileStream(@"Content\cursor.png", FileMode.Open, FileAccess.Read))
                {
                    cursor = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error reading images");
                grid = null;
                cursor = null;
            }
        }

        void mapDisplay_OnDraw(object sender, EventArgs e)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Render();
            Logic();
        }

        void mapDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseDown = false;
        }

        void mapDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseDown = true;
        }

        void mapDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X;
            mouse.Y = e.Y;
        }

        void mapDisplay_MouseLeave(object sender, EventArgs e)
        {
            trackMouse = false;
        }

        void mapDisplay_MouseEnter(object sender, EventArgs e)
        {
            trackMouse = true;
        }
        #endregion

        #region Display Rendering and Logic Region
        private void Render()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    null,
                    null,
                    null,
                    camera.Transformation);

                if (clbLayers.GetItemChecked(i))
                    layers[i].Draw(spriteBatch, camera, tileSets);

                spriteBatch.End();
            }

            DrawDisplay();
        }

        private void DrawDisplay()
        {
            if (map == null)
                return;

            Rectangle destination = new Rectangle(
                0,
                0,
                Engine.TileWidth,
                Engine.TileHeight);

            if (displayGridToolStripMenuItem.Checked)
            {
                int maxX = mapDisplay.Width / Engine.TileWidth + 1;
                int maxY = mapDisplay.Height / Engine.TileHeight + 1;

                spriteBatch.Begin();

                for (int y = 0; y < maxY; y++)
                {
                    destination.Y = y * Engine.TileHeight;
                    for (int x = 0; x < maxX; x++)
                    {
                        destination.X = x * Engine.TileWidth;
                        spriteBatch.Draw(grid, destination, Color.White);
                    }
                }

                spriteBatch.End();
            }

            spriteBatch.Begin();

            destination.X = mouse.X;
            destination.Y = mouse.Y;
            spriteBatch.Draw(
                tileSets[lbTileset.SelectedIndex].Texture,
                destination,
                tileSets[lbTileset.SelectedIndex].SourceRectangles[(int)nudCurrentTile.Value], Color.White);
            spriteBatch.Draw(cursor, destination, Color.White);

            spriteBatch.End();
        }

        private void Logic()
        {
            if (layers.Count == 0)
                return;
            Vector2 position = camera.Position;
            if (trackMouse)
            {
                if (mouse.X < Engine.TileWidth)
                    position.X -= Engine.TileWidth;

                if (mouse.X > mapDisplay.Width - Engine.TileWidth)
                    position.X += Engine.TileWidth;

                if (mouse.Y < Engine.TileHeight)
                    position.Y -= Engine.TileHeight;

                if (mouse.Y > mapDisplay.Height - Engine.TileHeight)
                    position.Y += Engine.TileHeight;

                camera.Position = position;
                camera.LockCamera();

                position.X = mouse.X + camera.Position.X;
                position.Y = mouse.Y + camera.Position.Y;
                Point tile = Engine.VectorToCell(position);
                tbMapLocation.Text = "( " + tile.X.ToString() + ", " + tile.Y.ToString() + " )";

                if (isMouseDown)
                {
                    if (rbDraw.Checked)
                    {
                        layers[clbLayers.SelectedIndex].SetTile(
                            tile.X,
                            tile.Y,
                            (int)nudCurrentTile.Value,
                            lbTileset.SelectedIndex);
                    }
                    if (rbErase.Checked)
                    {
                        layers[clbLayers.SelectedIndex].SetTile(
                            tile.X,
                            tile.Y,
                            -1,
                            -1);
                    }
                }
            }
        }
        #endregion
    }
}

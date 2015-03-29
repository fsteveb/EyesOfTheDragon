using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using XRpgLibrary;
using XRpgLibrary.Controls;
using RpgLibrary.SkillClasses;
using System.IO;

namespace EyesOfTheDragon.GameScreens
{
    internal class SkillLabelSet
    {
        internal Label Label;
        internal LinkLabel LinkLabel;
        internal SkillLabelSet(Label label, LinkLabel linkLabel)
        {
            Label = label;
            LinkLabel = linkLabel;
        }
    }

    public class SkillScreen : BaseGameState
    {
        #region Field Region
        int skillPoints;
        int unassignedPoints;
        PictureBox backgroundImage;
        Label pointsRemaining;
        List<SkillLabelSet> skillLabels = new List<SkillLabelSet>();
        Stack<string> undoSkill = new Stack<string>();
        EventHandler linkLabelHandler;
        #endregion

        #region Property Region
        public int SkillPoints
        {
            get { return skillPoints; }
            set
            {
                skillPoints = value;
                unassignedPoints = value;
            }
        }
        #endregion

        #region Constructor Region
        public SkillScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            linkLabelHandler = new EventHandler(addSkillLabel_Selected);
        }
        #endregion

        #region Method Region
        #endregion

        #region Virtual Method region
        #endregion

        #region XNA Method Region
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = GameRef.Content;
            CreateControls(Content);
        }

        private void CreateControls(ContentManager Content)
        {
            backgroundImage = new PictureBox(
            Game.Content.Load<Texture2D>(@"Backgrounds\titlescreen"),
            GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);

            string skillPath = Content.RootDirectory + @"\Game\Skills\";
            string[] skillFiles = Directory.GetFiles(skillPath, "*.xnb");
            for (int i = 0; i < skillFiles.Length; i++)
                skillFiles[i] = @"Game\Skills\" +
                Path.GetFileNameWithoutExtension(skillFiles[i]);

            List<SkillData> skillData = new List<SkillData>();
            Vector2 nextControlPosition = new Vector2(300, 150);
            pointsRemaining = new Label();
            pointsRemaining.Text = "Skill Points: " + unassignedPoints.ToString();
            pointsRemaining.Position = nextControlPosition;
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
            ControlManager.Add(pointsRemaining);

            foreach (string s in skillFiles)
            {
                SkillData data = Content.Load<SkillData>(s);
                Label label = new Label();
                label.Text = data.Name;
                label.Type = data.Name;
                label.Position = nextControlPosition;

                LinkLabel linkLabel = new LinkLabel();
                linkLabel.TabStop = true;
                linkLabel.Text = "+";
                linkLabel.Type = data.Name;
                linkLabel.Position = new Vector2(
                    nextControlPosition.X + 350,
                    nextControlPosition.Y);
                nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
                linkLabel.Selected += addSkillLabel_Selected;

                ControlManager.Add(label);
                ControlManager.Add(linkLabel);
                skillLabels.Add(new SkillLabelSet(label, linkLabel));
            }

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            LinkLabel undoLabel = new LinkLabel();
            undoLabel.Text = "Undo";
            undoLabel.Position = nextControlPosition;
            undoLabel.TabStop = true;
            undoLabel.Selected += new EventHandler(undoLabel_Selected);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
            ControlManager.Add(undoLabel);

            LinkLabel acceptLabel = new LinkLabel();
            acceptLabel.Text = "Accept Changes";
            acceptLabel.Position = nextControlPosition;
            acceptLabel.TabStop = true;
            acceptLabel.Selected += new EventHandler(acceptLabel_Selected);
            ControlManager.Add(acceptLabel);
            ControlManager.NextControl();
        }

        void acceptLabel_Selected(object sender, EventArgs e)
        {
            undoSkill.Clear();
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        void undoLabel_Selected(object sender, EventArgs e)
        {
            if (unassignedPoints == skillPoints)
                return;

            string skillName = undoSkill.Peek();
            undoSkill.Pop();
            unassignedPoints++;

            // Update the skill points for the appropriate skill
            pointsRemaining.Text = "Skill Points: " + unassignedPoints.ToString();
        }

        void addSkillLabel_Selected(object sender, EventArgs e)
        {
            if (unassignedPoints <= 0)
                return;

            string skillName = ((LinkLabel)sender).Type;
            undoSkill.Push(skillName);
            unassignedPoints--;

            // Update the skill points for the appropriate skill
            pointsRemaining.Text = "Skill Points: " + unassignedPoints.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }
        #endregion
    }
}

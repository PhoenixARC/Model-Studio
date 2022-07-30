namespace __Model_Studio.Forms
{
    partial class UnidentifiedModel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnidentifiedModel));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Could not detect model!\r\nwhat model are you trying to import?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "bat.geo",
            "bed.geo",
            "blaze.geo",
            "boat.geo",
            "cat.geo",
            "chicken.geo",
            "cod.geo",
            "creeper.geo",
            "creeper_head.geo",
            "dolphin.geo",
            "dragon.geo",
            "dragon_head.geo",
            "zombie.drowned.geo",
            "enderman.geo",
            "endermite.geo",
            "evoker.geo",
            "ghast.geo",
            "guardian.geo",
            "horse.v2.geo",
            "irongolem.geo",
            "lavaslime.geo",
            "llama.geo",
            "minecart.geo",
            "ocelot.geo",
            "panda.geo",
            "parrot.geo",
            "phantom.geo",
            "pig.geo",
            "pigzombie.geo",
            "polarbear.geo",
            "pufferfish.large.geo",
            "pufferfish.mid.geo",
            "pufferfish.small.geo",
            "rabbit.geo",
            "salmon.geo",
            "turtle.geo",
            "sheep.geo",
            "sheep.sheared.geo",
            "shulker.geo",
            "silverfish.geo",
            "skeleton.geo",
            "skeleton_head.geo",
            "skeleton.stray.geo",
            "skeleton.wither.geo",
            "skeleton_wither_head.geo",
            "slime.geo",
            "slime.armor.geo",
            "snowgolem.geo",
            "spider.geo",
            "squid.geo",
            "stray.armor.geo",
            "trident.geo",
            "tropicalfish_a.geo",
            "tropicalfish_b.geo",
            "vex.geo",
            "villager.geo",
            "villager.witch.geo",
            "vindicator.geo",
            "witherboss.geo",
            "wolf.geo",
            "zombie.geo",
            "zombie_head.geo",
            "zombie.husk.geo",
            "zombie.villager.geo"});
            this.comboBox1.Location = new System.Drawing.Point(38, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(173, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(136, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Import";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UnidentifiedModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 126);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UnidentifiedModel";
            this.Text = "Unidentified Model";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
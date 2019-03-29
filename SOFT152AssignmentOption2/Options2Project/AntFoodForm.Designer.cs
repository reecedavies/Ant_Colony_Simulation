namespace SOFT152Steering
{
    partial class AntFoodForm
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
            this.components = new System.ComponentModel.Container();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.robberNestRadioButton = new System.Windows.Forms.RadioButton();
            this.foodRadioButton = new System.Windows.Forms.RadioButton();
            this.nestRadioButton = new System.Windows.Forms.RadioButton();
            this.inputAntsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.createAntsButton = new System.Windows.Forms.Button();
            this.createRobberAntsButton = new System.Windows.Forms.Button();
            this.inputRobberAntsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.Silver;
            this.drawingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingPanel.Location = new System.Drawing.Point(39, 146);
            this.drawingPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(1322, 740);
            this.drawingPanel.TabIndex = 0;
            this.drawingPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingPanel_MouseClick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(1249, 77);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(112, 35);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Visible = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(724, 77);
            this.stopButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(112, 35);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.robberNestRadioButton);
            this.groupBox1.Controls.Add(this.foodRadioButton);
            this.groupBox1.Controls.Add(this.nestRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(411, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(260, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
            // 
            // robberNestRadioButton
            // 
            this.robberNestRadioButton.AutoSize = true;
            this.robberNestRadioButton.Location = new System.Drawing.Point(102, 31);
            this.robberNestRadioButton.Name = "robberNestRadioButton";
            this.robberNestRadioButton.Size = new System.Drawing.Size(124, 24);
            this.robberNestRadioButton.TabIndex = 2;
            this.robberNestRadioButton.TabStop = true;
            this.robberNestRadioButton.Text = "Robber Nest";
            this.robberNestRadioButton.UseVisualStyleBackColor = true;
            // 
            // foodRadioButton
            // 
            this.foodRadioButton.AutoSize = true;
            this.foodRadioButton.Location = new System.Drawing.Point(10, 60);
            this.foodRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.foodRadioButton.Name = "foodRadioButton";
            this.foodRadioButton.Size = new System.Drawing.Size(71, 24);
            this.foodRadioButton.TabIndex = 1;
            this.foodRadioButton.TabStop = true;
            this.foodRadioButton.Text = "Food";
            this.foodRadioButton.UseVisualStyleBackColor = true;
            // 
            // nestRadioButton
            // 
            this.nestRadioButton.AutoSize = true;
            this.nestRadioButton.Location = new System.Drawing.Point(10, 31);
            this.nestRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nestRadioButton.Name = "nestRadioButton";
            this.nestRadioButton.Size = new System.Drawing.Size(67, 24);
            this.nestRadioButton.TabIndex = 0;
            this.nestRadioButton.TabStop = true;
            this.nestRadioButton.Text = "Nest";
            this.nestRadioButton.UseVisualStyleBackColor = true;
            // 
            // inputAntsTextBox
            // 
            this.inputAntsTextBox.Location = new System.Drawing.Point(39, 37);
            this.inputAntsTextBox.Name = "inputAntsTextBox";
            this.inputAntsTextBox.Size = new System.Drawing.Size(117, 26);
            this.inputAntsTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Quantity";
            // 
            // createAntsButton
            // 
            this.createAntsButton.Location = new System.Drawing.Point(39, 77);
            this.createAntsButton.Name = "createAntsButton";
            this.createAntsButton.Size = new System.Drawing.Size(119, 34);
            this.createAntsButton.TabIndex = 7;
            this.createAntsButton.Text = "Create Ants";
            this.createAntsButton.UseVisualStyleBackColor = true;
            this.createAntsButton.Click += new System.EventHandler(this.createAntsButton_Click);
            // 
            // createRobberAntsButton
            // 
            this.createRobberAntsButton.Location = new System.Drawing.Point(193, 77);
            this.createRobberAntsButton.Name = "createRobberAntsButton";
            this.createRobberAntsButton.Size = new System.Drawing.Size(173, 34);
            this.createRobberAntsButton.TabIndex = 8;
            this.createRobberAntsButton.Text = "Create Robber Ants";
            this.createRobberAntsButton.UseVisualStyleBackColor = true;
            this.createRobberAntsButton.Click += new System.EventHandler(this.createRobberAntsButton_Click);
            // 
            // inputRobberAntsTextBox
            // 
            this.inputRobberAntsTextBox.Location = new System.Drawing.Point(193, 37);
            this.inputRobberAntsTextBox.Name = "inputRobberAntsTextBox";
            this.inputRobberAntsTextBox.Size = new System.Drawing.Size(169, 26);
            this.inputRobberAntsTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Quantity";
            // 
            // AntFoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 920);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputRobberAntsTextBox);
            this.Controls.Add(this.createRobberAntsButton);
            this.Controls.Add(this.createAntsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.inputAntsTextBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.drawingPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AntFoodForm";
            this.Text = "SOFT152 Ant Colony Simulation C# Project ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton foodRadioButton;
        private System.Windows.Forms.RadioButton nestRadioButton;
        private System.Windows.Forms.TextBox inputAntsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createAntsButton;
        private System.Windows.Forms.Button createRobberAntsButton;
        private System.Windows.Forms.TextBox inputRobberAntsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton robberNestRadioButton;
    }
}


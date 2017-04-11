namespace New_MCG
{
    partial class MCG
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
            this.lowerButton = new System.Windows.Forms.Button();
            this.upperButton = new System.Windows.Forms.Button();
            this.schoolButton = new System.Windows.Forms.Button();
            this.lowerTextBox = new System.Windows.Forms.TextBox();
            this.upperTextBox = new System.Windows.Forms.TextBox();
            this.schoolTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.monthBox = new System.Windows.Forms.ComboBox();
            this.dayBox = new System.Windows.Forms.ComboBox();
            this.yearBox = new System.Windows.Forms.ComboBox();
            this.validateGrade = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lowerButton
            // 
            this.lowerButton.Location = new System.Drawing.Point(13, 13);
            this.lowerButton.Name = "lowerButton";
            this.lowerButton.Size = new System.Drawing.Size(75, 23);
            this.lowerButton.TabIndex = 0;
            this.lowerButton.Text = "Lower File";
            this.lowerButton.UseVisualStyleBackColor = true;
            this.lowerButton.Click += new System.EventHandler(this.lowerButton_Click);
            // 
            // upperButton
            // 
            this.upperButton.Location = new System.Drawing.Point(13, 42);
            this.upperButton.Name = "upperButton";
            this.upperButton.Size = new System.Drawing.Size(75, 23);
            this.upperButton.TabIndex = 1;
            this.upperButton.Text = "Upper File";
            this.upperButton.UseVisualStyleBackColor = true;
            this.upperButton.Click += new System.EventHandler(this.upperButton_Click);
            // 
            // schoolButton
            // 
            this.schoolButton.Location = new System.Drawing.Point(13, 71);
            this.schoolButton.Name = "schoolButton";
            this.schoolButton.Size = new System.Drawing.Size(75, 23);
            this.schoolButton.TabIndex = 2;
            this.schoolButton.Text = "School File";
            this.schoolButton.UseVisualStyleBackColor = true;
            this.schoolButton.Click += new System.EventHandler(this.schoolButton_Click);
            // 
            // lowerTextBox
            // 
            this.lowerTextBox.Enabled = false;
            this.lowerTextBox.Location = new System.Drawing.Point(94, 15);
            this.lowerTextBox.Name = "lowerTextBox";
            this.lowerTextBox.Size = new System.Drawing.Size(177, 20);
            this.lowerTextBox.TabIndex = 3;
            this.lowerTextBox.Text = "Please Select Lower Division File...";
            // 
            // upperTextBox
            // 
            this.upperTextBox.Enabled = false;
            this.upperTextBox.Location = new System.Drawing.Point(94, 45);
            this.upperTextBox.Name = "upperTextBox";
            this.upperTextBox.Size = new System.Drawing.Size(177, 20);
            this.upperTextBox.TabIndex = 4;
            this.upperTextBox.Text = "Please Select Upper Division File...";
            // 
            // schoolTextBox
            // 
            this.schoolTextBox.Enabled = false;
            this.schoolTextBox.Location = new System.Drawing.Point(94, 74);
            this.schoolTextBox.Name = "schoolTextBox";
            this.schoolTextBox.Size = new System.Drawing.Size(177, 20);
            this.schoolTextBox.TabIndex = 5;
            this.schoolTextBox.Text = "Please Select Lower Division File...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Month";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Day";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Year";
            // 
            // monthBox
            // 
            this.monthBox.FormattingEnabled = true;
            this.monthBox.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.monthBox.Location = new System.Drawing.Point(13, 113);
            this.monthBox.MaxDropDownItems = 12;
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(117, 21);
            this.monthBox.TabIndex = 9;
            // 
            // dayBox
            // 
            this.dayBox.FormattingEnabled = true;
            this.dayBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.dayBox.Location = new System.Drawing.Point(136, 113);
            this.dayBox.MaxDropDownItems = 31;
            this.dayBox.Name = "dayBox";
            this.dayBox.Size = new System.Drawing.Size(52, 21);
            this.dayBox.TabIndex = 10;
            // 
            // yearBox
            // 
            this.yearBox.FormattingEnabled = true;
            this.yearBox.Items.AddRange(new object[] {
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035",
            "2036",
            "2037",
            "2038",
            "2039",
            "2040",
            "2041",
            "2042",
            "2043",
            "2044",
            "2045",
            "2046",
            "2047",
            "2048",
            "2049",
            "2050"});
            this.yearBox.Location = new System.Drawing.Point(195, 113);
            this.yearBox.MaxDropDownItems = 33;
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(76, 21);
            this.yearBox.TabIndex = 11;
            // 
            // validateGrade
            // 
            this.validateGrade.Location = new System.Drawing.Point(15, 141);
            this.validateGrade.Name = "validateGrade";
            this.validateGrade.Size = new System.Drawing.Size(256, 23);
            this.validateGrade.TabIndex = 12;
            this.validateGrade.Text = "Validate / Grade";
            this.validateGrade.UseVisualStyleBackColor = true;
            this.validateGrade.Click += new System.EventHandler(this.validateGrade_Click);
            // 
            // MCG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 179);
            this.Controls.Add(this.validateGrade);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.dayBox);
            this.Controls.Add(this.monthBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.schoolTextBox);
            this.Controls.Add(this.upperTextBox);
            this.Controls.Add(this.lowerTextBox);
            this.Controls.Add(this.schoolButton);
            this.Controls.Add(this.upperButton);
            this.Controls.Add(this.lowerButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MCG";
            this.Text = "Math Contest Grading Software";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lowerButton;
        private System.Windows.Forms.Button upperButton;
        private System.Windows.Forms.Button schoolButton;
        private System.Windows.Forms.TextBox lowerTextBox;
        private System.Windows.Forms.TextBox upperTextBox;
        private System.Windows.Forms.TextBox schoolTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox monthBox;
        private System.Windows.Forms.ComboBox dayBox;
        private System.Windows.Forms.ComboBox yearBox;
        private System.Windows.Forms.Button validateGrade;
    }
}


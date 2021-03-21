namespace LogicForm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.formula = new System.Windows.Forms.TextBox();
            this.and = new System.Windows.Forms.Button();
            this.or = new System.Windows.Forms.Button();
            this.xor = new System.Windows.Forms.Button();
            this.not = new System.Windows.Forms.Button();
            this.implication = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.A = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Button();
            this.equ = new System.Windows.Forms.Button();
            this.equality = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formula
            // 
            this.formula.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formula.Location = new System.Drawing.Point(10, 46);
            this.formula.Name = "formula";
            this.formula.Size = new System.Drawing.Size(435, 22);
            this.formula.TabIndex = 0;
            // 
            // and
            // 
            this.and.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.and.Location = new System.Drawing.Point(196, 74);
            this.and.Name = "and";
            this.and.Size = new System.Drawing.Size(25, 25);
            this.and.TabIndex = 1;
            this.and.Text = "∧";
            this.and.UseVisualStyleBackColor = false;
            this.and.Click += new System.EventHandler(this.button1_Click);
            // 
            // or
            // 
            this.or.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.or.Location = new System.Drawing.Point(226, 74);
            this.or.Name = "or";
            this.or.Size = new System.Drawing.Size(25, 25);
            this.or.TabIndex = 2;
            this.or.Text = "∨";
            this.or.UseVisualStyleBackColor = false;
            this.or.Click += new System.EventHandler(this.button2_Click);
            // 
            // xor
            // 
            this.xor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.xor.Location = new System.Drawing.Point(257, 74);
            this.xor.Name = "xor";
            this.xor.Size = new System.Drawing.Size(25, 25);
            this.xor.TabIndex = 3;
            this.xor.Text = "⊕";
            this.xor.UseVisualStyleBackColor = false;
            this.xor.Click += new System.EventHandler(this.button3_Click);
            // 
            // not
            // 
            this.not.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.not.Location = new System.Drawing.Point(166, 74);
            this.not.Name = "not";
            this.not.Size = new System.Drawing.Size(25, 25);
            this.not.TabIndex = 4;
            this.not.Text = "¬";
            this.not.UseVisualStyleBackColor = false;
            this.not.Click += new System.EventHandler(this.button4_Click);
            // 
            // implication
            // 
            this.implication.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.implication.Location = new System.Drawing.Point(288, 74);
            this.implication.Name = "implication";
            this.implication.Size = new System.Drawing.Size(25, 25);
            this.implication.TabIndex = 5;
            this.implication.Text = "⇒";
            this.implication.UseVisualStyleBackColor = false;
            this.implication.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(136, 74);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(25, 25);
            this.button6.TabIndex = 10;
            this.button6.Text = "E";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // A
            // 
            this.A.Location = new System.Drawing.Point(15, 74);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(25, 25);
            this.A.TabIndex = 9;
            this.A.Text = "A";
            this.A.UseVisualStyleBackColor = true;
            this.A.Click += new System.EventHandler(this.A_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(105, 74);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(25, 25);
            this.button8.TabIndex = 8;
            this.button8.Text = "D";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(74, 74);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(25, 25);
            this.button9.TabIndex = 7;
            this.button9.Text = "C";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(43, 74);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(25, 25);
            this.button10.TabIndex = 6;
            this.button10.Text = "B";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(382, 74);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(25, 25);
            this.right.TabIndex = 11;
            this.right.Text = "(";
            this.right.UseVisualStyleBackColor = true;
            this.right.Click += new System.EventHandler(this.button7_Click);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(411, 74);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(25, 25);
            this.left.TabIndex = 12;
            this.left.Text = ")";
            this.left.UseVisualStyleBackColor = true;
            this.left.Click += new System.EventHandler(this.button11_Click);
            // 
            // result
            // 
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.result.Location = new System.Drawing.Point(12, 105);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(192, 36);
            this.result.TabIndex = 13;
            this.result.Text = "Сформировать отчет";
            this.result.UseVisualStyleBackColor = true;
            this.result.Click += new System.EventHandler(this.button12_Click);
            // 
            // equ
            // 
            this.equ.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.equ.Location = new System.Drawing.Point(322, 74);
            this.equ.Name = "equ";
            this.equ.Size = new System.Drawing.Size(25, 25);
            this.equ.TabIndex = 14;
            this.equ.Text = "⇿";
            this.equ.UseVisualStyleBackColor = false;
            this.equ.Click += new System.EventHandler(this.button13_Click);
            // 
            // equality
            // 
            this.equality.Location = new System.Drawing.Point(352, 74);
            this.equality.Name = "equality";
            this.equality.Size = new System.Drawing.Size(25, 25);
            this.equality.TabIndex = 16;
            this.equality.Text = "=";
            this.equality.UseVisualStyleBackColor = true;
            this.equality.Click += new System.EventHandler(this.button14_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(457, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Введите формулу";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(37, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Таблица истинности";
            this.label2.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(219, 160);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(226, 300);
            this.textBox2.TabIndex = 20;
            this.textBox2.Visible = false;
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button15.Location = new System.Drawing.Point(210, 105);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(226, 36);
            this.button15.TabIndex = 21;
            this.button15.Text = "Проверить на эквивалентность";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 472);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.equality);
            this.Controls.Add(this.equ);
            this.Controls.Add(this.result);
            this.Controls.Add(this.left);
            this.Controls.Add(this.right);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.A);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.implication);
            this.Controls.Add(this.not);
            this.Controls.Add(this.xor);
            this.Controls.Add(this.or);
            this.Controls.Add(this.and);
            this.Controls.Add(this.formula);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogiCalculator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox formula;
        private System.Windows.Forms.Button and;
        private System.Windows.Forms.Button or;
        private System.Windows.Forms.Button xor;
        private System.Windows.Forms.Button not;
        private System.Windows.Forms.Button implication;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button A;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button result;
        private System.Windows.Forms.Button equ;
        private System.Windows.Forms.Button equality;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button15;
    }
}


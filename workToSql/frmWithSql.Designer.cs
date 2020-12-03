namespace workToSql
{
    partial class frmWithSql
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
            this.label3 = new System.Windows.Forms.Label();
            this.cmbHour = new System.Windows.Forms.ComboBox();
            this.cmbDay = new System.Windows.Forms.ComboBox();
            this.treeFolder = new System.Windows.Forms.TreeView();
            this.lvTube = new System.Windows.Forms.ListView();
            this.lvConsole = new System.Windows.Forms.ListView();
            this.statPGDN = new System.Windows.Forms.StatusStrip();
            this.dtPStart = new System.Windows.Forms.DateTimePicker();
            this.cmbProcudere = new System.Windows.Forms.ComboBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cLBHour = new System.Windows.Forms.CheckedListBox();
            this.clbDay = new System.Windows.Forms.CheckedListBox();
            this.btnEditHour = new System.Windows.Forms.Button();
            this.btnSaveHour = new System.Windows.Forms.Button();
            this.btnSaveDay = new System.Windows.Forms.Button();
            this.btnEditDay = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDeleteHour = new System.Windows.Forms.Button();
            this.btnDeleteDay = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Период";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbHour
            // 
            this.cmbHour.FormattingEnabled = true;
            this.cmbHour.Location = new System.Drawing.Point(12, 503);
            this.cmbHour.Name = "cmbHour";
            this.cmbHour.Size = new System.Drawing.Size(194, 21);
            this.cmbHour.TabIndex = 11;
            this.cmbHour.Text = "нет часовых параметров";
            this.cmbHour.SelectedIndexChanged += new System.EventHandler(this.cmbHour_SelectedIndexChanged);
            this.cmbHour.MouseEnter += new System.EventHandler(this.cmbHour_MouseEnter);
            // 
            // cmbDay
            // 
            this.cmbDay.FormattingEnabled = true;
            this.cmbDay.Location = new System.Drawing.Point(259, 503);
            this.cmbDay.Name = "cmbDay";
            this.cmbDay.Size = new System.Drawing.Size(181, 21);
            this.cmbDay.TabIndex = 12;
            this.cmbDay.Text = "нет суточных параметров";
            this.cmbDay.SelectedIndexChanged += new System.EventHandler(this.cmbDay_SelectedIndexChanged);
            // 
            // treeFolder
            // 
            this.treeFolder.Location = new System.Drawing.Point(12, 15);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.Size = new System.Drawing.Size(215, 128);
            this.treeFolder.TabIndex = 14;
            this.treeFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolder_AfterSelect);
            // 
            // lvTube
            // 
            this.lvTube.Location = new System.Drawing.Point(347, 15);
            this.lvTube.MultiSelect = false;
            this.lvTube.Name = "lvTube";
            this.lvTube.ShowItemToolTips = true;
            this.lvTube.Size = new System.Drawing.Size(159, 128);
            this.lvTube.TabIndex = 15;
            this.lvTube.TileSize = new System.Drawing.Size(168, 15);
            this.lvTube.UseCompatibleStateImageBehavior = false;
            this.lvTube.View = System.Windows.Forms.View.List;
            this.lvTube.SelectedIndexChanged += new System.EventHandler(this.lvTube_SelectedIndexChanged);
            // 
            // lvConsole
            // 
            this.lvConsole.Location = new System.Drawing.Point(12, 530);
            this.lvConsole.Name = "lvConsole";
            this.lvConsole.Size = new System.Drawing.Size(860, 182);
            this.lvConsole.TabIndex = 16;
            this.lvConsole.TileSize = new System.Drawing.Size(584, 15);
            this.lvConsole.UseCompatibleStateImageBehavior = false;
            this.lvConsole.View = System.Windows.Forms.View.List;
            this.lvConsole.SelectedIndexChanged += new System.EventHandler(this.lvConsole_SelectedIndexChanged);
            // 
            // statPGDN
            // 
            this.statPGDN.Location = new System.Drawing.Point(0, 715);
            this.statPGDN.Name = "statPGDN";
            this.statPGDN.Size = new System.Drawing.Size(884, 22);
            this.statPGDN.TabIndex = 17;
            this.statPGDN.Text = "statusStrip1";
            // 
            // dtPStart
            // 
            this.dtPStart.CustomFormat = "\"YYYY mm\"";
            this.dtPStart.Location = new System.Drawing.Point(112, 12);
            this.dtPStart.Name = "dtPStart";
            this.dtPStart.Size = new System.Drawing.Size(164, 20);
            this.dtPStart.TabIndex = 7;
            this.dtPStart.ValueChanged += new System.EventHandler(this.dtStart_ValueChanged);
            this.dtPStart.MouseEnter += new System.EventHandler(this.dtStart_MouseEnter);
            // 
            // cmbProcudere
            // 
            this.cmbProcudere.FormattingEnabled = true;
            this.cmbProcudere.Items.AddRange(new object[] {
            "Суточные из часовых",
            "Часовые из суточных",
            "Замещение часовой информации "});
            this.cmbProcudere.Location = new System.Drawing.Point(479, 503);
            this.cmbProcudere.Name = "cmbProcudere";
            this.cmbProcudere.Size = new System.Drawing.Size(197, 21);
            this.cmbProcudere.TabIndex = 18;
            this.cmbProcudere.Text = "выберите процедуру";
            this.cmbProcudere.SelectedIndexChanged += new System.EventHandler(this.cmbProcudere_SelectedIndexChanged);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(682, 480);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(163, 44);
            this.btnGo.TabIndex = 19;
            this.btnGo.Text = "Запуск процедуры";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Enabled = false;
            this.monthCalendar1.Location = new System.Drawing.Point(264, 274);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 20;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged_1);
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.Location = new System.Drawing.Point(708, 274);
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 23;
            this.monthCalendar2.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar2_DateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Пробелы часовых данных";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(523, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Пробелы суточных данных";
            // 
            // cLBHour
            // 
            this.cLBHour.Enabled = false;
            this.cLBHour.FormattingEnabled = true;
            this.cLBHour.Location = new System.Drawing.Point(12, 274);
            this.cLBHour.Name = "cLBHour";
            this.cLBHour.Size = new System.Drawing.Size(230, 154);
            this.cLBHour.TabIndex = 27;
            // 
            // clbDay
            // 
            this.clbDay.Enabled = false;
            this.clbDay.FormattingEnabled = true;
            this.clbDay.Location = new System.Drawing.Point(440, 274);
            this.clbDay.Name = "clbDay";
            this.clbDay.Size = new System.Drawing.Size(256, 154);
            this.clbDay.TabIndex = 29;
            // 
            // btnEditHour
            // 
            this.btnEditHour.Enabled = false;
            this.btnEditHour.Location = new System.Drawing.Point(12, 438);
            this.btnEditHour.Name = "btnEditHour";
            this.btnEditHour.Size = new System.Drawing.Size(103, 38);
            this.btnEditHour.TabIndex = 32;
            this.btnEditHour.Text = "Редактирование";
            this.btnEditHour.UseVisualStyleBackColor = true;
            this.btnEditHour.Click += new System.EventHandler(this.btnEditHour_Click);
            // 
            // btnSaveHour
            // 
            this.btnSaveHour.Location = new System.Drawing.Point(12, 439);
            this.btnSaveHour.Name = "btnSaveHour";
            this.btnSaveHour.Size = new System.Drawing.Size(103, 37);
            this.btnSaveHour.TabIndex = 33;
            this.btnSaveHour.Text = "Сохранить";
            this.btnSaveHour.UseVisualStyleBackColor = true;
            this.btnSaveHour.Visible = false;
            this.btnSaveHour.Click += new System.EventHandler(this.btnSaveHour_Click);
            // 
            // btnSaveDay
            // 
            this.btnSaveDay.Location = new System.Drawing.Point(440, 439);
            this.btnSaveDay.Name = "btnSaveDay";
            this.btnSaveDay.Size = new System.Drawing.Size(105, 37);
            this.btnSaveDay.TabIndex = 34;
            this.btnSaveDay.Text = "Сохранить";
            this.btnSaveDay.UseVisualStyleBackColor = true;
            this.btnSaveDay.Visible = false;
            this.btnSaveDay.Click += new System.EventHandler(this.btnSaveDay_Click);
            // 
            // btnEditDay
            // 
            this.btnEditDay.Enabled = false;
            this.btnEditDay.Location = new System.Drawing.Point(440, 438);
            this.btnEditDay.Name = "btnEditDay";
            this.btnEditDay.Size = new System.Drawing.Size(105, 38);
            this.btnEditDay.TabIndex = 35;
            this.btnEditDay.Text = "Редактирование";
            this.btnEditDay.UseVisualStyleBackColor = true;
            this.btnEditDay.Click += new System.EventHandler(this.btnEditDay_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Часовые параметры";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Суточные параметры";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.treeFolder);
            this.panel1.Controls.Add(this.lvTube);
            this.panel1.Location = new System.Drawing.Point(15, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 160);
            this.panel1.TabIndex = 38;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(596, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 31);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDeleteHour
            // 
            this.btnDeleteHour.Location = new System.Drawing.Point(121, 439);
            this.btnDeleteHour.Name = "btnDeleteHour";
            this.btnDeleteHour.Size = new System.Drawing.Size(98, 37);
            this.btnDeleteHour.TabIndex = 17;
            this.btnDeleteHour.Text = "Удалить заполненность ";
            this.btnDeleteHour.UseVisualStyleBackColor = true;
            this.btnDeleteHour.Click += new System.EventHandler(this.btnDeleteHour_Click);
            // 
            // btnDeleteDay
            // 
            this.btnDeleteDay.Location = new System.Drawing.Point(567, 439);
            this.btnDeleteDay.Name = "btnDeleteDay";
            this.btnDeleteDay.Size = new System.Drawing.Size(98, 37);
            this.btnDeleteDay.TabIndex = 39;
            this.btnDeleteDay.Text = "Удалить заполненность ";
            this.btnDeleteDay.UseVisualStyleBackColor = true;
            this.btnDeleteDay.Click += new System.EventHandler(this.btnDeleteDay_Click);
            // 
            // frmWithSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 737);
            this.Controls.Add(this.btnDeleteDay);
            this.Controls.Add(this.btnDeleteHour);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnEditDay);
            this.Controls.Add(this.btnSaveDay);
            this.Controls.Add(this.btnSaveHour);
            this.Controls.Add(this.btnEditHour);
            this.Controls.Add(this.clbDay);
            this.Controls.Add(this.cLBHour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monthCalendar2);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.cmbProcudere);
            this.Controls.Add(this.statPGDN);
            this.Controls.Add(this.lvConsole);
            this.Controls.Add(this.cmbDay);
            this.Controls.Add(this.cmbHour);
            this.Controls.Add(this.dtPStart);
            this.Controls.Add(this.label3);
            this.Name = "frmWithSql";
            this.Text = "Анализ заполнености данных";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbHour;
        private System.Windows.Forms.ComboBox cmbDay;
        private System.Windows.Forms.TreeView treeFolder;
        private System.Windows.Forms.ListView lvTube;
        private System.Windows.Forms.ListView lvConsole;
        private System.Windows.Forms.StatusStrip statPGDN;
        private System.Windows.Forms.DateTimePicker dtPStart;
        private System.Windows.Forms.ComboBox cmbProcudere;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox cLBHour;
        private System.Windows.Forms.CheckedListBox clbDay;
        private System.Windows.Forms.Button btnEditHour;
        private System.Windows.Forms.Button btnSaveHour;
        private System.Windows.Forms.Button btnSaveDay;
        private System.Windows.Forms.Button btnEditDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDeleteHour;
        private System.Windows.Forms.Button btnDeleteDay;
    }
}


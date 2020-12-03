using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using System.Linq;

namespace workToSql
{


    public partial class frmWithSql : Form
    {
        public string tubeId = "EC42985F-754A-46ED-B4C1-393903401812";
        string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;

        DateTime dt;
        public string dayFieldName;
        public string hourFieldName;

        public string tubeName = "";
        private readonly GraphClient client;
        private readonly string url;

        public frmWithSql()
        {
            InitializeComponent();
            url = ConfigurationManager.AppSettings["neo4j-url"];
            client = new GraphClient(new Uri(url));
            client.Connect();
            List<Folder> foldersTop = GetFoldersTop();
            statPGDN.Items.Add($"БД-источник: MSSQL {mssqlCS}");
            dayFieldName = "";
            hourFieldName = "";
            dt = new DateTime(dtPStart.Value.Year, dtPStart.Value.Month, 1, 0, 0, 0);

        }
        

 
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //---------------------------------------------------------------------------------
        private void lvTube_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  int dayI = dateMonth.Day;
            btnGo.Enabled = false;
            ListView.SelectedListViewItemCollection item = lvTube.SelectedItems;
            if (item.Count > 0)
            {
                tubeId = lvTube.SelectedItems[0].ImageKey;
                tubeName = lvTube.SelectedItems[0].Text;
                this.Text = "Анализ заполнености данных по объекту: " + treeFolder.SelectedNode.Text + ":" + tubeName;
                //Tube.Items.
                item[0].ToolTipText = "id объекта: '" + lvTube.SelectedItems[0].ImageKey + "' скопирован в буфер обмена";
                Clipboard.SetText("Текст из программы");
                cmbHour.Text = "нет часовых параметров";
                cmbHour.Enabled = false;
                cmbHour.Items.Clear();
                cmbDay.Text = "нет суточных параметров";
                cmbDay.Enabled = false;
                cmbDay.Items.Clear();
                cLBHour.Items.Clear(); //add ilmir
                clbDay.Items.Clear(); // add ilmir
                btnGo.Enabled = false;
                /*---------------------------------------*/
                string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
                var conMssql = new SqlConnection(mssqlCS);


                DayData result = new DayData(); result.hasData = false;
                try
                {
                    using (conMssql)
                    {
                        var cmd = new SqlCommand();
                        cmd.Connection = conMssql;
                        conMssql.Open();
                        //2017 - 08 - 13 07:00:00.0000000
                        //сутки
                        string dayStart = dt.AddMonths(-6).ToString("yyyy-MM-dd HH:mm:ss");
                        string dayEnd = dt.AddMonths(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
                        //string dayEnd = dtEnd.Value.ToString("yyyy-MM-dd hh:mm:ss");

                        string tableName = " DataRecordDay" + dtPStart.Value.ToString("yyyy");
                        var query = $"select distinct(s1) Date from " + tableName + " where  ObjectId='" + tubeId + "' and  Date between '" + dayStart + "' and '" + dayEnd + "'";


                        cmd.CommandText = query;
                        var reader = cmd.ExecuteReader();
                        cmbDay.Items.Clear();

                        while (reader.Read())
                        {
                            result.hasData = true;
                            result.s1 = reader.GetString(0);
                            cmbDay.Items.Add(result.s1);
                            clbDay.Items.Add(result.s1);
                        }
                        reader.Close();

                        if (cmbDay.Items.Count > 0)
                        {
                            cmbDay.Text = string.Format("Выберите параметр(сутки) из {0}", cmbHour.Items.Count);
                            cmbDay.Enabled = true;
                        }

                        //часы
                        tableName = " DataRecordHour" + dtPStart.Value.ToString("MM") + dtPStart.Value.ToString("yyyy");
                        query = $"select distinct(s1) Date from " + tableName + " where ObjectId='" + tubeId + "' and  Date between '" + dayStart + "' and '" + dayEnd + "'";


                        cmd.CommandText = query;
                        reader = cmd.ExecuteReader();
                        cmbHour.Items.Clear();
                        while (reader.Read())
                        {
                            result.hasData = true;
                            result.s1 = reader.GetString(0);
                            cmbHour.Items.Add(result.s1);
                            cLBHour.Items.Add(result.s1);
                        }
                        reader.Close();
                        if (cmbHour.Items.Count > 0)
                        {
                            cmbHour.Enabled = true;
                            cmbHour.Text = string.Format("Выберите параметр(часы) из {0}", cmbDay.Items.Count);
                        }

                    }

                }
                catch (Exception ex)
                {
                    lvConsole.Items.Add($"Ошибка btnSetParam: {ex}");
                    //Console.ResetColor();
                    //break;
                }
                conMssql.Close();
               
                SetCheckParameters();
                btnEditHour.Enabled = true;
                btnEditDay.Enabled = true;
                btnRefresh.Enabled = false;
                clbDay.Enabled = false;
                cLBHour.Enabled = false;
                MultipleSelectionParametersHour();
                MultipleSelectionParametersDay();
                visualisation(tubeId, dtPStart.Value, hourFieldName, dayFieldName, amountTypeHour, true, true);
            }

        }

        public dynamic GetNodeById(Guid id)
        {
            var query = client.Cypher.Match("(d:Device)").
                 Return((d) => d.Node<string>());
            var haa = query.Results;
            return haa;
        }

  


        public dynamic GetFolder_()
        {
            var query = client.Cypher.Match("(f:Folder)").
                 Return(f => f.As<Folder>());
            var folders = query.Results;
            //match(f:Folder)<-[c:contains]-(m:Folder) return m.id
            foreach (var f in folders)
            {
                var id = f.id;
               var query1 = client.Cypher.Match("(ff:Folder)<-[c:contains]-(m:Folder)")
                    .Where("ff.id = {id}").WithParams(new { id = id })
                    //.With("count(ff) as cnt")
                    .Return(cnt => cnt.As<Folder>());

                //var folders_ = query1.Results;



                //if (count == 0)
                treeFolder.Nodes.Add(f.name);
            }
            return folders;
        }


        private void button1_Click(object sender, EventArgs e)
        {
           // GetNodeById(new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"));
           // GetFolder();
            //frm
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Folder> foldersTop = GetFoldersTop();
  
        }

        private dynamic GetFoldersChild()
        {
            var query = client.Cypher.Match("(f:Folder)<--(:Folder)").
                 Return(f => f.As<Folder>());
            var folders = query.Results;
            return folders;
        }

        private dynamic GetFoldersAll()
        {
            var query = client.Cypher.Match("(f:Folder)").
                 Return(f => f.As<Folder>());
            var folders = query.Results.OrderBy(n => n.name);
            return folders;
        }

        private List<Folder> GetFoldersTop()
        {
            List<Folder> foldersTop = new List<Folder>();
            treeFolder.Nodes.Clear();
            IEnumerable<Folder> foldersAll = GetFoldersAll();
            IEnumerable<Folder> foldersChild = GetFoldersChild();

            foreach (var fAll in foldersAll)
            {
                Guid id = fAll.id;
                bool isTop = true;
                foreach (var fChild in foldersChild)
                {
                    if (id == fChild.id)
                    {
                        isTop = false;
                        break;
                    }
                }

                if (isTop)
                {
                    treeFolder.Nodes.Add(fAll.id.ToString(), fAll.name);
                    foldersTop.Add(fAll);
                    treeFolder.SelectedNode = treeFolder.Nodes[fAll.id.ToString()];
                    var folderChildS = searchChild(fAll.id.ToString(), treeFolder.SelectedNode);
                }
                treeFolder.SelectedNode = treeFolder.Nodes[0];
 
            }
            return foldersTop;
        }

        private dynamic searchChild(string id, TreeNode selected)
        {
            var query = client.Cypher.Match("(f:Folder)-->(c:Folder)")
                        .Where("f.id = {id}").WithParams(new { id = id })
                        .Return(c => c.As<Folder>());
            IEnumerable<Folder> folders = query.Results;
            foreach (Folder folderChild in folders)
            {
                selected.Nodes.Add(folderChild.id.ToString(),folderChild.name);
                TreeNode selected_ = selected.Nodes[folderChild.id.ToString()];
                searchChild(folderChild.id.ToString(), selected_);
            }
            return folders;
        }

        private void treeFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string folderId = treeFolder.SelectedNode.Name;
            tubesGet(folderId);
            this.Text = "Анализ заполнености данных по объекту: " + treeFolder.SelectedNode.Text + ": не выбран" ;
            btnEditDay.Enabled = false;
            btnEditHour.Enabled = false;
            btnRefresh.Enabled = false;
            clbDay.Enabled = false;
            cLBHour.Enabled = false;
        }

        private dynamic tubesGet(string id)
        {
            var query = client.Cypher.Match("(f:Folder)-->(a:Area)-->(t:Tube)")
                        .Where("f.id = {id}" ).WithParams(new { id = id })
                        .Return(t => t.As<Tube>());
            IEnumerable<Tube> tubes = query.Results.OrderBy(n=>n.name);
            lvTube.Items.Clear();
            foreach (Tube tube in tubes)
            {
                lvTube.Items.Add(tube.name, tube.id.ToString());
            }
            return tubes;
        }
        

        private void dtStart_ValueChanged(object sender, EventArgs e)
        {
            dt = new DateTime(dtPStart.Value.Year, dtPStart.Value.Month, 1, 0, 0, 0);
            dtPStart.Enabled = false;
            monthCalendar2.Visible=false;
            monthCalendar1.Visible = false;

           
            monthCalendar2.Visible = true;
            monthCalendar1.Visible = true;
            
            dtPStart.Enabled = true;
            monthCalendar2.MinDate = new DateTime(1753,1,1);
            monthCalendar1.MinDate = new DateTime(1753, 1, 1);
            monthCalendar2.MaxDate = DateTime.MaxValue; 
            monthCalendar1.MaxDate = DateTime.MaxValue; 

            monthCalendar2.MinDate = dt;
            monthCalendar1.MinDate = dt;
            monthCalendar2.MaxDate = dt.AddDays(1);
            monthCalendar1.MaxDate = dt.AddDays(1);
        }
        
     
        private void dtStart_MouseEnter(object sender, EventArgs e)
        {
//            dt = new DateTime(dtStart.Value.Year, dtStart.Value.Month, 1, 0, 0, 0);
        }

        private void cmbHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGoEnabled();
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGoEnabled();
         }

        private void btnGoEnabled()
        {
            
            if ((cmbHour.SelectedItem == null) || (cmbDay.SelectedItem == null))
            {
                lvConsole.Items.Add("Выберите часовые и суточные параметры одновременно!");
                return;
            }
                  
            if (cmbHour.SelectedItem.ToString() != "")
            {
                hourFieldName = cmbHour.SelectedItem.ToString();
            }
            if(cmbDay.SelectedItem.ToString() != "")
            {
                dayFieldName = cmbDay.SelectedItem.ToString();
            }

            if ((hourFieldName  != "" ) && (dayFieldName != ""))
            {
                btnGo.Enabled = true;
            }
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }


        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            //start();
            switch (cmbProcudere.SelectedIndex)
            {
                case 0:
                    start();
                    break;
                case 1:
                    hoursFromDay(tubeId, dtPStart.Value, hourFieldName, dayFieldName); 
                    break;
                case 2:
                    //Console.WriteLine("Default case");
                    monthCalendar1.AddBoldedDate(new DateTime(2018,7,7));
                    monthCalendar1.AddMonthlyBoldedDate(new DateTime(2018, 7, 8));
                    monthCalendar1.MaxSelectionCount = 10;
                    monthCalendar1.SelectionStart=new DateTime(2018, 7, 10);
                    monthCalendar1.SelectionEnd = new DateTime(2018, 7, 15);


                    break;
            }

            btnGo.Enabled = true;
            lvConsole.Items.Add($"Работа процедуры завершена");
            btnRefresh.Enabled = true;


        }

        private void cmbProcudere_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGo.Text = "Запуск процедуры:\n\r" + cmbProcudere.Text;
            btnGoEnabled();
        }

        private void cmbHour_MouseEnter(object sender, EventArgs e)
        {
            btnGoEnabled();
        }

        private void monthCalendar1_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            DateTime d = monthCalendar1.SelectionStart;
        }

        private void lvConsole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        public int amountTypeHour = 0;
        private void btnEditHour_Click(object sender, EventArgs e)
        {
            SetTypeHour();
            btnEditHour.Visible = false;
            btnSaveHour.Visible = true;
            cLBHour.Enabled = true;

           
        }
        private void btnSaveHour_Click(object sender, EventArgs e)
        {
            btnSaveHour.Visible = false;
            btnEditHour.Visible = true;
            cLBHour.Enabled = false;

            SetFulledHour();
            MultipleSelectionParametersHour();
            visualisation(tubeId, dtPStart.Value, hourFieldName, dayFieldName, amountTypeHour, true, false);
        }

        private void btnEditDay_Click(object sender, EventArgs e)
        {
            SetTypeDay();
            btnEditDay.Visible = false;
            btnSaveDay.Visible = true;
            clbDay.Enabled = true;
            
        }

        private void btnSaveDay_Click(object sender, EventArgs e)
        {
            btnSaveDay.Visible = false;
            btnEditDay.Visible = true;
            clbDay.Enabled = false;

            SetFulledDay();
            MultipleSelectionParametersDay();
            visualisation(tubeId, dtPStart.Value, hourFieldName, dayFieldName, amountTypeHour, false, true);
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            visualisation(tubeId, dtPStart.Value, hourFieldName, dayFieldName, amountTypeHour, true, true);
            btnRefresh.Enabled = false;
        }
        
        private void btnDeleteHour_Click(object sender, EventArgs e)
        {
            deleteHour();
            btnRefresh.Enabled = true;
        }

        private void btnDeleteDay_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = true;
        }
    }


}


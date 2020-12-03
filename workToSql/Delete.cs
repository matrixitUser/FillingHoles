using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workToSql
{
    public partial class frmWithSql : Form
    {
        public void deleteHour()
        {
            string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            var conMssql = new SqlConnection(mssqlCS);

             try
                {
                    using (conMssql)
                    {
                        var cmd = new SqlCommand();
                        cmd.Connection = conMssql;
                        conMssql.Open();
                    //часы
                    string dayStart = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    string dayEnd = dt.AddMonths(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
                    string tableName = " DataRecordHour" + dtPStart.Value.ToString("MM") + dtPStart.Value.ToString("yyyy");
                        var query = $"delete from " + tableName + " where ObjectId='" + tubeId + "' and  S3= 'calculated' and  Date between '" + dayStart + "' and '" + dayEnd + "'";

                        cmd.CommandText = query;
                        var reader = cmd.ExecuteReader();
                        reader.Close();
                }

                }
                catch (Exception ex)
                {
                    lvConsole.Items.Add($"Ошибка btnDeleteHour: {ex}");
                }
                conMssql.Close();

        }
        public void deleteDay()
        {
            string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            var conMssql = new SqlConnection(mssqlCS);

            try
            {
                using (conMssql)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conMssql;
                    conMssql.Open();
                    //часы
                    string tableName = " DataRecordDay" + dtPStart.Value.ToString("yyyy");
                    var query = $"delete from " + tableName + " where ObjectId='" + tubeId + "' and  S3= 'calculated'";

                    cmd.CommandText = query;
                    var reader = cmd.ExecuteReader();
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                lvConsole.Items.Add($"Ошибка btnDeleteDay: {ex}");
            }
            conMssql.Close();

        }
    }
}

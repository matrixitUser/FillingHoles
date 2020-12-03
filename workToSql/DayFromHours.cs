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

        private DateTime dateClear(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private int searchInRecords(List<DataRecord> records, DateTime date)
        {
            for (int index = 0; index < records.Count; index++)
            {
                if (records[index].date == date) return index;
            }
            return -1;
        }

        private int countRecordsThisMonth(List<DataRecord> records, DateTime date)
        {
            int countRecords = 0;
            for (int index = 0; index < records.Count; index++)
            {
                if ((records[index].date.Month == date.Month) && (records[index].date.Month == date.Month)) countRecords++;
            }
            return countRecords;
        }

        private int countRecordsThisDay(List<DataRecord> records, DateTime date)
        {
            int countRecords = 0;
            for (int index = 0; index < records.Count; index++)
            {
                if ((records[index].date.Month == date.Month) && (records[index].date.Month == date.Month) && (records[index].date.Day == date.Day)) countRecords++;
            }
            return countRecords;
        }

        private DaysData hoursThisDay(List<DataRecord> records, DateTime date)
        {
            DaysData dayData = new DaysData();
            int countRecords = 0;
            double sumd1 = 0;
            for (int index = 0; index < records.Count; index++)
            {
                if ((records[index].date.Month == date.Month) && (records[index].date.Month == date.Month) && (records[index].date.Day == date.Day))
                {
                    sumd1 = sumd1 + records[index].d1;
                    countRecords++;
                }
            }

            dayData.count = countRecords;
            dayData.hoursSum = sumd1;
            return dayData;
        }


        private void start()
        {

            DateTime dtClear = dateClear(dtPStart.Value);

            int day0 = 1;
            int year_ = dtPStart.Value.Year;
            int month_ = dtPStart.Value.Month;
            string s3 = "calculated";

            List<HasDayData> hasDayDataList = new List<HasDayData>();


            DateTime dateI = new DateTime(year_, month_, day0);
            int lastDay = dateI.AddDays(1 - dateI.Day).AddMonths(1).AddDays(-1).Day;
            //*DateTime dateHasData = DateTime.MinValue;  
            //Дата, за которую обнаружились данные
            //Запрос данных за месячный период плюс день до периода и день после периода
            GetData Days = new GetData(tubeId, dtClear.AddDays(-1), dtClear.AddMonths(1), "Day", dayFieldName);
            if (countRecordsThisMonth(Days.records, dtClear) == lastDay)
            {
                lvConsole.Items.Add(lvConsole.Text + ("Дыры по суточным не обнаружены."));
                return; //все данные по месяцу есть
            }
            //Так как есть дыры в суточных сразу делаем выборку часовых данных
            GetData Hours = new GetData(tubeId, dtClear.AddDays(-1), dtClear.AddMonths(1), "Hour", hourFieldName);
            if (Hours.records.Count == 0)
            {
                lvConsole.Items.Add(lvConsole.Text + ("Обнаружены Дыры по суточным. Также нет данных по часовым. Вычисление суточных по часовым невозможно."));
                return; //нет часовых данных
            }

            //по каждому дню
            for (int dayI = 1; dayI <= lastDay; dayI++)
            {
                dateI = new DateTime(year_, month_, dayI);
                lvConsole.Items.Add("");
                lvConsole.Items.Add("Анализ суточных за " + dateI.ToString());

                int index = searchInRecords(Days.records, dateI);
                if (index >= 0) { lvConsole.Items.Add("Cуточные за " + dateI.ToString() + " обнаружены."); continue; } //данные есть
                //если нет:
                //1. ищем, где есть ли показания за предыдущий день
                int indexPrev = searchInRecords(Days.records, dateI.AddDays(-1));
                if (indexPrev >= 0)
                {
                    DaysData hoursData = hoursThisDay(Hours.records, dateI);
                    if (hoursData.count == 24) //есть все 24 часа за этот день
                    {
                        double value = Days.records[indexPrev].d1 + hoursData.hoursSum;
                        DataRecord dataRecord = new DataRecord(tubeId, dateI, value, dayFieldName, Days.records[indexPrev].s2, s3);
                        DayData dayAdd = insertData(dataRecord);
                        //теперь данные за эти сутки есть- дополним Days
                        Days.records.Add(dataRecord);
                    }
                    else
                    {
                        lvConsole.Items.Add(lvConsole.Text + ("Отсуствуют или недостаточно данных по часовым " + dateI.AddDays(-1).ToString())); //todo
                    }
                } //if (!dayData.hasData)
                else
                {
                    lvConsole.Items.Add(lvConsole.Text + ("Суточные показания за предыдущий день не найдены. --------"));
                }
            } // for (int dayI = day0; dayI <= lastDay; dayI++)

            //Анализ результатов прогона вперед
            if (countRecordsThisMonth(Days.records, dtClear) == lastDay)
            {
                lvConsole.Items.Add(lvConsole.Text + ("Дыры по суточным закрыты."));
                return; //все данные по месяцу есть
            }
            //Иначе прогон назад

            //по каждому дню
            for (int dayI = lastDay; dayI > 0; dayI--)
            {
                dateI = new DateTime(year_, month_, dayI);
                lvConsole.Items.Add("");
                lvConsole.Items.Add("Анализ суточных за " + dateI.ToString());

                int index = searchInRecords(Days.records, dateI);
                if (index >= 0) { lvConsole.Items.Add("Cуточных за " + dateI.ToString() + " обнаружены."); continue; } //данные есть
                //если нет:
                //1. ищем, где есть ли показания за последующий день
                int indexNext = searchInRecords(Days.records, dateI.AddDays(+1));
                if (indexNext >= 0)
                {
                    DaysData hoursData = hoursThisDay(Hours.records, dateI.AddDays(+1));
                    if (hoursData.count == 24) //есть все 24 часа за этот день
                    {
                        double value = Days.records[indexNext].d1 - hoursData.hoursSum;
                        DataRecord dataRecord = new DataRecord(tubeId, dateI, value, dayFieldName, Days.records[indexNext].s2, s3);
                        DayData dayAdd = insertData(dataRecord);
                        //теперь данные за эти сутки есть- дополним Days
                        Days.records.Add(dataRecord);

                    }
                    else
                    {
                        lvConsole.Items.Add(lvConsole.Text + ("Отсуствуют или недостаточно данных по часовым " + dateI.AddDays(-1).ToString())); //todo
                    }
                } //if (!dayData.hasData)
                else
                {
                    lvConsole.Items.Add(lvConsole.Text + ("Суточные показания за предыдущий день не найдены. --------"));



                }
            } // for (int dayI = lastDay; dayI > 0 ; dayI--)

            //Анализ результатов прогонов
            int countdaysOk = countRecordsThisMonth(Days.records, dtClear);
            if (countdaysOk == lastDay)
            {
                lvConsole.Items.Add("Дыры по суточным закрыты.");
            }
            else
            {
                lvConsole.Items.Add(string.Format("Дыры по суточным не закрыты. Имеются данные по {0}  суткам из {1}", countdaysOk, lastDay));
            }
            return;


        }


        private DayData insertData(DataRecord dataRecord)
        {
            string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            var conMssql = new SqlConnection(mssqlCS);
            DayData result = new DayData(); result.hasData = false;
            string d1String = string.Format("{0:0.0000}", dataRecord.d1).Replace(",", ".");
            //tubeId = Guid.NewGuid().ToString(); //*
            try
            {
                using (conMssql)
                {
                    var cmd = new SqlCommand();

                    cmd.Connection = conMssql;
                    conMssql.Open();

                    string daysTableName = string.Format("DataRecordDay{0:0000}", dataRecord.date.Year);  // суточные данные
                    var query = $"INSERT INTO " + daysTableName +
                            " (ID,DT1,S1,D1,S2,S3,OBJECTID,DATE,TYPE)  VALUES (NEWID(), GETDATE (),'" + dataRecord.s1 + "','" + d1String + "','" + dataRecord.s2 + "','" + dataRecord.s3 + "','" + tubeId + "','" + dataRecord.date + "','Day')";

                    cmd.CommandText = query;
                    var reader = cmd.ExecuteNonQuery();
                    if (reader == 1) result.hasData = true;
                }
            }
            catch (Exception ex)
            {
                lvConsole.Items.Add(lvConsole.Text + ($"Ошибка adday: {ex}"));
            }
            return result;
        }

    }
}

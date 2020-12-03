using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workToSql
{
    public partial class frmWithSql : Form
    {

        private void hoursFromDay(string tubeId, DateTime date, string hourFieldName, string dayFieldName)
        {
            DateTime dateClear = new DateTime(date.Year, date.Month, 1); //первый день месяца
            DateTime dateEnd = dateClear.AddMonths(1).AddSeconds(-1); //последний день месяца
            int countDays = dateEnd.Day;  // именно день послед дня месяца??????
            GetData hourRecords = new GetData(tubeId, dateClear, dateEnd, "Hour", hourFieldName);
            if (hourRecords.records.Count == countDays * 24)
            {
                lvConsole.Items.Add("Часовые данные собраны в полном объеме. Нет необходимости в вычисление часовых дыр.");
                return;
            }

            GetData dayRecords = new GetData(tubeId, dateClear, dateEnd, "Day", dayFieldName);
            if (dayRecords.records.Count == 0)
            {
                lvConsole.Items.Add("Нет данных по суточным. Вычисление часовых дыр невозможно ");
                return;
            }

            for(DateTime dateI = dateClear; dateI <= dateEnd; dateI=dateI.AddDays(1))
            {
                DaysData hoursData =  hoursInDateI(hourRecords.records, dateI);
                if(hoursData.count == 24)
                {
                    //lvConsole.Items.Add("Часовые данные собраны в полном объеме. Нет необходимости в вычисление часовых дыр");
                    continue;
                }

                hoursInDay(dateI, dayRecords.records, hourRecords.records, hoursData,hourFieldName,dayFieldName);
                //firstDay = firstDay.AddDays(1)
            }
        }

        private void hoursInDay(DateTime date, List<DataRecord> dayRecords, List<DataRecord> hourRecords, DaysData hoursData,string hourFieldNames, string dayFieldName)
        {
            DateTime dateEnd = date.AddDays(1);
            int indexthisDay = searchDataInRecords(date, dayRecords); // заменить на энумербле
            if (indexthisDay >= 0 )
            {
                int indexPrevDay = searchDataInRecords(date.AddDays(-1), dayRecords);
                if (indexPrevDay >= 0)
                {
                    DataResult hoursThisDay = hoursThisDay_(date, hourRecords);
                    double delta = dayRecords[indexthisDay].d1 - dayRecords[indexPrevDay].d1; //разность показаний за сутки 
                    delta = delta - hoursThisDay.sum; //недособрано за день
                    if (Math.Abs(delta) < 0.000001) delta = 0.0;
                    if (delta < -0.00001) return;  
                    delta = delta / (24 - hoursData.count);
                    OurDB db = new OurDB();
                    for(DateTime h = date ; h < date.AddDays(1); h= h.AddHours(1))
                    {
                        if (searchDataInRecords(h, hoursThisDay.data) == -1)
                        {
                            int i = 1;
                            db.insertToDB(tubeId, h, "Hour", hourFieldName, delta, hourRecords[indexthisDay].s2, "calculated");
                        }
                    }
                }
                else
                {
                    lvConsole.Items.Add(string.Format("Нет данных по суточным за предыдущий день {0} . Вычисление часовых дыр невозможно.", date.AddDays(-1)));
                }    
            }
            else
            {
                lvConsole.Items.Add(string.Format("Нет данных по суточным за текущщий день {0} . Вычисление часовых дыр невозможно.", date.AddDays(-1)));
            }
        }

        private DaysData hoursInDateI(List<DataRecord> hourRecords, DateTime dateI)
        {
            DaysData daysData = new DaysData();
            daysData.count = 0;
            daysData.hoursSum = 0;
            daysData.hoursHas = false;

            foreach (DataRecord dataRecord in hourRecords)
            {
                if (dataRecord.date.Day == dateI.Day)
                {
                    daysData.count = daysData.count + 1;
                    daysData.hoursHas = true;
                    daysData.hoursSum = daysData.hoursSum = dataRecord.d1;
                    if (daysData.count == 24) return daysData;
                }
            }
            return daysData;
        }


        //поиск данных по одной дате (строго час или день)
        private int searchDataInRecords(DateTime date, List<DataRecord> records)
        {
            for(int index = 0; index< records.Count; index++)
            {
                if (records[index].date == date)
                    return index;
            }
            return -1;
        }

        private DataResult hoursThisDay_(DateTime date, List<DataRecord> records)
        {
            DataResult result = new DataResult();
            List<DataRecord> recs = new List<DataRecord>();

            double sumd1 = 0;
            int count = 0;
            for (int index = 0; index < records.Count; index++)
            {
                if (records[index].date.Day > date.Day)
                    break;
                if (records[index].date.Day == date.Day)
                {
                    result.data.Add(records[index]);
                    sumd1 = sumd1 + records[index].d1;
                    count++;
                }
            }
            result.count = count;
            result.sum = sumd1;

            return result;
        }

    }
}

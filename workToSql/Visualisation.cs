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
        private void visualisation(string tubeId, DateTime date, string hourFieldName, string dayFieldName, int amountTypeHour, bool ChoiceHour, bool ChoiceDay)
        {
            DateTime dateClear = new DateTime(date.Year, date.Month, 1); //первый день месяца
            DateTime dateEnd = dateClear.AddMonths(1).AddSeconds(-1); //последний день месяца
            int countDays = dateEnd.Day;  // именно день послед дня месяца??????

            if(ChoiceHour)
            {
                GetData hourRecords = new GetData(tubeId, dateClear, dateEnd, "Hour", hourFieldName);


                int[] counts = new int[countDays + 1];
                foreach (var record in hourRecords.records)
                {
                    counts[record.date.Day]++;
                }

                monthCalendar1.RemoveAllBoldedDates();
                for (int iDay = 1; iDay <= countDays; iDay++)
                {
                    if (counts[iDay] < 24 * amountTypeHour)
                        monthCalendar1.AddBoldedDate(new DateTime(date.Year, date.Month, iDay));
                }
                monthCalendar1.UpdateBoldedDates();

            }

            if (ChoiceDay)
            {
                int[] countsDay = new int[countDays + 1];
                for (int iDay = 1; iDay <= countDays; iDay++)
                    countsDay[iDay] = 0;

                GetData dayRecords = new GetData(tubeId, dateClear, dateEnd, "Day", dayFieldName);
                monthCalendar2.RemoveAllBoldedDates();

                foreach (var record in dayRecords.records)
                {
                    countsDay[record.date.Day] = 1;
                }
                for (int iDay = 1; iDay <= countDays; iDay++)
                {
                    if (countsDay[iDay] < 1)
                        monthCalendar2.AddBoldedDate(new DateTime(date.Year, date.Month, iDay));
                }
                monthCalendar2.UpdateBoldedDates();

            }


        }

    }
}

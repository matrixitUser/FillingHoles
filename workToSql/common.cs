// Программа формирует суточные показания (меркурий) на основе данных по часовым потребленям
//Суточные показания -  s1='EnergyP+'  хранятся с датой начала суток (например: 2018-01-04 00:00:00.0000000)  - На самом деле это данные на конец суток !!!
//Часовое потребление s1='P+', хранятся с датой начала часа (например: 2018-01-03 18:00:00.0000000)             На самом деле это потребление за этот час, то есть с 18:00 по 19:00
// значит, если надо посчитать показания надо  или 1 : к показаниям предыдущих суток прибавить потребление за искомые сутки ( сумма по часам за искомые сутки)
//                                             или 2 от показаний  последующих суток отнять потребление за последующих суток ( сумма по часам за последующие сутки) 


using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;



namespace workToSql
{
    public class QueryParam 
    {
        public string tableName;
        public string start;
        public string end;
        public int count;

        public QueryParam(string type, DateTime dateStart, DateTime dateEnd)
        {
            switch (type)
            {
                case "Day":  // суточные данные
                    tableName = string.Format("DataRecordDay{0:0000}", dateStart.Year);
                    start = dateStart.ToString("yyyy-MM-dd HH:mm:ss");
                    end = dateEnd.ToString("yyyy-MM-dd HH:mm:ss");
                    count = (dateEnd - dateStart).Days + 1;
                    break;
                case "Hour":  // часовые данные
                    tableName = string.Format("DataRecordHour{0:00}{1:0000}", dateStart.Month,dateStart.Year);
                    start = dateStart.ToString("yyyy-MM-dd HH:mm:ss");
                    end = dateEnd.ToString("yyyy-MM-dd HH:mm:ss");
                    count = (dateEnd - dateStart).Hours + 1;
                    break;
            }
        }

        public QueryParam()
        {
        }
    }

    public class DayData
    {
        public double d1 { get; set; }
        public string s1 { get; set; }
        public bool hasData { get; set; }
        public string s2 { get; set; }
    }

    public class LastDay
    {
        public int lastDay;
        public LastDay(DateTime date)
        {
            DateTime dt = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddSeconds(-1);
            lastDay = dt.Day;
        }
    }


    public class DataRecord
    {
        public string objectId { get; set; }
        public DateTime date { get; set; }
        public double d1 { get; set; }
        public string s1 { get; set; }
        public string s2 { get; set; }
        public string s3 { get; set; }

        public DataRecord(string objectId, DateTime date,double d1,string s1, string s2, string s3)
        {
            this.objectId = objectId;
            this.date = date;
            this.d1 = d1;
            this.s1 = s1;
            this.s2 = s2;
            this.s2 = s3;
        }
        public DataRecord()
        {
        }

    }

    public class DataResult
    {
        public List<DataRecord> data { get; set; }  //Ienumerable List<DataRecord>
        public DataRecord dataPrev { get; set; }
        public DataRecord dataNext { get; set; }
        public bool hasData { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public double sum { get; set; }
        public int count  { get; set; }
        public DataResult()
        {
            data = new List<DataRecord>();
        }


    }


    public class HasDayData
    {
        public DateTime dt { get; set; }
        public bool dayHas { get; set; }
        public double dayValue { get; set; }
        public bool hoursHas { get; set; }
        public double hoursSum { get; set; }

    }

    public class DaysData
    {
        public bool hoursHas { get; set; }
        public double hoursSum { get; set; }
        public int count { get; set; }
    }

    public class Folder
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }


    public class Tube
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
    public class Parameter
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string typePeriod { get; set; }
        public int fulled { get; set; }
    }


}

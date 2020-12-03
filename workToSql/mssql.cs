using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workToSql
{
    //Получение суточных значений за месяц dateI по парааметру 
    //dateEnd включительно, то есть если совпадает dateStart, dateEnd то один период
    public class GetData
    {
        public DataResult result = new DataResult();
        DataRecord record = new DataRecord();
        public List<DataRecord> records = new List<DataRecord>();
        public GetData(string objectId, DateTime dateStart, DateTime dateEnd, string type, string s1)
        {
            result.hasData = false; result.success = true;
            DateTime dtE = dateEnd;
            DateTime dateI = dateStart;
            DateTime dtStart = dateStart;
            QueryParam queryParam = new QueryParam(type, dtStart, dtE);

            while (dateI <= dateEnd) //цикл для вывода всех данных по дате
            {
                switch (type)
                {
                    
                    case "Day":  // суточные данные
                        dtE = dateI;
                        dateI = dateI.AddDays(1);
                        if (dateI.Year != dtE.Year)
                        {
                            queryParam = new QueryParam(type, dtStart, dtE);
                        }
                         records = fromMssql(objectId, s1, queryParam);
                         dtStart = dateI;
                        
                        break;
                        
                    case "Hour":  // часовые данные
                        dtE = dateI;
                        dateI = dateI.AddHours(1);

                        if (dateI.Month != dtE.Month)
                        {
                            queryParam = new QueryParam(type, dtStart, dtE);
                            var rec = fromMssql(objectId, s1, queryParam);
                            records.AddRange(rec);
                            dtStart = dateI;
                            
                        }
                        break;
                }
            }

            if (dtE > dtStart)
            {
                queryParam = new QueryParam(type, dtStart, dtE);
                var rec = fromMssql(objectId, s1, queryParam);
                records.AddRange(rec);
            }
        }
        private List<DataRecord> fromMssql(string tubeId, string parameter, QueryParam queryParam)
        {
            List<DataRecord> records = new List<DataRecord>();
            string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            SqlConnection conMssql = new SqlConnection(mssqlCS);
            
            try
            {
                using (conMssql)
                {
                    var cmd = new SqlCommand();
                    var query = $"";
                    cmd.Connection = conMssql;
                    conMssql.Open();
                    string s1 = "'" + parameter + "'";
                    query = $"select  s1, d1, s2, Date from " + queryParam.tableName + " where ObjectId='" + tubeId + "' and  Date between '" + queryParam.start + "' and '" + queryParam.end + "' and S1 IN(" + s1 + ") order by Date"; // and S1 ='" + parameter + "' order by Date";
                    
                    double sumd1 = 0.0;
                    int recordcount = 0;
              
                    cmd.CommandText = query;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRecord record = new DataRecord();
                        record.s1 = reader.GetString(0);
                        record.d1 = reader.GetDouble(1);
                        record.s2 = reader.GetString(2);
                        record.date = reader.GetDateTime(3);
                        recordcount++;
                        sumd1 = sumd1 + record.d1;
                        records.Add(record);
                    }
                   
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                /*
                result.message = $"Ошибка при запросе данных: {ex}";
                result.success = false;
                */
                conMssql.Close();
            }
            conMssql.Close();

            return records;
        }

    }


public class OurDB
        {
            public DataResult result = new DataResult();
            public void insertToDB(string objectId, DateTime date, string type, string s1, double d1, string s2, string s3)
            {
                string mssqlCS = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
                SqlConnection conMssql = new SqlConnection(mssqlCS);
                try
                {
                    using (conMssql)
                    {
                        var cmd = new SqlCommand();
                        cmd.Connection = conMssql;
                        conMssql.Open();
                        string table = tableName(date, type);
                        //удаление
                        var query = $"DELETE FROM " + table +
                        " where TYPE='" + type + "' and OBJECTID='" + objectId + "' and DATE='" + date.ToString() +
                       "' and S1='" + s1 + "' and S3='calculated'";

                        cmd.CommandText = query;
                        var reader = cmd.ExecuteNonQuery();

                        string dateS = date.ToString("yyyy-MM-dd HH:mm:ss");
                        string d1S = string.Format("{0:0.00000}", d1).Replace(",",".");
                        //вставка
                        query = $"INSERT INTO " + table +
                                " (ID,DT1,S1,D1,S2,S3,OBJECTID,DATE,TYPE)  VALUES (NEWID(), GETDATE (),'" +
                                s1 + "','" + d1S + "','" + s2 + "','" + s3 + "','" + objectId + "','" +
                                dateS + "','"+type+"')";

                        cmd.CommandText = query;
                        reader = cmd.ExecuteNonQuery();
                        if (reader == 1) result.count = 1;
                    }
                }
                catch (Exception ex)
                {
                    result.message = $"Ошибка при добавление данных: {ex}";
                    result.success = false;
                    conMssql.Close();

                }
                conMssql.Close();
            }

            private string tableName(DateTime date, string type)
            {
                switch (type.ToLower())
                {
                    case "hour": return string.Format("DataRecordHour{0:00}{1:0000}", date.Month, date.Year);
                    case "day": return string.Format("DataRecordDay{0:0000}", date.Year);
                    default: return "DataRecordDefualt";
                }
            }

        }
    }

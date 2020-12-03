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
        public void SetTypeHour()
        {
            IEnumerable<Parameter> parameters = null;
            for (int i = 0; i < cLBHour.Items.Count; i++)
            {
                var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = cLBHour.Items[i].ToString() })
                            .Set("p.typePeriod='Hour'")
                            .Return(p => p.As<Parameter>());
                parameters = query.Results;
            }
            var firstParameter = parameters.Cast<Parameter>().First();
            if(firstParameter.typePeriod != "")
                lvConsole.Items.Add("Заполнение по типу 'Hour' прошло успешно.");
            
     
        }
        public void SetTypeDay()
        {
            IEnumerable<Parameter>  parameters = null;
            
            for (int i = 0; i < clbDay.Items.Count; i++)
            {
                var query1 = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                           .Where("t.id = {id}").WithParams(new { id = tubeId })
                           .AndWhere("p.name ={name}").WithParams(new { name = clbDay.Items[i].ToString() })
                           .Set("p.typePeriod='Day'")
                           .Return(p => p.As<Parameter>());
                parameters = query1.Results;
            }
            if (parameters.Cast<Parameter>().First().typePeriod != "")
                lvConsole.Items.Add("Заполнение по типу 'Day' прошло успешно.");
        }
        public void SetFulledHour()
        {
            for (int i = 0; i < cLBHour.Items.Count; i++)
            {
                if (cLBHour.GetItemChecked(i) == true)
                {
                    var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = cLBHour.Items[i].ToString() })
                            .AndWhere("p.typePeriod='Hour'")
                            .Set("p.fulled=1")
                            .Return(p => p.As<Parameter>());
                    IEnumerable<Parameter> parameters = query.Results;
                }
                else
                {
                    var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = cLBHour.Items[i].ToString() })
                            .AndWhere("p.typePeriod='Hour'")
                            .Set("p.fulled=0")
                            .Return(p => p.As<Parameter>());
                    IEnumerable<Parameter> parameters = query.Results;
                }
            }
        }
        public void SetFulledDay()
        {
            for (int i = 0; i < clbDay.Items.Count; i++)
            {
                if (clbDay.GetItemChecked(i) == true)
                {
                    var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = clbDay.Items[i].ToString() })
                            .AndWhere("p.typePeriod='Day'")
                            .Set("p.fulled=1")
                            .Return(p => p.As<Parameter>());
                    IEnumerable<Parameter> parameters = query.Results;
                }
                else
                {
                    var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = clbDay.Items[i].ToString() })
                            .AndWhere("p.typePeriod='Day'")
                            .Set("p.fulled=0")
                            .Return(p => p.As<Parameter>());
                    IEnumerable<Parameter> parameters = query.Results;
                }
            }

        }
        /* ----------------------------------------------------------------------- */
        public void SetCheckParameters()
        {
            for (int i = 0; i < cLBHour.Items.Count; i++)
            {
                var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = cLBHour.Items[i].ToString() })
                            .AndWhere("p.typePeriod='Hour'")
                            .Return(p => p.As<Parameter>());
                IEnumerable<Parameter> parameters = query.Results;
                foreach (Parameter param in parameters)
                {
                    if (param.fulled==1)
                    {
                        cLBHour.SetItemChecked(i, true);
                    }
                }
            }
            for (int i = 0; i < clbDay.Items.Count; i++)
            {
                var query = client.Cypher.Match("(t:Tube)-->(p:Parameter)")
                            .Where("t.id = {id}").WithParams(new { id = tubeId })
                            .AndWhere("p.name ={name}").WithParams(new { name = clbDay.Items[i].ToString() })
                            .AndWhere("p.typePeriod='Day'")
                            .Return(p => p.As<Parameter>());
                IEnumerable<Parameter> parameters = query.Results;
                foreach (Parameter param in parameters)
                {
                    if (param.fulled == 1)
                    {
                        clbDay.SetItemChecked(i, true);
                    }
                }
            }
        }
        public void MultipleSelectionParametersHour()
        {
            string strHour = "";
            int iHour = 0;
            for (int i = 0; i < cLBHour.Items.Count; i++)
                if (cLBHour.GetItemChecked(i) == true)
                {
                    if (strHour == "")
                        strHour = cLBHour.Items[i].ToString();
                    else
                        strHour = strHour + "','" + cLBHour.Items[i].ToString();
                    iHour++;
                }
            amountTypeHour = iHour;
            hourFieldName = strHour; 
        }
        public void MultipleSelectionParametersDay()
        {
            string strDay = "";
            for (int i = 0; i < clbDay.Items.Count; i++)
                if (clbDay.GetItemChecked(i) == true)
                {
                    if (strDay == "")
                        strDay = clbDay.Items[i].ToString();
                    else
                        strDay = strDay + "','" + clbDay.Items[i].ToString();
                    /*
                    if (strDay == "")
                        strDay = "'" + clbDay.Items[i].ToString() + "'";
                    else
                        strDay = strDay + ",'" + clbDay.Items[i].ToString() + "'"; 
                      */
                }
            dayFieldName = strDay;
        }
    }
}

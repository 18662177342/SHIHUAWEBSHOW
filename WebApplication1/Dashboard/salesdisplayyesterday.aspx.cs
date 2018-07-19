using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace WebApplication1.Dashboard
{
    public partial class salesdisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.10.34)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=prod)));Persist Security Info=True;User ID=ifsapp;Password=BDa@45bxPS;";
            OracleConnection con = new OracleConnection(connString);
            con.Open();
            OracleCommand cmd = new OracleCommand("SELECT  to_char(so.date_entered,'yyyy-mm-dd hh24:mi:ss') 订单创建日期,so.ORDER_NO || '-' || so.RELEASE_NO || '-' || so.SEQUENCE_NO 订单号,so.PART_NO 零件号,Inventory_Part_API.Get_Description (so.contract, so.part_NO) 零件描述,so.process_type 线别,so.priority_category 优先级,so.revised_qty_due 订单量,SO.QTY_COMPLETE 完成数量,SS1.WORK_CENTER_NO 工序1,SS1.QTY_COMPLETE 完成量1,SS2.WORK_CENTER_NO 工序2,SS2.QTY_COMPLETE 完成量2,SS3.WORK_CENTER_NO 工序3,SS3.QTY_COMPLETE 完成量3,SS4.WORK_CENTER_NO 工序4,SS4.QTY_COMPLETE 完成量4,SS5.WORK_CENTER_NO 工序5,SS5.QTY_COMPLETE 完成量5 FROM ifsapp.Shop_ord so,IFSINFO.IAL_SO_OPERATION_SEQ SS1,IFSINFO.IAL_SO_OPERATION_SEQ SS2,IFSINFO.IAL_SO_OPERATION_SEQ SS3,IFSINFO.IAL_SO_OPERATION_SEQ SS4,IFSINFO.IAL_SO_OPERATION_SEQ SS5 WHERE SO.CONTRACT = SS1.CONTRACT(+) AND SO.ORDER_NO = SS1.ORDER_NO(+) AND SO.RELEASE_NO = SS1.RELEASE_NO(+) AND SO.SEQUENCE_NO = SS1.SEQUENCE_NO(+) AND 1 = SS1.ITEM_NO(+) AND SO.CONTRACT = SS2.CONTRACT(+) AND SO.ORDER_NO = SS2.ORDER_NO(+) AND SO.RELEASE_NO = SS2.RELEASE_NO(+) AND SO.SEQUENCE_NO = SS2.SEQUENCE_NO(+) AND 2 = SS2.ITEM_NO(+) AND SO.CONTRACT = SS3.CONTRACT(+) AND SO.ORDER_NO = SS3.ORDER_NO(+) AND SO.RELEASE_NO = SS3.RELEASE_NO(+) AND SO.SEQUENCE_NO = SS3.SEQUENCE_NO(+) AND 3 = SS3.ITEM_NO(+) AND SO.CONTRACT = SS4.CONTRACT(+) AND SO.ORDER_NO = SS4.ORDER_NO(+) AND SO.RELEASE_NO = SS4.RELEASE_NO(+) AND SO.SEQUENCE_NO = SS4.SEQUENCE_NO(+) AND 4 = SS4.ITEM_NO(+) AND SO.CONTRACT = SS5.CONTRACT(+) AND SO.ORDER_NO = SS5.ORDER_NO(+) AND SO.RELEASE_NO = SS5.RELEASE_NO(+) AND SO.SEQUENCE_NO = SS5.SEQUENCE_NO(+) AND 5 = SS5.ITEM_NO(+) AND earliest_start_date = TO_DATE (SYSDATE)-1 AND so.OBJSTATE <> 'Planned' AND so.OBJSTATE <> 'Closed' AND so.contract='SH10'ORDER BY so.Process_type, so.priority_category ", con);
            OracleDataReader dr = cmd.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
            dr.Close();
            con.Close();
        }
        protected void gvObjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                //保持列不变形
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    //方法一：
                    e.Row.Cells[i].Text = "&nbsp;" + e.Row.Cells[i].Text + "&nbsp;";
                    e.Row.Cells[i].Wrap = false;
                    //方法二：
                    //e.Row.Cells[i].Text = "<nobr>&nbsp;" + e.Row.Cells[i].Text + "&nbsp;</nobr>";            
                }
            }
        }
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace WebApplication1
{
    public partial class Oracledata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) 
        {

            string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.10.34)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=prod)));Persist Security Info=True;User ID=ifsapp;Password=BDa@45bxPS;";
            OracleConnection con = new OracleConnection(connString);
            con.Open();
            OracleCommand cmd = new OracleCommand("Select col.order_no ||'-'|| col.line_no ||'-'|| col.rel_no 客户订单 ,  col.customer_no 客户号  ,col.Customer_Part_No 客户料号 , col.PART_NO 库存料号 , col.CATALOG_DESC 料号描述,  col.qrd_qty  订单数量,  col.qty_shipped 已发数量, NVL(col.Qty_Available,0) 可用数量 ,ROUND ( (NVL(col.Qty_Available,0)-col.qrd_qty+col.qty_shipped+col.reserved_qty ), 3) 发货差异, to_char(col.date_entered,'yyyy-mm-dd hh24:mi:ss') 录入日期, to_char(col.planned_ship_date ,'yyyy-mm-dd hh24:mi:ss') 货运日期, col.delivery_type 发货渠道 , col.delivery_type_description 渠道描述  , col.Note_text  出货宽度备注 from ifsinfo.IAL_DELIVERY_REQUIREMENT col where col.contract = 'SH10' and to_date(to_char(col.planned_ship_date,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date(to_char((sysdate+(0)),'dd/mm/yyyy'),'dd/mm/yyyy')   order by col.planned_ship_date Asc ,col.PART_NO Asc  ", con);
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesdisplaytoday.aspx.cs" Inherits="WebApplication1.Dashboard.salesdisplaytoday" %>

<!DOCTYPE html>
<script src="../Scripts/Jquery.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <header>
        <img src="../5b04d0f1449d4.png" width="303px;" height="70px;">
        <h1 style="float: right; margin: 8px">Shop Order Tracking-<span style="color:red">Today</span><br/><span style="font-size:16px;font-weight:bold;float:right"><span class="howmany"></span></span></h1>
  
    </header>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" Font-Size="15px" Font-Bold="True" Font-Family="Microsoft Yahei" Width="100%" CellPadding="4" GridLines="None" ForeColor="#333333">
         <EditRowStyle BackColor="#999999" />
            <FooterStyle CssClass="GridViewFooterStyle" BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <RowStyle CssClass="GridViewRowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle CssClass="GridViewPagerStyle" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" BackColor="White" ForeColor="#284775" />
            <HeaderStyle CssClass="GridViewHeaderStyle" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </form>
    
</body>
</html>
<script>
    var index_page = 1
    $(function () {
        $("tr:first").css("background-color", "#e74c3c")
        $("tr:odd").css("background-color", "rgba(192,192,192,1)")
       
  
        var $table = $('#GridView1');
        var currentPage = 0;//当前页默认值为0  
        var pageSize = 35;//每一页显示的数目  
        $table.bind('paging', function () {
            $table.find('tbody tr').hide().slice(currentPage * pageSize, (currentPage + 1) * pageSize).show();
        });
        var sumRows = $table.find('tbody tr').length;
        var sumPages = Math.ceil(sumRows / pageSize);//总页数  
        document.cookie = SetCookie('currentPageToday', currentPage); //cookie位置
        document.cookie = SetCookie('sumPagesToday', sumPages); //cookie位置
        $('.howmany').html('&nbsp&nbspPage:' + index_page + '/' + sumPages);//页数显示
        var $pager = $('<div class="page" style="display:none" ></div>');  //新建div，放入a标签,显示底部分页码  
        for (var pageIndex = 0 ; pageIndex < sumPages ; pageIndex++) {
            $('<a href="#" id="pageStyle" onclick="changCss(this)"><span>' + (pageIndex + 1) + '</span></a>').bind("click", { "newPage": pageIndex }, function (event) {
                currentPage = event.data["newPage"];
                $table.trigger("paging");
                //触发分页函数  
            }).appendTo($pager);
            $pager.append(" ");
        }
        $pager.insertAfter($table);
        $table.trigger("paging");

        //默认第一页的a标签效果  
        //var $pagess = $('#pageStyle');
        //$pagess[0].style.backgroundColor = "#006B00";
        //$pagess[0].style.color = "#ffffff";
        setInterval(trig, 30000);
    });

    //a链接点击变色，再点其他回复原色  
    function changCss(obj) {
        var arr = document.getElementsByTagName("a");
        for (var i = 0; i < arr.length; i++) {
            if (obj == arr[i]) {       //当前页样式  
                //obj.style.backgroundColor = "#006B00";
                obj.style.color = "#ffffff";
            }
            else {
                arr[i].style.color = "";
                arr[i].style.backgroundColor = "";
            }
        }
    }


</script>
<script>
    function trig() {
        var currentid = unescape(getCookie('currentPageToday'));
        var maxid = unescape(getCookie('sumPagesToday'));
        parsecurrentid = parseInt(currentid);
        parsemaxid = parseInt(maxid)
        if (parsecurrentid < parsemaxid) {
            parsecurrentids = parsecurrentid + 1;
            $("a:eq('" + parsecurrentids + "')").trigger("click");
            $("tr:eq(0)").css('display', 'table-row');
            if (index_page < parsemaxid) {
                $('.howmany').html('&nbsp&nbsp页数:' + ++index_page + '/' + maxid);//页数显示
            }
            document.cookie = SetCookie('currentPageToday', parsecurrentids);
            console.log(parsecurrentids);
        } else {
            window.location.reload()
        }
    }
    function SetCookie(name, value) {
        var exp = new Date();
        exp.setTime(exp.getTime() + 3 * 24 * 60 * 60 * 1000); //3天过期
        document.cookie = name + "=" + encodeURIComponent(value) + ";expires=" + exp.toGMTString() + ";path=/";
        return true;
    };
    function getCookie(name) {
        var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return null;
    };
    $("#GridView1").find("tr").each(function (i) {
        var thisrow = $(this);
        $(this).find("td:eq(8)").each(function (i) {
            if (1<0) {
                thisrow.css("color", "red");
            } else {
                thisrow.css("color", "black");
            }
        })
    })


</script>



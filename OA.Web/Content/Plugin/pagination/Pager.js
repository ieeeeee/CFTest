var pagerGlobalParam = { pageSize: 5, currPage: 1, pageNum: 5, pageCount: 0, contentData: null, type: null, param: null };

function CreatePager(container, isHaveGo, isMore, isMoreCss, isSelect, isSelectCss, currPageCss, numPageCss, nextCss, preCss, preTxt, nextTxt, firstPage, lastPage, isHaveTxt) {
    //设置一些可省参数的默认值
    if (preTxt == undefined || preTxt == "") {
        preTxt = "上一页";
    }
    if (nextTxt == undefined || nextTxt == "") {
        nextTxt = "下一页";
    }
    if (isHaveGo == undefined) {
        isHaveGo = false;
    }
    if (isMore == undefined) {
        isMore = true;
    }
    if (isSelect == undefined) {
        isSelect = true;
    }

    if (firstPage == undefined || firstPage == "") {
        firstPage = "首页";
    }
    if (lastPage == undefined || lastPage == "") {
        lastPage = "尾页";
    }

    //if(isHaveTxt ==undefined){
    //    isHaveTxt =true;
    //}

    var pageCount = GetPageCount();
    if (pagerGlobalParam.currPage > pageCount) {
        pagerGlobalParam.currPage = pageCount;
    }
    var html = "";
    if (isHaveTxt == undefined || isHaveTxt == "") {
        html += "<div style='width:auto;float:left;line-height:25px;'>当前第 <font color='green'><b>" + pagerGlobalParam.currPage + "</b></font> 页&nbsp;&nbsp;&nbsp;&nbsp;每页显示&nbsp;<font color='blue'><b>" + pagerGlobalParam.pageSize + "</b></font>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;共&nbsp;<font color='red'><b>" + pageCount + "</b></font>&nbsp;页&nbsp;&nbsp;&nbsp;&nbsp;</div>";
    } else {
        html += isHaveTxt;
    }
   
    //上一页，如果当前页是第一页 “上一页”为不可以点击的
    if (firstPage != "") {
        if (pagerGlobalParam.currPage == 1) {
            html += "<div class='" + preCss + "'>" + firstPage + "</div>";
        } else {
            html += "<div class='" + preCss + "' onclick='javascript:FirstPage()'><a href='javascript:void(0)'>" + firstPage + "</a></div>";
        }
    }
    if (pagerGlobalParam.currPage == 1) {
        html += "<div class='" + preCss + "'>" + preTxt + "</div>";
    } else {
        html += "<div class='" + preCss + "' onclick='javascript:PrePage()'><a href='javascript:void(0)' >" + preTxt + "</a></div>";
    }
    //生成页码: 如果当前页数 >= 页码显示的个数 ，就滚动生成页码，[5][6] 7 [8][9],  [6][7] 8 [9][10],  [7][8] 9 [10][11].....
    var maxPage = pagerGlobalParam.pageNum >= pageCount ? pageCount : pagerGlobalParam.pageNum;
    if (pagerGlobalParam.currPage <= (pagerGlobalParam.pageNum - 2)) {
        for (var i = 1; i <= maxPage; i++) {
            if (pagerGlobalParam.currPage == i) {
                html += "<div class='" + currPageCss + "'>" + i + "</div>";
            } else {
                html += "<div class='" + numPageCss + "' onclick='javascript:Page(" + i + ")'><a href='javascript:void(0)'>" + i + "</a></div>";
            }
        }
        if (isMore) {
            if (maxPage != pageCount) {
                html += "<div class='" + isMoreCss + "' onclick='javascript:MorePage(" + (maxPage + 1) + ")'><a href='javascript:void(0)'>…</a></div>";
            }
        }
    } else {
        var deviation = pagerGlobalParam.pageNum % 3 == 0 ? pagerGlobalParam.pageNum / 3 : parseInt(pagerGlobalParam.pageNum / 3) + 1;
        var endPage = pagerGlobalParam.currPage + (pagerGlobalParam.pageNum - deviation) > pageCount ? pageCount : (pagerGlobalParam.currPage + (pagerGlobalParam.pageNum - deviation - 1));
        if (endPage == pageCount) {
            if (isMore) {
                if (maxPage != pageCount) {
                    endPage = pagerGlobalParam.currPage - deviation > pageCount ? pageCount : pagerGlobalParam.currPage - deviation;
                    html += "<div class='" + isMoreCss + "' onclick='javascript:MorePage(" + endPage + ")'><a href='javascript:void(0)'>...</a></div>";
                }
            }
            for (var l = pageCount - maxPage + 1; l <= pageCount; l++) {
                if (pagerGlobalParam.currPage == l) {
                    html += "<div class='" + currPageCss + "'>" + l + "</div>";
                } else {
                    html += "<div class='" + numPageCss + "' onclick='javascript:Page(" + l + ")'><a href='javascript:void(0)'>" + l + "</a></div>";
                }
            }
        } else {
            var starPage = pagerGlobalParam.currPage - (pagerGlobalParam.pageNum - deviation - 1) <= 0 ? 1 : pagerGlobalParam.currPage - (pagerGlobalParam.pageNum - deviation - 1);
            for (var j = starPage; j < pagerGlobalParam.currPage; j++) {
                html += "<div class='" + numPageCss + "' onclick='javascript:Page(" + j + ")'><a href='javascript:void(0)'>" + j + "</a></div>";
            }
            html += "<div class='" + currPageCss + "'>" + pagerGlobalParam.currPage + "</div>";
            for (var k = pagerGlobalParam.currPage + 1; k <= endPage; k++) {
                html += "<div class='" + numPageCss + "' onclick='javascript:Page(" + k + ")'><a href='javascript:void(0)'>" + k + "</a></div>";
            }
            if (isMore) {
                endPage = pagerGlobalParam.currPage + deviation > pageCount ? pageCount : pagerGlobalParam.currPage + deviation;
                html += "<div class='" + isMoreCss + "' onclick='javascript:MorePage(" + endPage + ")'><a href='javascript:void(0)'>…</a></div>";
            }
        }
    }

    //如果是最后一页 那么"下一页"为不可点击状态
    if (pagerGlobalParam.currPage == pageCount) {
        html += "<div class='" + nextCss + "'>" + nextTxt + "</div>";
    } else {
        html += "<div class='" + nextCss + "' onclick='javascript:NextPage()'><a href='javascript:void(0)' >" + nextTxt + "</a></div>";
    }

    if (lastPage != "") {
        if (pagerGlobalParam.currPage == pageCount) {
            html += "<div class='" + nextCss + "'>" + lastPage + "</div>";
        } else {
            html += "<div class='" + nextCss + "' onclick='javascript:LastPage()'><a href='javascript:void(0)' >" + lastPage + "</a></div>";
        }
    }
    if (isSelect) {
        html += "<div style='width:auto;float:left;height:25px;margin-left:25px;text-align:left;float:left;'>";
        html += "<select onchange='SlOnChange(this)' class='" + isSelectCss + "'>";
        for (var m = 1; m <= pageCount; m++) {
            if (m == pagerGlobalParam.currPage) {
                html += "<option value='" + m + "' selected='selected'>" + m + "</option>";
            } else {
                html += "<option value='" + m + "'>" + m + "</option>";
            }
        }
        html += "</select></div>";
    }

    if (isHaveGo) {
        html += "<div style='width:auto;float:left;height:25px;margin-left:25px;text-align:left;float:left;'>";
        html += "<input type='text'onkeypress=\"return event.keyCode>=48&&event.keyCode<=57\" onpaste=\"return !clipboardData.getData('text').match(/\D/)\" ondragenter=\"return false\" style=\"ime-mode:Disabled;width:25px;\"  id='txtPager' value='" + pagerGlobalParam.currPage + "' />";
        html += "<input type='button' value='GO' style='height:26px;' onclick='javascript:GoPage()' /></div>";
    }
    $(container).html(html);
    
}

function GetPageCount() {
    return parseInt(pagerGlobalParam.pageCount) % pagerGlobalParam.pageSize == 0 ? parseInt((parseInt(pagerGlobalParam.pageCount) / pagerGlobalParam.pageSize)) : parseInt((parseInt(pagerGlobalParam.pageCount) / pagerGlobalParam.pageSize)) + 1;
}

//下拉框
function SlOnChange(obj) {
    pagerGlobalParam.currPage = parseInt($(obj).val());
    ChoiceFuntion();
}

//首页
function FirstPage() {
    pagerGlobalParam.currPage = 1;
    ChoiceFuntion();
}

//末页
function LastPage() {
    pagerGlobalParam.currPage = GetPageCount();
    ChoiceFuntion();
}

//更多“...”按钮
function MorePage(index) {
    pagerGlobalParam.currPage = index;
    ChoiceFuntion();
}

//跳转按钮
function GoPage() {
    var page = parseInt($.trim($("#txtPager").val()) === "" ? "1" : $.trim($("#txtPager").val()));
    var pageCount = GetPageCount();
    if (page <= 0) {
        pagerGlobalParam.currPage = 1;
    } else if (page > pageCount) {
        pagerGlobalParam.currPage = pageCount;
    } else {
        pagerGlobalParam.currPage = page;
    }
    ChoiceFuntion();
}

//上一页
function PrePage() {
    pagerGlobalParam.currPage--;
    ChoiceFuntion();
}

///下一页
function NextPage() {
    pagerGlobalParam.currPage++;
    ChoiceFuntion();
}

///数字页码
function Page(index) {
    pagerGlobalParam.currPage = index;
    ChoiceFuntion();
}

function ChoiceFuntion() {
    pagerGlobalParam.contentData(pagerGlobalParam.pageSize, pagerGlobalParam.currPage, pagerGlobalParam.pageCount);
}

jQuery.fn.extend({ pager: function (options) {
    pagerGlobalParam.contentData = options.funContent;
    pagerGlobalParam.pageSize = options.pagesize;
    if (options.pagesize != undefined) {
        pagerGlobalParam.pageSize = options.pagesize;
    }
    if (options.currpage != undefined) {
        pagerGlobalParam.currPage = options.currpage;
    }
    if (options.pagecount != undefined) {
        pagerGlobalParam.pageCount = options.pagecount;
    }
    if (options.pagenum != undefined) {
        pagerGlobalParam.pageNum = options.pagenum;
    }
    if (options.param != undefined) {
        pagerGlobalParam.param = options.param;
    }

    CreatePager(options.container,
        options.ishavego,
        options.ismore,
        options.ismorecss,
        options.isselect,
        options.isselectcss,
        options.currpagecss,
        options.numpagecss,
        options.nextcss,
        options.precss,
        options.pretxt,
        options.nexttxt,
        options.firstpage,
        options.lastpage,
        options.isHaveTxt);
}
});

/*
 *  调用示例
 *
 *  //获取数据条数，用于生成页码
    $.get("Default.ashx", { type: "getPageCount" }, function (result) {
            FillContent(param.pageSize, param.currPage,parseInt(result));
        });

    //填充数据这3个参数必填。**$**
    function FillContent(pageSize, currPage, count) {
        $.getJSON("Default.ashx", { type: "getPage", pageSize: pageSize, currPage: currPage }, function (result) {
            if (result !== null && result !== "" && result.data.length > 0) {
                var html = "";
                for (var i = 0; i < result.data.length; i++) {
                    var person = result.data[i];
                    html += person.Name + " —— " + person.Age + "岁——" + person.Height + "<br/><hr>";
                }
                $("#divResult").html(html);

                // 生成页码。
                $("body").pager({
                    funContent: FillContent,    //填充数据的方法名称，就是“**$**”处标识的方法. 根据需要修改
                    container: "#pager",        //用来展示页码的标签ID或者CLASS.
                    ismorecss: "isMore",        //更多的按钮“...”的样式.
                    isselectcss: "isSelect",    //下拉框选择页码的样式.
                    currpagecss: "currPage",    //当前页的样式.
                    numpagecss: "pageNum",      //除当前页其他页码的样式.
                    nextcss: "preNextPager",    //下一页的样式.
                    precss: "preNextPager",     //上一页的样式.
                    pagesize: pageSize,         //页面显示的数据条数.
                    pagecount: count            //总数居条数.
                });
            } else {
                $("#divResult").html("无匹配数据！！！");
            }
        });
    }
 */

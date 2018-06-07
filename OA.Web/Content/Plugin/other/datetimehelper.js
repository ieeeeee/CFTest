//计算当前日期
function CurentDate() {
    var now = new Date();
    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var clock = year + "-";

    if (month < 10)
        clock += "0";
    clock += month + "-";

    if (day < 10)
        clock += "0";
    clock += day + " ";

    return (clock);
}

function CalForDate(date) {
    var now = new Date(date);
    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var clock = year + "-";

    if (month < 10)
        clock += "0";
    clock += month + "-";

    if (day < 10)
        clock += "0";
    clock += day + " ";

    return (clock);
}

//计算当前日期时间
function CurentTime() {
    var now = new Date();

    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分

    var clock = year + "-";

    if (month < 10)
        clock += "0";
    clock += month + "-";

    if (day < 10)
        clock += "0";
    clock += day + " ";

    if (hh < 10)
        clock += "0";
    clock += hh + ":";

    if (mm < 10) clock += '0';
    clock += mm;
    return (clock);
}
//CalForTime(DateAdd("d ", -parseInt(time), new Date(entTime)));
function CalForTime(time) {
    var now = new Date(time);

    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分

    var clock = year + "-";

    if (month < 10)
        clock += "0";
    clock += month + "-";

    if (day < 10)
        clock += "0";
    clock += day + " ";

    if (hh < 10)
        clock += "0";
    clock += hh + ":";

    if (mm < 10) clock += '0';
    clock += mm;
    return (clock);
}

function DateAdd(interval, number, Date) {
    /*
      *   功能:实现VBScript的DateAdd功能.
      *   参数:interval,字符串表达式，表示要添加的时间间隔.
      *   参数:number,数值表达式，表示要添加的时间间隔的个数.
      *   参数:date,时间对象.
      *   返回:新的时间对象.
      *   var   now   =   new   Date();
      *   var   newDate   =   DateAdd( "d ",5,now);
      *---------------   DateAdd(interval,number,date)   -----------------
      */
    var date = Date;
    switch (interval) {
        case "y ": {
            date.setFullYear(date.getFullYear() + number);
            return date;
            break;
        }
        case "q ": {
            date.setMonth(date.getMonth() + number * 3);
            return date;
            break;
        }
        case "m ": {
            date.setMonth(date.getMonth() + number);
            return date;
            break;
        }
        case "w ": {
            date.setDate(date.getDate() + number * 7);
            return date;
            break;
        }
        case "d ": {
            date.setDate(date.getDate() + number);
            return date;
            break;
        }
        case "h ": {
            date.setHours(date.getHours() + number);
            return date;
            break;
        }
        case "m ": {
            date.setMinutes(date.getMinutes() + number);
            return date;
            break;
        }
        case "s ": {
            date.setSeconds(date.getSeconds() + number);
            return date;
            break;
        }
        default: {
            date.setDate(d.getDate() + number);
            return date;
            break;
        }
    }
}
/*
var now = new Date();
//加五天.
var newDate = DateAdd("d ", 5, now);
alert(newDate.toLocaleDateString())
//加两个月.
newDate = DateAdd("m ", 2, now);
alert(newDate.toLocaleDateString())
//加一年
newDate = DateAdd("y ", 1, now);
alert(newDate.toLocaleDateString()) */

///获取URL地址参数
function getUrlParam(param) {
    var params = decodeURI(location.search.substring(1));
    return param ? params[param] : params;
}
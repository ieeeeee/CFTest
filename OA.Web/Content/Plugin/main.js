import Vue from "~/Content/Plugin/vue.js"
$(function () {
    import vaddtext from "~/Content/Plugin/vueTemplate";
    Vue.use(vaddtext);
    var vList = new Vue({
        el: "#tsAddControl",
        data: {
            tableStructList: []
        },
        methods: {
            Save: function () {
                GetSave();
            }
        }

    });
    var modelInfo = {};
    var vueTemplate = [];
    //获取值
    GetModelInfo(function () {
        GetVueTemplate(function () {
            //获取结构
            GetTableStruct();
        });
    });


    //获取界面结构
    function GetTableStruct() {
        $.post("/Base/GetAddTableStruct", { "TableID": 98 }, function (result) {
            if (result) {
                console.log(modelInfo);
                for (var i = 0; i < result.length; i++) {
                    //赋初值
                    for (var key in modelInfo) {
                        if (result[i].Field == key) {
                            result[i].FieldValue = modelInfo[key];
                        }
                    }
                    //select 选项操作
                    if (result[i].Field == "VueTemplate") {
                        var options = [];
                        for (var j = 0; j < vueTemplate.length; j++) {
                            options.push({ text: vueTemplate[j].BaseName, value: vueTemplate[j].BaseValue });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }

                    if (result[i].Field == "IsQuery") {
                        var options = [];
                        for (var j = 0; j < RadioNum.length; j++) {
                            options.push({ text: RadioNum[j].text, value: RadioNum[j].value });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }
                    if (result[i].Field == "IsDeleted") {
                        var options = [];
                        for (var j = 0; j < RadioIsDel.length; j++) {
                            options.push({ text: RadioIsDel[j].text, value: RadioIsDel[j].value });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }
                    if (result[i].Field == "IsHide") {
                        var options = [];
                        for (var j = 0; j < RadioBool.length; j++) {
                            options.push({ text: RadioBool[j].text, value: RadioBool[j].value });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }
                }
                console.log(result);
                vList.tableStructList = result;
            }
        }, 'Json');
    }

    //获取初始值
    function GetModelInfo(func) {
        var ID =@Model.TStructID;

        $.post("/TableStruct/GetDetailInfo", { "id": ID }, function (result) {
            if (result) {
                // console.log(result);
                modelInfo = result;
            }
            func();
        }, 'Json');

    }

    //获取select选项 vueTemplate
    function GetVueTemplate(func) {
        $.post("/BaseInfo/GetClassInfo", { "baseClassID": 1 }, function (result) {
            if (result) {
                vueTemplate = result;
            }
            func();
        }, 'Json');
    }

    //提交
    function GetSave() {
        var data = {};
        for (var i = 0; i < vList.tableStructList.length; i++) {
            console.log(vList.tableStructList[i].Field);
            console.log(vList.tableStructList[i].FieldValue);
            data[vList.tableStructList[i].Field] = vList.tableStructList[i].FieldValue;//$("#" + vList.menuStructList[i].Field + "").val();
        }
        data.TStructID =@Model.TStructID;
        console.log(data);
        $.post('/TableStruct/Save', data, function (result) {
            if (result.flag) {
                layer.msg(result.msg);
                //window.location.href = "/TableS/Index";
                if (@Model.TStructID== 0) {
            var initField = ["Field", "FieldName", "VueTemplate"];

            for (var i = 0; i < vList.tableStructList.length; i++) {
                for (var j = 0; j < initField.length; j++) {
                    if (vList.tableStructList[i].Field == initField[j])
                        vList.tableStructList[i].FieldValue = "";
                    if (vList.tableStructList[i].Field == "OrderID")
                        vList.tableStructList[i].FieldValue = vList.tableStructList[i].FieldValue + 2;
                }

                //$("#" + vList.menuStructList[i].Field + "").val();
                //console.log(vList.tableStructList[i].FieldValue);
            }
        } else {
            window.location.href = "/TableStruct/Index";
        }


    } else {
        layer.msg(result.msg);
    }
}, 'Json');
            }

        })
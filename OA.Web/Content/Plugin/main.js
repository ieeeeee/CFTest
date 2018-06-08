
    Vue.component('vadd-text', {
        props: ['txt_index', 'txt_item'],
        template: `
                        <div class="input-group input-icon right">
                            <span class= "input-group-addon">
                                <i :class="txt_item.Icon"></i>
                            </span>
                            <input type="text" v-model="txt_item.FieldValue" :placeholder="txt_item.Placeholder" class="form-control1 icon"/>
                        </div>
                        `
    });
    Vue.component('vadd-checkbox', {
        props: ['ck_index', 'ck_item'],
        template: `
                         <div>
                             <div class="checkbox-inline"  v-for="option in ck_item.FieldSelect"><label><input type="radio" v-model="ck_item.FieldValue" :value="option.value">{{option.text}}</label></div>
                         </div>
                        `
    });
    Vue.component('vadd-textarea', {
        props: ['txtarea_index', 'txtarea_item'],
        template: `
                         <div>
                             <textarea class="form-control1" cols="50" rows="4" style="height:70px;" v-model="txtarea_item.FieldValue"></textarea>
                         </div>
                          `
    });
    Vue.component('vadd-select', {
        props: ['select_index', 'select_item'],
        template: `
                         <select class="form-control1" v-model="select_item.FieldValue">
                             <option v-for="option in select_item.FieldSelect" :value="option.value" >{{option.text}}</option>
                         </select>
                          `
    });
    Vue.component('vadd-password', {
        props: ['pwd_index', 'pwd_item'],
        template: `
                            <div class="input-group input-icon right">
                                <span class= "input-group-addon">
                                    <i :class="pwd_item.Icon"></i>
                                </span>
                                <input type="password" v-model="pwd_item.FieldValue" :placeholder="pwd_item.Placeholder" class="form-control1 icon"/>
                            </div>
                            `
    });
//    Vue.component('vadd-editor', {
//        props: ['editor_index', 'editor_item'],
//        template: `
//                                <div>
//                                         <div id="editor" " style="text-align:left">
//                                                 <button v-on:click="getContent">查看内容</button>
//                                        </div>
//                                </div>
//                                `
//})
    Vue.component('vadd-editor', {
        props: ['editor_index', 'editor_item'],
        template: `
                                    <div class="md-editor active">
                                          <textarea id="add-editor" name="content" data-provide="markdown" rows="10"  v-model="editor_item.FieldValue"></textarea>
                                    </div>
                                    `
    })
    var vList = new Vue({
        el: "#AddControl",
        data: {
            tableStructList: []
        },
        methods: {
            Save: function (SaveControl,ID,IDFlag) {
                GetSave(SaveControl,ID, IDFlag);
            }
        }

    });
//import E from 'wangeditor'

//export default {
//    name: 'editor',
//    data() {
//        return {
//            editorContent: ''
//        }
//    },
//    methods: {
//        getContent: function () {
//            alert(this.editorContent)
//        }
//    },
//    mounted() {
//        var editor = new E(this.$refs.editor)
//        editor.customConfig.onchange = (html) => {
//            this.editorContent = html
//        }
//        editor.create()
//    }
//}
    var modelInfo = {};
    var vueTemplate = [];
    var parentMenuInfo = [];
    var baseClassInfo = [];
    var deptInfo = [];

    //获取界面结构
function GetTableStruct(ID) {
    console.log("222");
        $.post("/Base/GetAddTableStruct", { "TableID": ID }, function (result) {
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
                    if (result[i].Field == "Gender") {
                        var options = [];
                        for (var j = 0; j < RadioSex.length; j++) {
                            options.push({ text: RadioSex[j].text, value: RadioSex[j].value });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }
                    //select 选项操作
                    if (result[i].Field == "ParentID") {
                        var options = [];
                        for (var j = 0; j < parentMenuInfo.length; j++) {
                            options.push({ text: parentMenuInfo[j].MenuName, value: parentMenuInfo[j].MenuID });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }
                    //获取select 选项
                    if (result[i].Field == "BaseClassID") {
                        var options = [];
                        for (var j = 0; j < baseClassInfo.rows.length; j++) {
                            options.push({ text: baseClassInfo.rows[j].BaseClassName, value: baseClassInfo.rows[j].BaseClassID });
                        }
                        result[i].FieldSelect = options;
                        console.log(result[i].FieldSelect);
                    }
                    //select 部门
                    if (result[i].Field == "DeptID") {
                        var options = [];
                        for (var j = 0; j < deptInfo.length; j++) {
                            options.push({ text: deptInfo[j].DeptName, value: deptInfo[j].DepartmentID});
                        }
                        result[i].FieldSelect = options;
                       
                        console.log(result[i].FieldSelect);
                    }
                }
                console.log(result);
                vList.tableStructList = result;    
            }
           // func();
        }, 'Json');
    }

    //获取初始值
    function GetModelInfo(Url,ID,func) {
        //var ID =@Model.TStructID;
        $.post(Url, { "id": ID }, function (result) {
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
    //select 选项操作
    function GetParentInfo(func) {
        $.post("/Base/GetMenuInfo", {}, function (result) {
            if (result) {
                console.log(result);
                parentMenuInfo = result;
            }
            func();
        }, 'Json');
    }
    //获取select选项 字典分类
    function GetBaseClass(baseClassFilter, func) {
        $.post("/BaseClass/GetAllBaseClass", baseClassFilter, function (result) {
            if (result) {
                baseClassInfo = result;
                console.log(result);
            }
            func();
        });
    }
    //获取select选项 部门
    function GetEntDeptInfo(func) {
        //var entID = UserInfo.LockEntID;
        //console.log(UserInfo);
        //console.log($("#LockEnt").val());
        $.post("/DeptInfo/GetEntDeptInfo", {}, function (result) {
            if (result) {
                deptInfo = result;
                console.log(deptInfo);

            }
            func();
        });
    }
    //提交
    function GetSave(SaveControl,ID, IDFlag) {
        var data = {};
        for (var i = 0; i < vList.tableStructList.length; i++) {
            console.log(vList.tableStructList[i].Field);
            console.log(vList.tableStructList[i].FieldValue);
            data[vList.tableStructList[i].Field] = vList.tableStructList[i].FieldValue;//$("#" + vList.menuStructList[i].Field + "").val();
        }
        data.EntID = UserInfo.LockEntID;
        data["" + IDFlag + ""] = ID;
        console.log(data);    
        $.post("/" + SaveControl+"/Save", data, function (result) {
            if (result.flag) {
                layer.msg(result.msg);
                if (SaveControl == "MenuInfo" || SaveControl == "BaseClass" || SaveControl == "DeptInfo" || SaveControl == "EntInfo" || SaveControl == "UserInfo") {
                    window.location.href = "/" + SaveControl + "/Index";
                }
                //window.location.href = "/TableS/Index";
                if (ID== 0) {
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
                    window.location.href = "/" + SaveControl +"/Index";
                }


            } else {
                layer.msg(result.msg);
            }
        }, 'Json');
}


//createEditor();
//function createEditor() {
//    console.log("111");
//    console.log(document.getElementById('editor'));
//    var editor = window.wangEditor;
//    var addEditor = new editor(document.getElementById('editor'));
//    addEditor.create();
//}


var menuSetting = {
    //async: {
    //    enable: true,
    //    url: "/RoleMenu/AuthenMenuData",
    //    autoParam: ["id", "name=n", "level=lv"],
    //    otherParam: { "otherParam": "tree" }
    //},
    view: {
        selectedMulti: false
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    check: {
        enable: true
        //chkboxType: {"Y":"s","N":"s"}
    }
};

function GetCheck(roleId) {
    console.log("111");
    var menuTree = $.fn.zTree.getZTreeObj("menuTree");
    menuTree.checkAllNodes(false);
    $.ajax({
        url: "/RoleMenu/AuthenRoleMenus?id=" + roleId,
        type: "get",
        dataType: "json",
        success: function (result) {
            for (var i = 0, id; id = result[i]; i++) {
                var node = menuTree.getNodeByParam("id", id);
                menuTree.checkNode(node, true, false, false);
            }
        }
    });
}

function saveAuthen() {
    var roles = [];
    $("#modelInfoList table tbody tr td").find("input[type=checkbox]").each(function (i, item) {
        if ($(item).is(':checked')) {
            roles.push(item.getAttribute("data-id"));
        }

    });
    if (roles != null && roles.length == 1) {
        menuTree = $.fn.zTree.getZTreeObj("menuTree");
        var menus = menuTree.getCheckedNodes(true);
        if (menus == null || menus.length === 0) {
            parent.layer.alert("请选择要授权的菜单");
        } else {
            var datas = [];
            var roleId = roles[0];
            console.log(roleId);
            for (var i = 0, menu; menu = menus[i]; i++) {
                datas.push({ RoleID: roleId, MenuID: menu.id });
            }
            console.log(datas);
            var btn = $(this);
            btn.button("loading");
            $.post("/RoleMenu/SaveRoleMenus", { "datas": datas }, function (result) {
                btn.button("reset");
                if (result.flag) {
                    parent.layer.alert("授权成功");
                } else {
                    parent.layer.alert("授权失败:" + result.msg);
                }
            });
        }
    } else if (roles != null && roles.length > 1) {
        parent.layer.alert("只能选择一个角色进行授权");
    } else {
        parent.layer.alert("请选择一个角色");
    }
}

function clearAuthen() {
    var roles = [];
    $("#modelInfoList table tbody tr td").find("input[type=checkbox]").each(function (i, item) {
        if ($(item).is(':checked')) {
            console.log($(item).next().val());
            roles.push({ "id": item.getAttribute("data-id"), "name": $(item).next().val() });
        }

    });
    if (roles != null && roles.length == 1) {
        var role = roles[0];
        var btn = $(this);
        btn.button("loading");
        parent.layer.confirm("您确定要清空【" + role.name + "】下的所有权限？", { btn: ['确认', '取消'] }, function () {
            var menuTree = $.fn.zTree.getZtreeObj("menuTree");
            $.post("/RoleMenu/ClearRoleMenus", { id: role.id }, function (result) {
                btn.button("reset");
                if (result.flag) {
                    menuTree.checkAllNodes(false);
                    parent.layer.alert("清空成功");
                } else {
                    parent.layer.alert("清空失败:" + res.msg);
                }
            });
        }, function () {
            btn.button("reset");
        });
    } else if (roles != null && roles.length > 1) {
        parent.layer.alert("只能选择一个角色进行授权");
    } else {
        parent.layer.alert("请选择一个角色");
    }
}

function GetMenuTree() {
    zTreeNodes = [];
    $.ajax({
        type: "GET",
        url: "/RoleMenu/AuthenMenuData",
        data: {},
        dataType: "text",
        async: false,
        success: function (result) {
            if (result.length > 0) {
                zTreeNodes = eval('(' + result + ')');
                //console.log(zTreeNodes);
            }
        },
        failure: function (response, options) { }

    });
    $.fn.zTree.init($("#menuTree"), menuSetting, zTreeNodes);
}
$(document).ready(function () {
    GetMenuTree();
    $("#btnSave").click(saveAuthen);
    $("#btnClear").click(clearAuthen);
    // $(".Subcb").click(onCheck($(this).prop("data-id")));
})
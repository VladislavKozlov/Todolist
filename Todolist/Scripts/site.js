var _dialog;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $(".ajaxLink").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $("#DialogContent").html(data);
            _dialog = $("#ModDialog")
            _dialog.modal("show");
        });
    });
});

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#AddTask").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $("#DialogContent").html(data);
            _dialog = $("#ModDialog")
            _dialog.modal("show");
        });
    });
});

function OnSuccess(result) {
    OnAjaxRequest(result);
}

function OnFailure() {
    $("#Results").html("Запрос не выполнен!");
}

function RefreshPartialContent() {
    var url = "/Todolist/PartialContent";
    $.get(url, null, function (data) {
        $("#PartialContent").html(data);
    });
}

function OnAjaxRequest(result) {
    if (result.EnableSuccess) {
        alert(result.SuccessMsg);
        _dialog.modal("hide");
        RefreshPartialContent();
    }
    if (result.EnableError) {
        $("#Results").html(result.ErrorMsg);
    }
}


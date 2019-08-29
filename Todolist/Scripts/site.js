var _partialContentUrl;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $(".ajaxLink").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $("#DialogContent").html(data);
            $("#ModDialog").modal("show");
        });
    });
});

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $("#AddTask").click(function (e) {
        e.preventDefault();
        $.get($(this).data("url"), function (data) {
            $("#DialogContent").html(data);
            $("#ModDialog").modal("show");
        });
    });
});

function OnSuccess(result) {
    OnAjaxRequest(result);
}

function OnFailure() {
    $("#Results").html("Запрос не выполнен!");
}

function RefreshPartialContent(url) {
    $.get(url, null, function (data) {
        $("#PartialContent").html(data);
    });
}

function OnAjaxRequest(result) {
    if (result.EnableSuccess) {
        alertBootstrap(result.SuccessMsg);
        $("#ModDialog").modal("hide");
        RefreshPartialContent(_partialContentUrl);
    }
    if (result.EnableError) {
        $("#Results").html(result.ErrorMsg);
    }
}

function initUrl(partialContentUrl) {
    _partialContentUrl = partialContentUrl;
}

function alertBootstrap(mess) {
    $("#InfoMessage").text(mess);
    $(".alert").show();
    window.setTimeout(function () {
        $(".alert").hide();
    }, 3000);
}



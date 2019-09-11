var _partialContentUrl;
var _checkCoincidencesUrl;

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $(document).on("click", ".ajaxLink", function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $("#DialogContent").html(data);
            $("#ModDialog").modal("show");
            keydownRequest();
        });
    });
    $("#AddTask").click(function (e) {
        e.preventDefault();
        $.get($(this).data("url"), function (data) {
            $("#DialogContent").html(data);
            $("#ModDialog").modal("show");
            keydownRequest();
        });
    });
});

function OnSuccess(result) {
    OnAjaxRequest(result);
}

function OnFailure() {
    $("#Results").html("Запрос не выполнен!");
}

function refreshPartialContent(url) {
    $.get(url, null, function (data) {
        $("#PartialContent").html(data);
    });
}

function OnAjaxRequest(result) {
    if (result.EnableSuccess) {
        alertBootstrap(result.SuccessMsg);
        $("#ModDialog").modal("hide");
        refreshPartialContent(_partialContentUrl);
    }
    if (result.EnableError) {
        $("#Results").html(result.ErrorMsg);
        lightBorderError();
    }
}

function initPartialContentUrl(partialContentUrl) {
    _partialContentUrl = partialContentUrl;
}

function initCheckCoincidencesUrl(checkCoincidencesUrl) {
    _checkCoincidencesUrl = checkCoincidencesUrl;
}

function alertBootstrap(mess) {
    $("#InfoMessage").text(mess);
    $("#Alert").removeClass("hide");
    window.setTimeout(function () {
        $("#Alert").addClass("hide");
    }, 3000);
}

function lightBorderError() {
    $("#ErrorMsg").removeClass("has-success has-error").addClass("has-error");
}

function keydownRequest() {
    $(document).keydown(function (e) {
        if (e.which == 32) {
            checkCoincidences(_checkCoincidencesUrl);
        }
    });
}

function checkCoincidences(url) {//$("#AjaxForm")
    //var $data = {};    
    //$data["taskDescription"] = $("#TaskDescription").val();
    //$data["todolistId"] = $("#TodolistId").val();

    var description = $("#TaskDescription").val();
    //var todolistId = $("#TodolistId").val();

    $.ajax({
        url: url,
        type: "POST",
        data: { taskDescription: description },//$data//$("#AjaxForm").serialize(),
        contentType: false,
        processData: false,
        success: function (result) {
            console.log("execut success");
            if (result.EnableSuccess) {
                $("#Results").html(result.EnableSuccess);
            }
            if (result.EnableError) {
                $("#Results").html(result.ErrorMsg);
            }
        },
        error: function () {
            console.log("execut error");
            $("#Results").html("Запрос не выполнен!");
        }
    });
}




var _partialContentUrl;
var _checkCoincidencesUrl;
var _descDescription = false;
var _descDate = false;
var _descApproved = false;

function initUrls(partialContentUrl, checkCoincidencesUrl) {
    _partialContentUrl = partialContentUrl;
    _checkCoincidencesUrl = checkCoincidencesUrl;
}

$(document).on("click", "#AddTask", function (e) {
    $.get($(this).data("url"), function (data) {
        $("#DialogContent").html(data);
        $("#ModDialog").modal("show");
    });
});

$(document).on("click", ".ajaxLink", function (e) {
    e.preventDefault();
    $.get(this.href, function (data) {
        $("#DialogContent").html(data);
        $("#ModDialog").modal("show");
    });
});

$(document).on("click", ".sort", function () {
    var data;
    var descending;
    var sortColumn = $(this).data("column");
    if (sortColumn === "TaskDescription") {
        descending = _descDescription;
        data = { sortColumn: sortColumn, descending: descending }
        _descDescription = !descending;
    }
    if (sortColumn === "EnrollmentDate") {
        descending = _descDate;
        data = { sortColumn: sortColumn, descending: descending }
        _descDate = !descending;
    }
    if (sortColumn === "Approved") {
        descending = _descApproved;
        data = { sortColumn: sortColumn, descending: descending }
        _descApproved = !descending;
    }
    $.ajax({
        url: _partialContentUrl,
        type: "GET",
        data: data,
        success: function (result) {
            $("#PartialContent").html(result);
        },
        error: function () {
            $("#PartialContent").html("Запрос не выполнен!");
        }
    });
});

$(document).on("input", "#TaskDescription", function (e) {
    checkCoincidences(_checkCoincidencesUrl);
});

function onSuccess(result) {
    onAjaxRequest(result);
}

function onFailure() {
    $("#Results").html("Запрос не выполнен!");
}

function refreshPartialContent(url) {
    $.get(url, null, function (data) {
        $("#PartialContent").html(data);
    });
}

function onAjaxRequest(result) {
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

function checkCoincidences() {
    var description = $("#TaskDescription").val();
    var todolistId = $("#TodolistId").val();
    $.ajax({
        url: _checkCoincidencesUrl,
        type: "POST",
        data: { taskDescription: description, taskId: todolistId },
        success: function (result) {
            if (result.EnableSuccess) {
                $("#Results").html(result.SuccessMsg);
            }
            if (result.EnableError) {
                $("#Results").html(result.ErrorMsg);
            }
        },
        error: function () {
            $("#Results").html("Запрос не выполнен!");
        }
    });

    $(document).on("click", ".paginLink", function (e) {
        e.preventDefault();
        var data;
        var page = $(this).attr("href");//???
        data = { page: page }
        console.log(data);
        $.ajax({
            url: _partialContentUrl,
            type: "GET",
            data: data,
            success: function (result) {
                $("#PartialContent").html(result);
            },
            error: function () {
                $("#PartialContent").html("Запрос не выполнен!");
            }
        });
    });
}




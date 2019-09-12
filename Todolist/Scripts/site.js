var _partialContentUrl;
var _checkCoincidencesUrl;
var _partialContentSortUrl;
var _descDescription = "";
var _descDate = "";

function initUrls(partialContentUrl, checkCoincidencesUrl, partialContentSortUrl) {
    _partialContentUrl = partialContentUrl;
    _checkCoincidencesUrl = checkCoincidencesUrl;
    _partialContentSortUrl = partialContentSortUrl;
}

function initSort(descDescription, descDate) {
    _descDescription = descDescription;
    _descDate = descDate;
}

$(document).on("click", "#AddTask", function (e) {
    e.preventDefault();
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

$(document).on("click", "#Description", function (e) {
    e.preventDefault();
    //var elemId = $(".sortLink")....;
    //var data = { sortOrder: elemId }
    if (_descDescription == "") {
        _descDescription = "true";
    }
    $.ajax({
        url: _partialContentSortUrl,
        type: "GET",
        data: { sortOrder: "Description", descending: _descDescription },//data
        success: function (result) {
            $("#PartialContent").html(result);
        },
        error: function () {
            $("#PartialContent").html("Запрос не выполнен!");
        }
    });
});

$(document).on("click", "#EnrollmentDate", function (e) {
    e.preventDefault();
    if (_descDate == "") {
        _descDate = "true";
    }
    $.ajax({
        url: _partialContentSortUrl,
        type: "GET",
        data: { sortOrder: "EnrollmentDate", descending: _descDate },
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
}




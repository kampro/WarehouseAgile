/// <reference path="jquery-1.7.1.min.js" />
/// <reference path="jquery-ui-1.10.2.custom.min.js" />

$(document).ready(function () {
    $("#models-make-select").change(function () {
        $("#models-make-form").submit();
    });
    $("#models-select").change(function () {
        $("#models-form").submit();
    });

    $("#makes-select").change(function () {
        $("#makes-form").submit();
    });

    $("#makes-section, #models-section, #equipments-section").hide();
    $("#makes-header").click(function () {
        showSection("#makes-section");
    });
    $("#models-header").click(function () {
        showSection("#models-section");
    });
    $("#equipments-header").click(function () {
        showSection("#equipments-section");
    });
    $(".ui-state-default").hover(
		function () { $(this).addClass("ui-state-hover"); },
		function () { $(this).removeClass("ui-state-hover"); }
	);
});

// Shared functions
function showResponse(element, message) {
    $(element).hide();
    $(element).html(message);
    $(element).fadeIn({ duration: 400, queue: false });
    $(element).delay(3000).fadeOut({ duration: 400, queue: true });
}
function showSection(section) {
    if ($(section).is(":hidden"))
        $(section).fadeIn(400);
    else
        $(section).fadeOut(400);
}

// Models functions
function saveModelSuccess() {
    showResponse("#savemodel-rsp", "<div class=\"success\">Dane zostały zapisane</div>");
}
function saveModelError() {
    showResponse("#savemodel-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}

// Makes functions
function addMakeSuccess() {
    $.ajax({
        url: "/Offer/GetMakes",
        dataType: "html",
        success: function (data) {
            reloadMakes(data);
            showResponse("#savemake-rsp", "<div class=\"success\">Dane zostały zapisane</div>");
        },
        error: function () {
            showResponse("#savemake-rsp", "<div class=\"error\">Wystąpił błąd</div>");
        }
    });
}
function saveMakeSuccess() {
    addMakeSuccess();
}
function reloadMakes(data) {
    $("#makes-select").html(data);
    $("#models-make-select").html(data);
}
function addMakeError() {
    showResponse("#savemake-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function saveMakeError() {
    showResponse("#savemake-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}

// Equipments functions
function addEquipmentSuccess() {
    $.ajax({
        url: "/Offer/GetEquipments",
        dataType: "html",
        success: function (data) {
            reloadEquipments(data);
            showResponse("#saveequipment-rsp", "<div class=\"success\">Dane zostały zapisane</div>");
        },
        error: function () {
            showResponse("#saveequipment-rsp", "<div class=\"error\">Wystąpił błąd</div>");
        }
    });
}
function reloadEquipments(data) {
    $("#equipments-select").html(data);
}
function addEquipmentError() {
    showResponse("#saveequipment-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function saveEquipmentError() {
    showResponse("#saveequipment-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}


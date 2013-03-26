﻿/// <reference path="jquery-1.7.1.min.js" />
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

    $("#equipments-select").change(function () {
        $("#equipments-form").submit();
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
    
    //Event bubbling
    $("body").click(function (e) {
        var target = e.target;

        if (isClickContained("a[rel=\"delete-equipment-model\"]", target)) {
            return deleteEquipmentModel(chooseTarget(target));
        }
        else if (isClickContained("a[rel=\"delete-equipment\"]", target)) {
            return deleteEquipment(chooseTarget(target));
        }
        else if (isClickContained("a[rel=\"delete-model\"]", target)) {
            return deleteModel(chooseTarget(target));
        }
        else if (isClickContained("a[rel=\"delete-make\"]", target)) {
            return deleteMake(chooseTarget(target));
        }

        return true;
    });
});

// Shared functions
function isClickContained(element, clicked) {
    if ($(clicked).parent().is(element) || $(clicked).is(element))
        return true;
    else
        return false;
}
function chooseTarget(element) {
    if ($(element).is("a"))
        return $(element);
    else
        return $(element).parent();
}
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
function addModelSuccess() {
    $.ajax({
        url: "/Offer/GetModels",
        data: {
            "make": $("#model-make-id").val()
        },
        dataType: "html",
        success: function (data) {
            $("#models-select").html(data);
            saveModelSuccess();
        }
    });
}
function saveModelSuccess() {
    showResponse("#savemodel-rsp", "<div class=\"success\">Dane zostały zapisane</div>");
}
function saveModelError() {
    showResponse("#savemodel-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function addModelEquipmentSuccess() {
    $.ajax({
        url: "/Offer/GetModelDetails",
        data: {
            "model": $("#model-id").val()
        },
        dataType: "html",
        success: function (data) {
            $("#model-details").html(data);
            saveModelSuccess();
        }
    });
}
function deleteEquipmentModel(e) {
    if (confirm("Potwierdź usunięcie wyposażenia z danego modelu - usuwanie wyposażenia jest NIEZALECANE (zostaną usunięte skojarzone z nim zamówienia)")) {
        $.ajax({
            url: $(e).attr("href"),
            dataType: "html",
            success: function () {
                addModelEquipmentSuccess();
            },
            error: function () {
                saveModelError();
            }
        });
    }

    return false;
}
function deleteModel(e) {
    if (confirm("Potwierdź usunięcie modelu oraz skojarzonych z nim obiektów - usuwanie modelu jest NIEZALECANE (zostaną usunięte skojarzone z nim zamówienia)")) {
        $.ajax({
            url: $(e).attr("href"),
            dataType: "html",
            success: function () {
                addModelSuccess();
            },
            error: function () {
                saveModelError();
            }
        });
    }

    return false;
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
function deleteMake(e) {
    if (confirm("Potwierdź usunięcie marki oraz skojarzonych z nią obiektów - usuwanie marki jest NIEZALECANE (zostaną usunięte skojarzone z nią modele i zamówienia)")) {
        $.ajax({
            url: $(e).attr("href"),
            dataType: "html",
            success: function () {
                addMakeSuccess();
            },
            error: function () {
                saveMakeError();
            }
        });
    }

    return false;
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
function saveEquipmentSuccess() {
    addEquipmentSuccess();
}
function reloadEquipments(data) {
    $("#equipments-select").html(data);
    $("#equipments-model-select").html(data);
}
function addEquipmentError() {
    showResponse("#saveequipment-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function saveEquipmentError() {
    showResponse("#saveequipment-rsp", "<div class=\"error\">Wystąpił błąd</div>");
}
function deleteEquipment(e) {
    if (confirm("Potwierdź usunięcie wyposażenia oraz skojarzonych z nim obiektów - usuwanie wyposażenia jest NIEZALECANE (zostaną usunięte skojarzone z nim zamówienia)")) {
        $.ajax({
            url: $(e).attr("href"),
            dataType: "html",
            success: function () {
                addEquipmentSuccess();
            },
            error: function () {
                saveEquipmentError();
            }
        });
    }

    return false;
}
